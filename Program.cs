using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class DownloadManager
{
    public async Task DownloadFilesAsync(string[] urls)
    {
        var tasks = new Task[urls.Length];
        for (int i = 0; i < urls.Length; i++)
        {
            tasks[i] = DownloadFileAsync(urls[i]);
        }
        await Task.WhenAll(tasks);
    }

    private async Task DownloadFileAsync(string url)
    {
        Console.WriteLine($"Начало скачивания файла {url}.");

        using (var httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string fileName = Path.GetFileName(url);
                string saveDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string filePath = Path.Combine(saveDirectory, fileName);
                if (File.Exists(filePath))
                {
                    fileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{Path.GetExtension(fileName)}";
                    filePath = Path.Combine(saveDirectory, fileName);
                }

                using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                              stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await contentStream.CopyToAsync(stream);
                }

                Console.WriteLine($"Файл успешно скачан и сохранен как {filePath}.");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка при скачивании файла {url}: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при скачивании файла {url}: {ex.Message}");
            }
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        string[] urls = { "https://github.com/VsevolodYatsuk/DevOps-KT1/archive/refs/heads/main.zip", "https://github.com/VsevolodYatsuk/cloud-yandex/archive/refs/heads/main.zip" };
        var downloadManager = new DownloadManager();
        await downloadManager.DownloadFilesAsync(urls);

        Console.WriteLine("Программа завершена.");
    }
}
