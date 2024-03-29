using System;
using System.Threading.Tasks;

namespace ConsoleApp38
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrayProcessor arrayProcessor = new ArrayProcessor();

            int[] array = new int[] { 1, 2, 2, 2, 2, 2, 2 };

            arrayProcessor.IncrementArrayElements(array);

            Console.ReadLine();
        }
    }

    public class ArrayProcessor
    {
        public void IncrementArrayElements(int[] array)
        {
            Parallel.For(0, array.Length, i =>
            {
                array[i]++;
            });
        }
    }
}
