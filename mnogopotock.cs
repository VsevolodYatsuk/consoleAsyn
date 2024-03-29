using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class DataProcessor
{
    public void ProcessData(ConcurrentBag<int> data)
    {
        ConcurrentBag<int> processedData = new ConcurrentBag<int>();

        Parallel.ForEach(data, item =>
        {
            int processedItem = item + 1;

            processedData.Add(processedItem);
        });
        Console.WriteLine("Обработанные элементы:");
        foreach (var processedItem in processedData)
        {
            Console.WriteLine(processedItem);
        }
    }
}
