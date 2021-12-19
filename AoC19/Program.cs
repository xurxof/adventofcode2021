using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AoC9;

namespace AoC19
{
    [DebuggerDisplay ("{ToString()}")]
    public class Scanner
    {
        private List<PointsDistance> _Distances;
        private List<Point> _Points = new List<Point> ();

        public IEnumerable<Point> Points
        {
            get => _Points;
            set
            {
                _Points = value.ToList ();
                _Distances = null;
            }
        }

        public (int BestOverlappedCount, (int rotationX, int rotationY, int rotationZ) RotationToOriginal, Point IncrementToOriginal, Point IncrementToOther, List<Point> OverlappedPoints, List<Point>
            TransformedPoints) Compare (Scanner other)
        {
            int best = 0;
            (int rotationX, int rotationY, int rotationZ) bestRotationToOriginal = default;
            Point bestIncrementToOriginal = default;
            List<Point> bestTransformedPoints = new List<Point> ();
            List<Point> bestOverlappedPoints = new List<Point> ();
            Point bestIncrementToOther = default;
            var LocalDistances = Distances ()
                .GroupBy (d => d.Distance)
                .Select (d => new
                {
                    Distance = d.Key,
                    Points = d.SelectMany (pd => pd.Points)
                        .Distinct ()
                        .ToList ()
                });
            var OtherDistances = other.Distances ()
                .GroupBy (d => d.Distance)
                .Select (d => new
                {
                    Distance = d.Key,
                    Points = d.SelectMany (pd => pd.Points)
                        .Distinct ()
                        .ToList ()
                });

            foreach (var localDistance in LocalDistances)
            {
                var potentialOtherPoints = OtherDistances.FirstOrDefault (od => od.Distance == localDistance.Distance);
                if (potentialOtherPoints == null)
                {
                    continue;
                }
                foreach (var localPoint in localDistance.Points)
                {
                    foreach (var otherPoint in potentialOtherPoints.Points)
                    {
                        // intentamos igualar otherPoint a localPoint
                        foreach (var rotationX in new[] { 0, 1, 2, 3 })
                        {
                            foreach (var rotationY in new[] { 0, 4, 5, 6 })
                            {
                                foreach (var rotationZ in new[] { 0, 7, 8, 9 })
                                {
                                    Point otherRotated = otherPoint.Rotate (rotationX)
                                        .Rotate (rotationY)
                                        .Rotate (rotationZ);
                                    Point incrementToLocal = otherRotated.IncrementTo (localPoint);
                                    //Point incrementToOther = localPoint.IncrementTo (otherRotated);
                                    //
                                    // aplicamos todos la transformacion a todos los puntos de other
                                    // y comparamos cuantos sin iguales
                                    var TransformedPoints = other.Points.Select (p => p.Rotate (rotationX)
                                            .Rotate (rotationY)
                                            .Rotate (rotationZ)
                                            .Increment (incrementToLocal))
                                        .ToList ();
                                    var OverlappedPoints = TransformedPoints.Where (o => Points.Contains (o))
                                        .ToList ();
                                    if (OverlappedPoints.Count () > best)
                                    {
                                        best = OverlappedPoints.Count ();
                                        bestRotationToOriginal = (rotationX, rotationY, rotationZ);
                                        bestIncrementToOriginal = incrementToLocal;
                                        //bestIncrementToOther = incrementToOther;
                                        bestOverlappedPoints = OverlappedPoints;
                                        bestTransformedPoints = TransformedPoints;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (best, bestRotationToOriginal, bestIncrementToOriginal, bestIncrementToOther, bestOverlappedPoints, bestTransformedPoints);
        }

        public IEnumerable<PointsDistance> Distances ()
        {
            if (_Distances != null)
            {
                return _Distances;
            }
            _Distances = new List<PointsDistance> ();
            for (int i = 0; i < _Points.Count (); i++)
            {
                for (int j = i; j < _Points.Count (); j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var d = _Points[i]
                        .To (_Points[j]);
                    _Distances.Add (new PointsDistance (_Points[i], _Points[j], d));
                }
            }
            return _Distances;
        }

        public static Scanner From (string input)
        {
            var B = new Scanner ();
            foreach (var line in input.Trim ()
                .Split (Environment.NewLine))
            {
                B._Points.Add (Point.From (line));
            }
            return B;
        }

        public static (IEnumerable<Point> Points, List<Point> ScannerCoord) IterativeCompare (Scanner[] scanners)
        {
            List<Scanner> Pending = new List<Scanner> (scanners.Skip (1));
            // Scanner Transformed = scanners.First (); 
            List<Scanner> Processed = new List<Scanner> ();

            List<Scanner> ToCompare = scanners.Take (1)
                .ToList ();
            List<Point> _ScannerCoord = new List<Point> ();
            _ScannerCoord.Add (new Point (0, 0, 0));
            while (Pending.Any ())
            {
                bool change = false;
                List<Scanner> ToCompareNext = new List<Scanner> ();
                foreach (var p in Pending)
                {
                    foreach (var t in ToCompare)
                    {
                        if (ToCompareNext.Contains (p))
                        {
                            continue;
                        }
                        var result = t.Compare (p);
                        if (result.BestOverlappedCount >= 12)
                        {
                            // Pending.Remove (p);
                            p.Points = result.TransformedPoints;
                            _ScannerCoord.Add (result.IncrementToOriginal);
                            ToCompareNext.Add (p);
                            // Union() includes Distinct()
                            //t.Points = t.Points.Union (result.TransformedPoints)
                            //    .ToList (); 
                            Debug.WriteLine ($"{Pending.Count}");
                        }
                    }
                }
                Pending.RemoveAll (p => ToCompareNext.Contains (p));
                Processed.AddRange (ToCompare);
                ToCompare = ToCompareNext;
            }
            Processed.AddRange (ToCompare);
            /// return Transformed.Points;
            return (Processed.SelectMany (t => t.Points)
                .Distinct (),_ScannerCoord);
        }

        public override string ToString () => $"{_Points.Count}";

        public void Transform (int rotation, Point increment)
        {
            _Points = _Points.Select (p => p.Rotate (rotation)
                    .Increment (increment))
                .ToList ();
        }

        [DebuggerDisplay ("{ToString()}")]
        public class PointsDistance
        {
            public PointsDistance (Point p1, Point p2, decimal distance)
            {
                P1 = p1;
                P2 = p2;
                Distance = distance;
            }

            public decimal Distance { get; }

            public Point P1 { get; }

            public Point P2 { get; }

            public IEnumerable<Point> Points => new[] { P1, P2 };

            public override string ToString () => $"[{Distance}] {P1}=>{P2}";
        }
    }

    [DebuggerDisplay ("{ToString()}")]
    public readonly struct Point
    {
        public override string ToString () => $"{X},{Y},{Z}";

        public Point (int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point From (string input)
        {
            var p = input.Trim ()
                .Split (',')
                .Select (int.Parse)
                .ToArray ();
            return new Point (p[0], p[1], p[2]);
        }

        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public decimal To (Point other) => (decimal) Math.Round (Math.Sqrt (Math.Pow (other.X - X, 2) + Math.Pow (other.Y - Y, 2) + Math.Pow (other.Z - Z, 2)), 6);

        public Point Rotate (int rotation)
        {
            if (rotation == 0)
            {
                return this;
            }
            if (rotation == 1)
            {
                return new Point (-Y, X, Z);
            }
            if (rotation == 2)
            {
                return new Point (-X, -Y, Z);
            }
            if (rotation == 3)
            {
                return new Point (Y, -X, Z);
            }
            if (rotation == 4)
            {
                return new Point (X, Z, -Y);
            }
            if (rotation == 5)
            {
                return new Point (X, -Y, -Z);
            }
            if (rotation == 6)
            {
                return new Point (X, -Z, Y);
            }
            if (rotation == 7)
            {
                return new Point (-Z, Y, X);
            }
            if (rotation == 8)
            {
                return new Point (-X, Y, -Z);
            }
            if (rotation == 9)
            {
                return new Point (Z, Y, -X);
            }
            throw new InvalidOperationException ();
        }

        public Point IncrementTo (Point other) =>
            // cuanto hay que sumar a 'this' para que resulte en 'other'
            new Point (other.X - X, other.Y - Y, other.Z - Z);

        public Point Increment (Point other) => new Point (X + other.X, Y + other.Y, Z + other.Z);

        public Point Transform (int rotation, Point variation) =>
            Rotate (rotation)
                .Increment (variation);
         
    }

    class Program
    {
        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=nF00Rb3IFJs
            Debug.WriteLine (Part1 ());
            Debug.WriteLine (Part2 ());
        }

        public static int Part1 ()
        {
            string tmp = "";
            var _Scanners = new List<Scanner> ();
            foreach (var line in Input.Problem.Split (Environment.NewLine))
            {
                if (line.Contains ("---"))
                {
                    continue;
                }
                if (line == "")
                {
                    var s = Scanner.From (tmp);
                    _Scanners.Add (s);
                    tmp = "";
                    continue;
                }
                tmp += line + Environment.NewLine;
            }
            var last = Scanner.From (tmp);
            _Scanners.Add (last);
            var Result = Scanner.IterativeCompare (_Scanners.ToArray ());
            // 398 -> too low, 13:33 
            // 404 -> 1:15
            Debug.WriteLine (Result.Points.Count ());
            List<int> Manhattan = new List<int> ();
            foreach (var center1 in Result.ScannerCoord)
            {
                foreach (var center2 in Result.ScannerCoord)
                {
                    Manhattan.Add (Math.Abs (center1.X - center2.X) + Math.Abs (center1.Y - center2.Y) + Math.Abs (center1.Z - center2.Z));
                }
            }

            Debug.WriteLine (Manhattan.Max());
            return 0;
        }

        public static int Part2 () => 0;
    }
}