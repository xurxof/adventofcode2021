using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Priority_Queue;
using Utils;

namespace AoC15
{
    public class Cave
    {
        private readonly Dictionary<Coord, long> _Cave = new Dictionary<Coord, long> ();
        private readonly Dictionary<Coord, long> _Distancias = new Dictionary<Coord, long> ();
        private readonly int _MaxCol;
        private readonly int _MaxRow;
        private readonly Dictionary<Coord, Coord?> _Padre = new Dictionary<Coord, Coord?> ();
        private readonly Dictionary<Coord, bool> _Visto = new Dictionary<Coord, bool> ();

        public Cave (string input, bool f) : this (MultiplyCave (input))
        { }

        private static string MultiplyCave (string input)
        {
            // var rest = Add ("123456789", 3);
            var lineas = input.Split (Environment.NewLine);
            string[] newLineas = new string[lineas.Length * 5];
            for (int i = 0; i < 5; i++)
            {

                for (var r = 0; r < lineas.Length; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        newLineas[r + (i * lineas.Length)] += Add (lineas[r], i + c);
                    }
                }

            }
            return newLineas.ConcatStrings (Environment.NewLine).Trim ();
        }

        private static string Add (string linea, int i)
        {
            var result = linea.Select (c =>
                {
                    var r = int.Parse (c.ToString ()) + i;
                    if (r > 9)
                        r = r - 9;
                    return r;
                })
                .Select (c => c.ToString ())
                .ConcatStrings ();
            return result;
        }

        public Cave (string input)
        {
            var lines = input.Split (Environment.NewLine);
            _MaxCol = lines.First ()
                .Length - 1;
            _MaxRow = lines.Length - 1;
            foreach (var line in lines.Select ((v, k) => new { k, v }))
            {
                foreach (var c in line.v.Select ((v, k) => new { k, v }))
                {
                    var point = new Coord (line.k, c.k);
                    _Cave.Add (point, int.Parse (c.v.ToString ()));
                    _Distancias.Add (point, long.MaxValue);
                    _Visto.Add (point, false);
                    _Padre.Add (point, null);
                }
            }
            _Distancias[new Coord (0, 0)] = 0;

        }

        public List<Coord> Path { get; set; }

        public long Risk { get; set; }

        private List<Coord> GetNeightbours (Coord coord)
        {
            var coords = new[] { coord.Left (), coord.Right (), coord.Top (), coord.Bottom () };
            return coords.Where (c => c.Col >= 0 && c.Row >= 0 && c.Col <= _MaxCol && c.Row <= _MaxRow)
                .ToList ();
        }

        public void Search (Coord actual)
        {
            SimplePriorityQueue<Coord, long> cola = new SimplePriorityQueue<Coord, long> ();
            cola.Enqueue (actual, _Distancias[actual]);
            while (cola.Any ())
            {
                var u = cola.Dequeue ();
                _Visto[u] = true;
                foreach (var v in GetNeightbours (u))
                {
                    if (_Visto[v])
                    {
                        continue;
                    }
                    if (_Distancias[v] <= _Distancias[u] + _Cave[v])
                    {
                        continue;
                    }
                    _Distancias[v] = _Distancias[u] + _Cave[v];
                    _Padre[v] = u;
                    cola.Enqueue (v, _Distancias[v]);
                }
            }
            Risk = _Distancias[new Coord (_MaxRow, _MaxCol)];
        }

        public void Search ()
        {
            Search (new Coord (0, 0));
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
            Debug.WriteLine (Part1 ());
            Debug.WriteLine (Part2 ());
        }

        public static long Part1 ()
        {

            Cave C = new Cave (Input.Problem);
            C.Search ();
            return C.Risk;
        }

        public static long Part2 ()
        {

            Cave C = new Cave (Input.Problem, true);
            Stopwatch sw = new Stopwatch ();
            sw.Start ();
            C.Search ();
            sw.Stop ();
            Debug.WriteLine ("Elapsed=" + sw.Elapsed.TotalMinutes + " minutes");
            return C.Risk;
        }
    }
}