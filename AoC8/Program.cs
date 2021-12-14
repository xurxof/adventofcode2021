using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Utils;

namespace AoC8
{
    public class Signals
    {
        private readonly Dictionary<int, string> _Dic = new Dictionary<int, string> ();

        //readonly int[] _FixedLens =
        //{
        //    2, 4, 3, 7
        //};

        //private readonly string[] _Cues;
        private readonly Dictionary<string, int> _SegmentsToNumber;

        public Signals (string data)
        {
            var tmp = data.Split ('|')
                .Select (s => s.Split (' ')
                    .Where (z => z != "")
                    .ToList ())
                .ToList ();
            Input = tmp[0];
            Output = tmp[1];
            //new[]
            //{
            //    'a', 'b', 'c', 'd', 'e', 'f', 'g'
            //}.ForEach (a => _Dic.Add (a, '-'));
            //_Cues = new []
            //{
            //    "cf",
            //    "bcdf",
            //    "acf",
            //    "abcdefg"
            //};
            //GetKnowPairs ();
            _SegmentsToNumber = new Dictionary<string, int>
            {
                ["abcefg"] = 0,
                ["cf"] = 1,
                ["acdeg"] = 2,
                ["acdfg"] = 3,
                ["bcdf"] = 4,
                ["abdfg"] = 5,
                ["abdefg"] = 6,
                ["acf"] = 7,
                ["abcdefg"] = 8,
                ["abcdfg"] = 9
            };
        }

        public List<string> Input { get; }

        public List<string> Output { get; }

        //public void GetKnowPairs ()
        //{
        //    var comparable = _FixedLens.Select (l => new
        //    {
        //        L = l,
        //        In = Input.Where (i => i.Length == l)
        //            .Distinct ().FirstOrDefault(),
        //        Cues = _Cues.Where (i => i.Length == l)
        //            .Distinct ()
        //            .FirstOrDefault ()
        //    });
        //    comparable =comparable.Where (l => l.In!=null);
        //    foreach (var s in comparable)
        //    {
        //        var valueTuples = s.In
        //            .Zip (s.Cues).ToList ();
        //        valueTuples
        //            .ForEach (a => _Dic[a.First] = a.Second);
        //    }
        //}

        public string Output2Numbers ()
        {
            //var InputTranslatedSegments = Output.Select (s=>s.Select (c=> _Dic[c]).OrderBy (c=>c).Aggregate ("",(c,d)=>c.ToString()+d)).ToList ();

            //return InputTranslatedSegments.Select (t => _SegmentsToNumber[t].ToString()).ConcatStrings ();
            _Dic[1] = Input.FirstOrDefault (l => l.Length == 2);
            _Dic[4] = Input.FirstOrDefault (l => l.Length == 4);
            _Dic[7] = Input.FirstOrDefault (l => l.Length == 3);
            _Dic[8] = Input.FirstOrDefault (l => l.Length == 7);
            _Dic[2] = Input.FirstOrDefault (l => l.Length == 5 && Solapa (l, 4) == 2);
            _Dic[3] = Input.FirstOrDefault (l => l.Length == 5 && Solapa (l, 1) == 2);
            _Dic[5] = Input.FirstOrDefault (l => l.Length == 5 && Solapa (l, 4) == 4);
            _Dic[6] = Input.FirstOrDefault (l => l.Length == 6 && Solapa (l, 1) == 1 && Solapa (l, 4) == 3);
            _Dic[0] = Input.FirstOrDefault (l => l.Length == 6 && Solapa (l, 1) == 2 && Solapa (l, 4) == 3);
            _Dic[9] = Input.FirstOrDefault (l => l.Length == 6 && Solapa (l, 1) == 2 && Solapa (l, 4) == 5);
            
            string s = "";
            foreach (var output in Output)
            {
                s +=  _Dic.FirstOrDefault ((kv) => kv.Value.Sorted() == output.Sorted()).Key.ToString();

            }
            return s;
        }

        private int Solapa (string s, int target)
        {
            if (!_Dic.ContainsKey (target))
            {
                return 0;
            }
            else
            {
                return s.Count (c => _Dic[target]
                    .Contains (c));
            }
        }
    }

    public class Program
    {
        public static int Digest (string test)
        {
            // 2 seg, 4, 3, 7 seg
            var strings = test.Split (Environment.NewLine);
            IEnumerable<List<string>> inputs = strings.Select (s => s.Split ('|')
                .ToList ());
            var selectMany = inputs.SelectMany (s => s[1]
                .Split (' '));
            return selectMany.Count (s => new[]
            {
                2, 4, 3, 7
            }.Contains (s.Length));
        }

        public static List<string> Digest2 (string test)
        {
            // 2 seg, 4, 3, 7 seg
            var strings = test.Split (Environment.NewLine);
            var signals = strings.Select (s => new Signals (s))
                .ToList ();
            // emparejamos los segmentes de 2,4,3,7 segmentos
            return signals.Select (s => s.Output2Numbers ())
                .ToList ();
        }

        static void Main (string[] args)
        {
            Debug.WriteLine (Digest (Input.Problem));
        }
    }
}