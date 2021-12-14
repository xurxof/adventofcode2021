using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC5
{
    public class Program
    {
        /// <summary></summary>
        /// <param name="input">Ex: 0,9 -> 5,9</param>
        /// <returns></returns>
        private static Line GeneratePoints (string input)
        {
            var cords = input.Split (" -> ");
            var point1 = cords[0];
            var point2 = cords[1];
            var cord1 = point1.Split (',').ToList ();
            var cord2 = point2.Split (',').ToList ();

            return new Line (new[]
            {
                new Coord (cord1[0], cord1[1]), new Coord (cord2[0], cord2[1])
            });
        }

        static void Main (string[] args)
        {
            Func<Line, bool> OnlyHV = c => c.Cord1.C1 == c.Cord2.C1 || c.Cord1.C2 == c.Cord2.C2;
            int r = Process (Input.P1_Test, OnlyHV);
            r = Process (Input.P1_Input, OnlyHV);

            //

            r = Process (Input.P1_Test, _ => true);
            r = Process (Input.P1_Input, _ => true);
        }

        private static int Process (string[] inputs, Func<Line, bool> condition)
        {
            var lines = inputs.Select (GeneratePoints);
            var board = new Board ();
            board.Mark (lines, condition);

            return board.LeastTwoLinePonts ();
        }

        [DebuggerDisplay ("{ToString()}")]
        public readonly struct Coord
        {
            public int C2 { get; }

            public int C1 { get; }

            public Coord (string c1, string c2) : this (int.Parse (c1), int.Parse (c2))
            {
            }

            public Coord (int c1, int c2)
            {
                C2 = c2;
                C1 = c1;
            }

            public override string ToString () =>
                C1 + "," + C2;
        }
    }

    [DebuggerDisplay ("{ToString()}")]
    public class Board
    {
        private readonly Dictionary<Program.Coord, int> _Dict = new Dictionary<Program.Coord, int> ();

        public int LeastTwoLinePonts () =>
            _Dict.Values.Count (v => v >= 2);

        public void Mark (IEnumerable<Line> lines, Func<Line, bool> condition)
        {
            foreach (var line in lines.Where (l => condition (l)))
            {
                Mark (line);

                // Debug.WriteLine (ToString ());
            }
        }

        private void Mark (Line lines)
        {
            foreach (var cord in lines.Coords ())
            {
                if (_Dict.ContainsKey (cord))
                {
                    _Dict[cord]++;
                }
                else
                {
                    _Dict.Add (cord, 1);
                }
            }
        }

        public override string ToString ()
        {
            if (_Dict.Count == 0)
            {
                return "";
            }
            var maxC1 = _Dict.Keys.Max (c => c.C1);
            var maxC2 = _Dict.Keys.Max (c => c.C2);
            string s = "";
            for (int j = 0; j <= maxC2; j++)
            {
                var line = "";
                for (int i = 0; i <= maxC1; i++)
                {
                    var coord = new Program.Coord (i, j);
                    line += _Dict.ContainsKey (coord)
                        ? _Dict[coord].ToString ()
                        : ".";
                }
                s += line + Environment.NewLine;
            }
            return s;
        }
    }

    [DebuggerDisplay ("{ToString()}")]
    public class Line
    {
        public Line (IEnumerable<Program.Coord> cords)
        {
            Cord1 = cords.First ();
            Cord2 = cords.Last ();
        }

        public Program.Coord Cord1 { get; }

        public Program.Coord Cord2 { get; }

        public IEnumerable<Program.Coord> Coords ()
        {
            int C1 = Cord1.C1;
            int C2 = Cord1.C2;
            while (true)
            {
                var value = new Program.Coord (C1, C2);
                yield return value;
                if (value.C1 == Cord2.C1 &&
                    value.C2 == Cord2.C2)
                {
                    break;
                }
                if (Cord1.C1 < Cord2.C1)
                {
                    C1++;
                }
                else if (Cord1.C1 > Cord2.C1)
                {
                    C1--;
                }
                if (Cord1.C2 < Cord2.C2)
                {
                    C2++;
                }
                else if (Cord1.C2 > Cord2.C2)
                {
                    C2--;
                }
            }
        }

        public override string ToString () =>
            Cord1 + "->" + Cord2;
    }
}