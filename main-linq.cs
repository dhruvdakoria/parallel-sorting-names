using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        List<name> Names = new List<name>();
        // populate the list of names from a file
        using (StreamReader sr = new StreamReader("names.txt"))
        {
            while (sr.Peek() >= 0)
            {
                string[] s = sr.ReadLine().Split(' ');
                Names.Add(new name(s[0], s[1]));
            }
        }
        Console.WriteLine("Sorting...");
        // time the sort.
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<name> sortedNames = Names.OrderBy(s => s.lastName).ThenBy(s => s.firstName).ToList();
        stopwatch.Stop();
        Console.WriteLine("Code took {0} milliseconds to execute", stopwatch.ElapsedMilliseconds);
        foreach (name n in sortedNames)
        {
            Console.WriteLine("{0} {1}", n.firstName, n.lastName);
        }
        Console.WriteLine("Press Return to exit");
        Console.ReadLine();
    }
}