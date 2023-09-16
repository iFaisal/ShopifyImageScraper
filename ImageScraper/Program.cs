string baseUrl = "https://example.com/cdn/shop/files/IMG_";
string mainDirectory = AppDomain.CurrentDomain.BaseDirectory;
string downloadFolder = Path.Combine(mainDirectory, "Images");


for (int i = 1243; i <= 9999; i++)
{
    string imageName = $"{i:D4}";
    string imgUrl = $"{baseUrl}{imageName}.jpg";
    using (HttpClient client = new HttpClient())
    {
        try
        {
            var response = await client.GetAsync(imgUrl);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Image not found: {imgUrl}");
                continue;
            }

            Directory.CreateDirectory(downloadFolder);

            byte[] image = await response.Content.ReadAsByteArrayAsync();
            
            string imagePath = Path.Combine(downloadFolder, $"{imageName}.jpg");
            await File.WriteAllBytesAsync(imagePath, image);
            
            Console.WriteLine($"Downloaded: {imagePath}");

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    await Task.Delay(5000);
}