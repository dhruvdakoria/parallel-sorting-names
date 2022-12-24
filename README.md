# Parallel Name Sorter

## Objective

Write a parallel implementation without Linq in C# to read the file names.txt of 10,000 people names (first name followed by given name) and sort this list – by last name (first) and then by first name (for any repeated last names)

## Description

This C# program reads in a list of names from a file called "names.txt" and sorts the list of names by last name and then by first name in parallel using the Parallel.Invoke() method and a merge sort algorithm. It then writes the sorted list of names to a file called "output.txt" and also prints it to the console. The program also measures and prints the time it takes to sort the list of names.

### How it works

- The list of names is stored in a List<name> object called "Names" and is populated by reading in each line of the "names.txt" file and splitting it by the space character to separate the first and last names.
- The list of names is then converted to an array of "name" objects called "namesArray" and passed to the ParallelMergeSort() function along with the left and right indices of the portion of the array to be sorted.
- The ParallelMergeSort() function uses the Parallel.Invoke() method to sort the left and right halves of the array in parallel and then merges the sorted halves using the MergeAndCompare() function.
- The MergeAndCompare() function creates temporary arrays for the left and right halves of the array and copies the elements from the original array into these temporary arrays. It then compares the elements in the left and right arrays and merges them back into the original array in the correct order. If there are any remaining elements in the left or right arrays, they are also added to the original array. It uses the CompareNames() function to compare two names and determine the order they should be sorted in.
- Finally, the sorted array is converted back to a list of "name" objects and written to the "output.txt" file and printed to the console. The program also measures and prints the time it took to sort the list of names using a Stopwatch object.

### How to run

- Run main.cs
- Tested on Repl.it

### Runtime

Ran 3 times with an average time of about 140 ms.

Sample Output:
Sorting...
Code took 148 milliseconds to execute
Press Return to exit
