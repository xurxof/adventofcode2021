using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Utils;

namespace AoC15
{
    public class Cave
    {
        private readonly int[,] _Cave;

        public Cave (string input)
        {
            var lines = input.Split (Environment.NewLine);
            _Cave = new int[lines.Length, lines.First ()
                .Length];
            foreach (var line in lines.Select ((v, k) => new { k, v }))
            {
                foreach (var c in line.v.Select ((v, k) => new { k, v }))
                {
                    _Cave[line.k, c.k] = int.Parse (c.v.ToString ());
                }
            }
        }

        public List<Coord> Path { get; set; }

        public int Risk { get; set; }

        private IEnumerable<Coord> GetNeightbours (Coord coord)
        {
            var coords = new[] { coord.Left (), coord.Right (), coord.Top (), coord.Bottom () };
            return coords.Where (c => c.Col >= 0 && c.Row >= 0 && c.Col <= _Cave.GetUpperBound (1) && c.Row <= _Cave.GetUpperBound (0));
        }

        public (int risk, List<Coord> path) Search (Coord coord, List<Coord> path, int limit)
        {
            var pathRisk = path.Sum (c => _Cave[c.Row, c.Col]);
            if (pathRisk > limit)
            {
                return (int.MaxValue, null);
            }
            if (coord.Col == _Cave.GetUpperBound (1) && coord.Row == _Cave.GetUpperBound (0))
            {
                return (pathRisk, path);
            }
            var toExplore = GetNeightbours (coord)
                .Where (c => !path.Contains (c));
            //.OrderBy (c => _Cave[c.Row, c.Col])
            //.ToList ();
            int bestRisk = int.MaxValue;
            List<Coord> bestPath = null;
            foreach (var candidate in toExplore)
            {
                var r = Search (candidate,
                    new List<Coord> (path).Union (new[] { candidate })
                        .ToList (),
                    bestRisk);

                if (r.risk < bestRisk)
                {
                    bestPath = r.path;
                    bestRisk = r.risk;
                }
            }
            return (bestRisk, bestPath);
        }

        public void Search ()
        {
            var r = Search (new Coord (0, 0), new List<Coord> (), int.MaxValue);
            Risk = r.risk;
            Path = r.path;
        }

        [DebuggerDisplay ("{ToString()}")]
        public struct Coord
        {
            public override string ToString () => $"{Row},{Col}";

            public Coord (int row, int col)
            {
                Col = col;
                Row = row;
            }

            public int Col { get; set; }

            public int Row { get; set; }

            public Coord Left () => new Coord (Row, Col - 1);

            public Coord Right () => new Coord (Row, Col + 1);

            public Coord Top () => new Coord (Row - 1, Col);

            public Coord Bottom () => new Coord (Row + 1, Col);
        }
    }

    class Program
    {
        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=nF00Rb3IFJs
            Debug.WriteLine (Part1 (Input.Test.ToInput ()));
            Debug.WriteLine (Part2 (Input.Test.ToInput ()));
        }

        public static int Part1 (List<string> toInput) => 0;

        public static int Part2 (List<string> toInput) => 0;
    }
}