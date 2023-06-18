using System;

public class SortMergedCommand : ICommand
{
    private int[] _array { get; }

    public SortMergedCommand(int[] unsortedArray)
    {
        if (unsortedArray is null)
        {
            throw new ArgumentNullException(nameof(unsortedArray));
        }
        _array = unsortedArray;
    }

    public void Execute()
    {
        MergeSort(_array);
    }

    private static void MergeSort(int[] arr)
    {
        int n = arr.Length;

        // Если массив содержит только один элемент, он уже отсортирован
        if (n < 2)
        {
            return;
        }

        int mid = n / 2;
        int[] leftArr = new int[mid];
        int[] rightArr = new int[n - mid];

        // Заполняем левую и правую половины массива
        for (int i = 0; i < mid; ++i)
        {
            leftArr[i] = arr[i];
        }
        for (int i = mid; i < n; ++i)
        {
            rightArr[i - mid] = arr[i];
        }

        // Рекурсивно сортируем левую и правую половины
        MergeSort(leftArr);
        MergeSort(rightArr);

        // Объединяем отсортированные левую и правую половины
        Merge(leftArr, rightArr, arr);
    }

    private static void Merge(int[] leftArr, int[] rightArr, int[] arr)
    {
        int leftLen = leftArr.Length;
        int rightLen = rightArr.Length;
        int i = 0,
            j = 0,
            k = 0;

        // Слияние отсортированных левой и правой половин
        while (i < leftLen && j < rightLen)
        {
            if (leftArr[i] <= rightArr[j])
            {
                arr[k++] = leftArr[i++];
            }
            else
            {
                arr[k++] = rightArr[j++];
            }
        }

        // Копируем оставшиеся элементы из левой половины (если такие есть)
        while (i < leftLen)
        {
            arr[k++] = leftArr[i++];
        }

        // Копируем оставшиеся элементы из правой половины (если такие есть)
        while (j < rightLen)
        {
            arr[k++] = rightArr[j++];
        }
    }
}
