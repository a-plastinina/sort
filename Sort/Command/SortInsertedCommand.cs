using System;

public class SortInsertedCommand : ICommand
{
    private int[] _array { get; }
    
    public SortInsertedCommand(int[] unsortedArray)
    {
        if (unsortedArray is null)
        {
            throw new ArgumentNullException(nameof(unsortedArray));
        }
        _array = unsortedArray;
    }

    public void Execute()
    {
        InsertedSort(_array);
    }

    private static void InsertedSort(int[] arr)
    {
        int length = arr.Length;
        for (int i = 1; i < length; ++i)
        {
            int key = arr[i];
            int j = i - 1;

            // Перемещаем элементы _array[0..i-1], которые больше, чем key, на одну позицию вперед
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }
    }
}
