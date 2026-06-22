
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows;
using WpfCalculator.Models; 

namespace WpfCalculator
{
    public partial class MainWindow : Window
    {
     
        private readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7000/") 
        };

        public MainWindow()
        {
            InitializeComponent();
          
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
                if (!double.TryParse(TextBoxA.Text, out double a) ||
                    !double.TryParse(TextBoxB.Text, out double b))
                {
                    MessageBox.Show("Введите корректные числа!", "Ошибка");
                    return;
                }

             
                var request = new CalcRequest { A = a, B = b };

                var response = await _client.PostAsJsonAsync("api/calculator/calculate", request);

                if (response.IsSuccessStatusCode)
                {
                   
                    var resultObject = await response.Content.ReadFromJsonAsync<dynamic>();
                  
                    ResultTextBox.Text = resultObject?.result.ToString() ?? "Ошибка";
                }
                else
                {
                    ResultTextBox.Text = "Ошибка сервера";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка соединения: {ex.Message}\nУбедитесь, что API запущен!", "Ошибка");
            }
        }

       
        private async void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var history = await _client.GetFromJsonAsync<List<Calculation>>("api/calculator");

               
                HistoryDataGrid.ItemsSource = history;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения истории: {ex.Message}", "Ошибка");
            }
        }
    }
}