using System.Threading.Tasks;

public class ArrayProcessor
{
    public void IncrementArrayElements(int[] array)
    {
        Parallel.ForEach(array, (element, state, index) =>
        {
            array[index] = element + 1;
        });
    }
}
