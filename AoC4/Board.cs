using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Utils;

namespace AoC4
{
    [DebuggerDisplay ("{ToString()}")]
    public class Board
    {
        private readonly List<Dictionary<int, bool>> _Board;

        public Board (string input)
        {
            _Board = input.Split (Environment.NewLine)
                .Select (l =>
                {
                    var keys = l.Split (' ').Where (c => c != "").Select (int.Parse);
                    var d = new Dictionary<int, bool> ();
                    foreach (var key in keys)
                    {
                        d.Add (key, false);
                    }
                    return d;
                })
                .ToList ();
        }

        public string CalcStrign =>
            _Board.Select (l => l.Select (kv => kv.Key +
                                                (kv.Value
                                                    ? "·"
                                                    : "") +
                                                " ")
                    .ConcatStrings (" ", Environment.NewLine))
                .ConcatStrings ();

        public bool HasLine
        {
            get
            {
                if (_Board.Any (l => l.Values.All (v => v)))
                {
                    return true;
                }
                var perColumn = Enumerable.Range (0, 5)
                    .Select (i =>new {Col=i, Items= _Board.Select (b => b.Values.ElementAt(i))});
                if (perColumn.Any (col => col.Items.All (item => item)))
                {
                    return true;
                }
                return false;
            }
        }

        public int Marks => _Board.SelectMany (l => l).Count (kv => kv.Value);

        public int UnmarkedSum
        {
            get
            {
                var keyValuePairs = _Board.SelectMany (l => l.Select (kv => kv));
                var valuePairs = keyValuePairs.Where (kv => !kv.Value);
                var unmarkedSum = valuePairs.Sum (kv => kv.Key);
                return unmarkedSum;
            }
        }

        public void Mark (int num)
        {
            var line = _Board.FirstOrDefault (l => l.ContainsKey (num));
            if (line != null)
            {
                line[num] = true;
            }
        }

        public override string ToString () =>
            CalcStrign; //.Aggregate ((a,b)=>a+b);
    }
}