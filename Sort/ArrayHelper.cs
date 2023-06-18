public class ArrayHelper
{
    public static int[] CreateRandom(int length, int maxValue)
    {
        var result = new int[length];
        var random = new Random();

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = random.Next(maxValue);
        }

        return result;
    }

    public static void Print<T>(IEnumerable<T> array)
    {
        Console.WriteLine($"Length = {array.Count()}");
        Console.WriteLine(array.Aggregate("", (first, next) => $"{first},{next}").Trim(','));
    }
}
