using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class Extensions
    {
        public static string ConcatStrings<T> (this IEnumerable<T> source, string between = null, string end = null) =>
            source.Aggregate ("", (a, b) => a.ToString () + (between ?? "") + b) + (end ?? "");

        public static void ForEach<T> (this IEnumerable<T> e, Action<T> action)
        {
            foreach (var item in e)
            {
                action (item);
            }
        }

        public static bool IsLowercase (this string source) => source.ToLowerInvariant () == source;

        public static string Sorted (this string source)
        {
            if (source == null)
            {
                return null;
            }
            return source.OrderBy (c => c).Aggregate ("", (a, b) => a + b);
        }

        public static List<List<T>> Split<T> (this IEnumerable<T> source, int numElements)
        {
            return source.Select ((x, i) => new
                {
                    Index = i, Value = x
                })
                .GroupBy (x => x.Index / numElements)
                .Select (x => x.Select (v => v.Value).ToList ())
                .ToList ();
        }

        public static List<string> ToInput (this string s)
        {
            return s.Split (Environment.NewLine).Where (s => s != "").ToList ();
        }

        public static List<int> ToInputInt (this string s) => s.Split (',').Select (int.Parse).ToList ();

        public static T TryGet<T> (this T[,] source, int i, int j) where T : class
        {
            if (i < source.GetLowerBound (0))
            {
                return null;
            }
            if (i > source.GetUpperBound (0))
            {
                return null;
            }
            if (j < source.GetLowerBound (1))
            {
                return null;
            }
            if (j > source.GetUpperBound (1))
            {
                return null;
            }
            return source[i, j];
        }
    }
}