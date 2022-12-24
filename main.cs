/*
Write a parallel implementation in C# to read the file names.txt of 10,000 people names 
(first name followed by given name) and sort this list – by last name (first) 
and then by first name (for any repeated last names)
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

class name
{
    public name(string fname, string lname)
    {
        this.firstName = fname; this.lastName = lname;
    }
    public string firstName { get; set; }
    public string lastName { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list of names
        List<name> Names = new List<name>();

        // Populate the list of names from a file
        using (StreamReader sr = new StreamReader("names.txt"))
        {
            while (sr.Peek() >= 0)
            {
                string[] s = sr.ReadLine().Split(' ');
                Names.Add(new name(s[0], s[1]));
            }
        }

        Console.WriteLine("Sorting...");
        // Time the sort
        Stopwatch stopwatch = Stopwatch.StartNew();
        // Convert the list to an array
        name[] namesArray = Names.ToArray();
        // Sort the array in parallel
        ParallelMergeSort(namesArray, 0, namesArray.Length - 1);
        stopwatch.Stop();

        // Convert the sorted array back to a list
        List<name> sortedNames = new List<name>(namesArray);

        // Print out the sorted list of names to file output.txt
        using (StreamWriter sw = new StreamWriter("output.txt"))
        {
            foreach (name n in sortedNames)
            {
                sw.WriteLine("{0} {1}", n.firstName, n.lastName);
            }
        }
        // Print to console
        foreach (name n in sortedNames)
        {
            Console.WriteLine("{0} {1}", n.firstName, n.lastName);
        }
        Console.WriteLine("Code took {0} milliseconds to execute", stopwatch.ElapsedMilliseconds);
        Console.WriteLine("Press Return to exit");
        Console.ReadLine();
    }

    // Function to sort an array of names in parallel using a merge sort algorithm
    private static void ParallelMergeSort(name[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            // Sort the left half of the array in parallel
            Parallel.Invoke(() => ParallelMergeSort(array, left, middle));
            // Sort the right half of the array in parallel
            Parallel.Invoke(() => ParallelMergeSort(array, middle + 1, right));
            // Merge the sorted halves
            MergeAndCompare(array, left, middle, right);
        }
    }

    // Function to merge two sorted halves of an array
    private static void MergeAndCompare(name[] array, int left, int middle, int right)
    {
        // Create temporary arrays to store the left and right halves of the array
        name[] leftArray = new name[middle - left + 1];
        name[] rightArray = new name[right - middle];
        Array.Copy(array, left, leftArray, 0, middle - left + 1);
        Array.Copy(array, middle + 1, rightArray, 0, right - middle);

        // Merge the left and right arrays back into the original array
        int i = 0, j = 0, k = left;
        while (i < leftArray.Length && j < rightArray.Length)
        {
            if (CompareNames(leftArray[i], rightArray[j]) < 0)
            {
                array[k] = leftArray[i];
                i++;
            }
            else
            {
                array[k] = rightArray[j];
                j++;
            }
            k++;
        }
        // Add any remaining elements from the left array to the original array
        while (i < leftArray.Length)
        {
            array[k] = leftArray[i];
            i++;
            k++;
        }
        // Add any remaining elements from the right array to the original array
        while (j < rightArray.Length)
        {
            array[k] = rightArray[j];
            j++;
            k++;
        }
    }

    // Function to compare two names and determine the order they should be sorted in
    private static int CompareNames(name a, name b)
    {
        // Compare the last names
        int comparison = a.lastName.CompareTo(b.lastName);
        // If the last names are the same, compare the first names
        if (comparison == 0)
        {
            comparison = a.firstName.CompareTo(b.firstName);
        }
        return comparison;
    }
}