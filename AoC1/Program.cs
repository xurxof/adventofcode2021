using System;
using System.Collections.Generic;
using System.Linq;
using AoC1;

namespace ConsoleApp1
{
    class Program
    {
        private static int CheckIncrements (List<int> ints)
        {
            return ints.Where ((s, i) => i > 0 && s > ints[i - 1]).Count ();
        }

        private static List<int> ExtractInts (string stringInput) =>
            stringInput.Split (Environment.NewLine).Select (int.Parse).ToList ();

        static void Main (string[] args)
        {
            var test = CheckIncrements (ExtractInts (Input.P1_TestInput));
            int num = CheckIncrements (ExtractInts (Input.P1_StringInput));
            Console.WriteLine (num);
            //
            List<int> preparedTest = PrepareData (ExtractInts (Input.P1_TestInput));
            var test2 = CheckIncrements (preparedTest);
            preparedTest = PrepareData (ExtractInts (Input.P1_StringInput));
            int num2 = CheckIncrements (preparedTest);
        }

        private static List<int> PrepareData (List<int> ints)
        {
            return Enumerable.Range (0, ints.Count - 2).Select (i => ints[i] + ints[i + 1] + ints[i + 2]).ToList ();
        }
    }
}