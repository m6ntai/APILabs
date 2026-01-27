using System.Net.Http.Json;
var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7043/");
var data = new
{
    A = 10,
    B = 2
};
var response = await client.PostAsJsonAsync("api/calculator/calculate", data);
var result = await response.Content.ReadAsStringAsync();
Console.WriteLine($"Результат: {result}");