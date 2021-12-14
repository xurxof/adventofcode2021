using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC14
{
    public class PolyBuilder
    {
        private string _LastPair;
        private Dictionary<string, long> _Poly;
        private readonly Dictionary<string, string> _Rules;

        public PolyBuilder (string input)
        {
            _Rules = new Dictionary<string, string> ();
            var lines = input.Split (Environment.NewLine);
            SplitPairs (lines[0]);
            foreach (var line in lines.Skip (2))
            {
                var kv = line.Split (" -> ");
                _Rules.Add (kv[0], kv[1]);
            }
        }

        public long Points
        {
            get
            {
                var chars = _Poly.GroupBy (kv => kv.Key[0]).ToDictionary (kv=>kv.Key,kv=>kv.Sum(kv2=>kv2.Value));
                //new[]
                //{
                //    _LastPair[1]
                //}
                chars[_LastPair[1]] += 1;
                // var repetitions = chars.Select (kv => kv.ValueCount ());

                long Max = chars.Values.Max ();
                long Min = chars.Values.Min ();
                return Max - Min;
            }
        }

        public void SplitPairs (string input)
        {
            _Poly = new Dictionary<string, long> ();
            var lenPoly = input.Length;
            string Result = "";
            for (int i = 0; i < lenPoly - 1; i++)
            {
                var Pair = input.Substring (i, 2);
                if (_Poly.ContainsKey (Pair))
                {
                    _Poly[Pair] += 1;
                }
                else
                {
                    _Poly.Add (Pair, 1);
                }
            }
            _LastPair = input.Substring (input.Length - 2);
        }

        public void Step ()
        {
            Dictionary<string, long> N = new Dictionary<string, long> ();
            foreach (var pair in _Poly)
            {
                if (!_Rules.ContainsKey (pair.Key))
                {
                    N.Add (pair.Key, pair.Value);
                    continue;
                }
                var firstNewPair = pair.Key[0] + _Rules[pair.Key];
                if (!N.TryAdd (firstNewPair, pair.Value)) N[firstNewPair] += pair.Value;
                
                var secondNewPair = _Rules[pair.Key] + pair.Key[1];
                if (!N.TryAdd (secondNewPair, pair.Value)) N[secondNewPair] += pair.Value;
                
                if (_LastPair == pair.Key)
                    _LastPair = secondNewPair;
            }
            _Poly = N;
        }

        public void Steps (int numSteps)
        {
            for (int j = 0; j < numSteps; j++)
            {
                Step ();
            }
        }
    }

    class Program
    {
        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=1RcVIuZ8Wdk
            Debug.WriteLine (Part1 (Input.Problem));
            Debug.WriteLine (Part2 (Input.Problem));
        }

        public static long Part1 (string input)
        {
            PolyBuilder B = new PolyBuilder (input);
            B.Steps (10);
            return B.Points;
        }

        public static long Part2 (string input)
        {
            PolyBuilder B = new PolyBuilder (input);
            B.Steps (40);
            return B.Points;
        }
    }
}