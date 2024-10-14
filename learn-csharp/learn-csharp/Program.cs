using System;

namespace learn_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqTest linqTest = new LinqTest();
            linqTest.TestFirstOrDefault();
            Console.WriteLine("----------------------------------------------");

            string text = "banana";
            SuffixTree tree = new SuffixTree(text);
            tree.PrintTree();
        }
    }
}
