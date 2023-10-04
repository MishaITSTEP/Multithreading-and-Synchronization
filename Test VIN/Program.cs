class Program
{
    static async Task Main()
    {
        Console.OutputEncoding=System.Text.Encoding.Unicode;

        // Замініть цю URL адресу на адресу вашого сервера або сервісу, який надає інформацію за VIN номером.
        string apiUrl = "https://example.com/api/vininfo/";

        // Замініть "YOUR_VIN_NUMBER" на VIN номер автомобіля, для якого ви хочете отримати інформацію.
        string vinNumber = "YOUR_VIN_NUMBER";

        using(HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl+vinNumber);

                if(response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Отримано інформацію за VIN номером:");
                    Console.WriteLine(responseData);
                } else
                {
                    Console.WriteLine($"Помилка: {response.StatusCode}");
                }
            } catch(Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
