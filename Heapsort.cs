using System;

class Program
{
    public static void HeapSort(ref int[] arr)
    {
        int n = arr.Length;
        for (int i = (n - 1) / 2; i >= 0; i--)
            MaxHeapify(ref arr, n, i);

        for (int i = n - 1; i > 0; i--)
        {
            (arr[0], arr[i]) = (arr[i], arr[0]);
            MaxHeapify(ref arr, i, 0);
        }
    }

    private static void MaxHeapify(ref int[] arr, int size, int i)
    {
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        int largest = i;

        if (left < size && arr[left] > arr[largest])
            largest = left;

        if (right < size && arr[right] > arr[largest])
            largest = right;

        if (largest != i)
        {
            (arr[i], arr[largest]) = (arr[largest], arr[i]);
            MaxHeapify(ref arr, size, largest);
        }
    }

    public static void Main()
    {
        Console.WriteLine("Enter the number of elements:");
        int n = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the elements separated by spaces:");
        int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        HeapSort(ref arr);

        Console.WriteLine("\nSorted array:");
        Console.WriteLine(string.Join(" ", arr));
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
