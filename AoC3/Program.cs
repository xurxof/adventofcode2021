using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC3
{
    public class Program
    {
        public static (string, string) CalcGammaBinary (string[] input)
        {
            var (itemLenght, numItems, zeroes) = CalculateResume (input);

            string GetString (string ifZeroesString, string ifOnesString)
            {
                return Enumerable.Range (0, itemLenght)
                    .Select (i => zeroes[i].MaxRepeatedValue == '0'
                        ? ifZeroesString
                        : ifOnesString)
                    .Aggregate ((a, b) => a + b);
            }

            return (GetString ("0", "1"), GetString ("1", "0"));
        }

        public static (string, string) CalcOxygenCo2 (string[] input)
        {
            string GetFilteredValues (Func<char, char, bool> condition)
            {
                
                IEnumerable<string> filtered = input;
                var itemLenght = input.First ().Length;
                for (int i = 0; i < itemLenght; i++)
                {
                    var (_, _, resumes) = CalculateResume (filtered.ToArray ());
                    filtered = filtered.Where (item => condition (item[i], resumes[i].MaxRepeatedValue)).ToList ();
                    if (filtered.Count () == 1)
                    {
                        return filtered.First ();
                    }
                }
                return "";
            }

            
            var oxygen = GetFilteredValues ((value, maxRepeatedValue) => value == maxRepeatedValue);
            var co2 = GetFilteredValues ((value, maxRepeatedValue) => value != maxRepeatedValue);
            return (oxygen, co2);
        }

        public static int CalculateResult (string gamma, string epsilon) =>
            Convert.ToInt32 (gamma, 2) * Convert.ToInt32 (epsilon, 2);

        private static (int itemLenght, int numItems, List<Resume> zeroes) CalculateResume (string[] input)
        {
            var numItems = input.Length;
            var itemLenght = input.First ().Length;

            var zeroes = Enumerable.Range (0, itemLenght)
                .Select (i => input.Select (s => s[i]).Count (s => s == '0'))
                .Select ((v, idx) => new Resume
                {
                    Idx = idx,
                    MaxRepeatedValue = v > (numItems/2)
                        ? '0'
                        : '1'
                })
                .ToList ();
            return (itemLenght, numItems, zeroes);
        }

        static void Main (string[] args)
        {
            var (gamma, epsilon) = CalcGammaBinary (Input.P1_Test.ToArray ());
            var r = CalculateResult (gamma, epsilon);
            (gamma, epsilon) = CalcGammaBinary (Input.P1_Input.ToArray ());
            r = CalculateResult (gamma, epsilon);
            // P2
            var (oxygen, co2) = CalcOxygenCo2(Input.P1_Input.ToArray ());
            r = CalculateResult (oxygen, co2);
        }
    }

    public class Resume
    {
        public int Idx { get; set; }

        public char MaxRepeatedValue { get; set; }
    }
}