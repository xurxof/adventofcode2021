using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace AoC11
{
    internal class Cave
    {
        private readonly Octo[,] _Arr;

        public Cave (string input)
        {
            var lines = input.Split (Environment.NewLine);
            _Arr = new Octo[lines.Length, lines.First ().Length];
            foreach (var line in lines.Select ((val, idx) => (val, idx)))
            {
                foreach (var item in line.val.Select ((val, idx) => (val, idx)))
                {
                    _Arr[line.idx, item.idx] = new Octo (line.idx,
                        item.idx,
                        int.Parse (item.val.ToString ()));
                }
            }
        }

        private IEnumerable<Octo> GetElements ()
        {
            for (int i = 0; i <= _Arr.GetUpperBound (0); i++)
            {
                for (int j = 0; j <= _Arr.GetUpperBound (1); j++)
                {
                    yield return _Arr[i, j];
                }
            }
        }

        private IEnumerable<Octo> GetNeightbours (Octo octo)
        {
            var top = octo.I - 1;
            var bottom = octo.I + 1;
            var left = octo.J - 1;
            var right = octo.J + 1;
            return new[]
            {
                _Arr.TryGet (top, left), _Arr.TryGet (top, octo.J),
                _Arr.TryGet (top, right), _Arr.TryGet (octo.I, left),
                _Arr.TryGet (octo.I, octo.J), _Arr.TryGet (octo.I, right),
                _Arr.TryGet (bottom, left), _Arr.TryGet (bottom, octo.J),
                _Arr.TryGet (bottom, right)
            }.Where (n => n != null);
        }

        private (long Count, bool AllBright) Step ()
        {
            GetElements ().ForEach (kv => kv.Val++);

            List<(int, int)> NotRecursive = new List<(int, int)> ();
            while (true)
            {
                List<Octo> Brights = GetElements ()
                    .Where (kv => kv.Val >= 10)
                    .Where (n => !NotRecursive.Contains ((n.I, n.J)))
                    .ToList ();
                var Neightbours = Brights.SelectMany (GetNeightbours);
                // NotRecursive .AddRange (Neightbours.Where (o=>o.Val>9).Select (n=>(n.I,n.J)));
                Neightbours.ForEach (n => n.Val++);
                if (!Neightbours.Any ())
                {
                    break;
                }
                NotRecursive.AddRange (Brights.Select (n => (n.I, n.J)));
            }
            var LastBright = GetElements ().Where (o => o.Val > 10).ToList ();
            LastBright.ForEach (k => k.Val = 0);
            var allBrigh = GetElements ().Count () ==
                           GetElements ().Count (o => o.Val == 0);

            return (LastBright.LongCount (), allBrigh);
        }

        public long Steps (int numSteps)
        {
            long count = 0;
            for (int i = 0; i < numSteps; i++)
            {
                var r = Step ();
                count += r.Count;
                
            }
            return count;
        }



        public long StepsUntilAllBright ()
        {
            int i = 0; 
            while (true)
            {
                var r = Step ();
                i++;
                if (r.AllBright)

                {
                    return i;
                }
            }
            
        }

        public override string ToString ()
        {
            string s = "";
            for (int i = 0; i <= _Arr.GetUpperBound (0); i++)
            {
                for (int j = 0; j <= _Arr.GetUpperBound (1); j++)
                {
                    s += _Arr[i, j].Val.ToString ();
                }
                s += Environment.NewLine;
            }
            return s.SkipLast (2).ConcatStrings ();
        }

        public class Octo
        {
            public Octo (int i, int j, int val)
            {
                I = i;
                J = j;
                Val = val;
            }

            public int I { get; set; }

            public int J { get; set; }

            public int Val { get; set; }
        }
    }
}