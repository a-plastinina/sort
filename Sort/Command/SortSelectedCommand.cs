using System;

public class SortSelectedCommand : ICommand
{
    private int[] _array { get; }
    
    public SortSelectedCommand(int[] unsortedArray)
    {
        if (unsortedArray is null)
        {
            throw new ArgumentNullException(nameof(unsortedArray));
        }
        _array = unsortedArray;
    }

    public void Execute()
    {
        SelectionSort(_array);
    }

    private static void SelectionSort(int[] arr)
{
    int n = arr.Length;

    // Проходим по всем элементам массива
    for (int i = 0; i < n - 1; ++i)
    {
        int minIndex = i;

        // Ищем наименьший элемент в оставшейся части массива
        for (int j = i + 1; j < n; ++j)
        {
            if (arr[j] < arr[minIndex])
            {
                minIndex = j;
            }
        }

        // Меняем местами текущий элемент и наименьший найденный элемент
        int temp = arr[minIndex];
        arr[minIndex] = arr[i];
        arr[i] = temp;
    }
}

}
