using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Utils;

namespace AoC9
{
    [DebuggerDisplay ("{ToString ()}")]
    public readonly struct Point
    {
        public Point (string input)
        {
            var s = input.Split (',');
            X = int.Parse (s[0]);
            Y = int.Parse (s[1]);
        }

        public Point (int pointX, int pointY)
        {
            X = pointX;
            Y = pointY;
            if (X < 0 ||
                Y < 0)
            {
                throw new Exception ("Invalid value");
            }
        }

        public int X { get; }

        public int Y { get; }

        public override string ToString () =>
            X + "," + Y;
    }

    public class Fold
    {
        private List<string> _Instructions;
        List<Point> _Points;
        private int _MaxX;
        private int _MaxY;

        public Fold (string input)
        {
            var Lines = input.Split (Environment.NewLine);
            _Points = new List<Point> ();

            foreach (var s in Lines.TakeWhile (f => f != ""))
            {
                _Points.Add (new Point (s));
            }
            _MaxX = _Points.Max (p => p.X);
            _MaxY = _Points.Max (p => p.Y);
            _Instructions = Lines.Where (f => f.Contains ("fold")).ToList ();
        }

        private void FoldHorizontal (int edge)
        {
            var remove = _Points.Where (p => p.Y > edge).ToList ();
            remove.ForEach (r => _Points.Remove (r));
            //
            foreach (var point in remove)
            {
                _Points.Add (new Point (point.X, point.Y - ((point.Y - edge) * 2)));
            }
            // remove duplicates
            _Points = _Points.Distinct ().OrderBy (p => p.ToString ()).ToList ();
            _MaxY = edge - 1;
        }

        private void FoldVertical (int edge)
        {
            var remove = _Points.Where (p => p.X >= edge).ToList ();
            remove.ForEach (r => _Points.Remove (r));
            //
            foreach (var point in remove)
            {
                _Points.Add (new Point (point.X - ((point.X - edge) * 2), point.Y));
            }
            // remove duplicates
            _Points = _Points.Distinct ().OrderBy (t => t.ToString ()).ToList ();
            _MaxX = edge - 1;
        }

        public int Process ()
        {
            var f = int.Parse (_Instructions.First ().Remove (0, 13));
            if (_Instructions.First ()[11] == 'y')
            {
                FoldHorizontal (f);
            }
            else
            {
                FoldVertical (f);
            }
            _Instructions = _Instructions.Skip (1).ToList ();
            return _Points.Count ();
        }

        public override string ToString ()
        {
            string S = "";
            for (int y = 0; y <= _MaxY; y++)
            {
                for (int x = 0; x <= _MaxX; x++)
                {
                    if (_Points.Contains (new Point (x, y)))
                    {
                        S += "#";
                    }
                    else
                    {
                        S += ".";
                    }
                }
                S += Environment.NewLine;
            }
            return S;
        }

        public void ProcessFull ()
        {
            while (_Instructions.Any ())
            {
                Process ();
            }
        }
    }

    class Program
    {
        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=nF00Rb3IFJs
            Debug.WriteLine (Part1 (Input.Test.ToInput ()));
            // 791: too high!
            Debug.WriteLine (Part2 (Input.Test.ToInput ()));
        }

        public static int Part1 (List<string> toInput)
        {
            var F = new Fold (Input.Problem);
            // asert
            var r = F.Process ();
            return r;
        }

        public static int Part2 (List<string> toInput)
        {
            var F = new Fold (Input.Problem);
            // asert
            F.ProcessFull ();
            System.Diagnostics.Debug.Print (F.ToString ());
            return 0;
        }
    }
}