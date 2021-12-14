using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Utils;

namespace AoC9
{
    class Program
    {
        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=lATJR6DG23k & https://www.youtube.com/watch?v=enk9srBmTqQ
            Debug.WriteLine (Part1 (Input.Problem.ToInput ()));
            // 29184309 too low
            // Debug.WriteLine (Part2 (Input.Test.ToInput ()));
        }

        public static (int pointsByWrong, long pointsByAutocomplete) Part1 (List<string> toInput)
        {
            var open = "([{<";
            var close = ")]}>";
            var wrong = "";
            var autoComplete = new List<string> ();
            foreach (var chunk in toInput)
            {
                Stack<char> _Q = new Stack<char> ();
                foreach (var c in chunk)
                {
                    if (open.Contains (c))
                    {
                        _Q.Push (c);
                        continue;
                    }
                    _Q.TryPop (out char lastQueuedChar);
                    if (lastQueuedChar == 0)
                    {
                        wrong += c;
                        _Q.Clear ();
                        break;
                    }
                    var expectedOpen = open[close.IndexOf (c)];
                    if (lastQueuedChar != expectedOpen)
                    {
                        wrong += c;
                        _Q.Clear ();
                        break;
                    }
                }
                autoComplete.Add (_Q.AsEnumerable ().ConcatStrings ());
            }
            int points = 0;
            Dictionary<int, int> prizesByWrong = new Dictionary<int, int>
            {
                [')'] = 3,
                [']'] = 57,
                ['}'] = 1197,
                ['>'] = 25137
            };
            Dictionary<int, int> prizesByAutocomplete = new Dictionary<int, int>
            {
                ['('] = 1,
                ['['] = 2,
                ['{'] = 3,
                ['<'] = 4
            };
            var pointsByWrong = wrong.Select (c => prizesByWrong[c]).Sum ();
            List<long> pointsByAutocomplete = new List<long> ();
            
            foreach (var completed in autoComplete.Where (s=>s!=""))
            {
                long subtotal = 0;
                foreach (var c in completed)
                {
                    subtotal *= 5;
                    subtotal += prizesByAutocomplete[c];
                }
                pointsByAutocomplete.Add (subtotal);
            }
            
            return (pointsByWrong,
                pointsByAutocomplete.OrderBy (p => p)
                    .ElementAt ((pointsByAutocomplete.Count / 2)));
        }

        public static int Part2 (List<string> toInput) =>
            0;
    }
}