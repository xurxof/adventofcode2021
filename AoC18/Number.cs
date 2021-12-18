using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using Utils;

namespace AoC18
{
    [DebuggerDisplay ("{ToString()}")]
    public class Number
    {
        private Number _Left;
        private Number _Parent;
        private Number _Right;
        private int? _Value;

        public Number () { }

        private Number (Number parent) => _Parent = parent;

        public Number (string s) => Value = int.Parse (s);

        public bool Any => Left != null && Value != null;

        public bool Explodable => Parent?.Parent?.Parent?.Parent != null && Left?.Value != null && Right?.Value != null; //&& (Parent?.Left?.Value != null || Parent?.Right?.Value != null);

        public Number Left
        {
            get => _Left;
            set
            {
                _Left = value;
                if (_Left != null)
                {
                    _Left._Parent = this;
                }
            }
        }

        public Number Parent => _Parent;

        public Number Right
        {
            get => _Right;
            set
            {
                _Right = value;
                if (_Right != null)
                {
                    _Right._Parent = this;
                }
            }
        }

        public bool Splitable => Left?.Value >= 10 || Right?.Value >= 10;

        public Number TopNumber => Parent == null ? this : Parent.TopNumber;

        public int? Value
        {
            get => _Value;
            private set
            {
                _Value = value;
                Left = null;
                Right = null;
            }
        }

        public int Magnitude => Value.HasValue ? Value.Value : (Left.Magnitude * 3) + (Right.Magnitude * 2);
         

        public Number Add (Number other) =>
            FromString ("[" + this + "," + other + "]")
                .Reduce ();

        public IEnumerable<Number> EnumerateNodeLeftRight ()
        {
            if (Left?.Value == null)
            {
                foreach (var l in Left.EnumerateNodeLeftRight ())
                {
                    yield return l;
                }
            }

            if (Value == null)
            {
                yield return this;
            }
            if (Right?.Value == null)
            {
                foreach (var l in Right.EnumerateNodeLeftRight ())
                {
                    yield return l;
                }
            }
        }

        public IEnumerable<Number> EnumerateValuesLeftRight ()
        {
            if (Left.Value != null)
            {
                yield return Left;
            }
            else
            {
                foreach (var l in Left.EnumerateValuesLeftRight ())
                {
                    yield return l;
                }
            }
            if (Right.Value != null)
            {
                yield return Right;
            }
            else
            {
                foreach (var l in Right.EnumerateValuesLeftRight ())
                {
                    yield return l;
                }
            }
        }

        public IEnumerable<Number> EnumerateValuesRightLeft ()
        {
            if (Right.Value != null)
            {
                yield return Right;
            }
            else
            {
                foreach (var l in Right.EnumerateValuesRightLeft ())
                {
                    yield return l;
                }
            }
            if (Left.Value != null)
            {
                yield return Left;
            }
            else
            {
                foreach (var l in Left.EnumerateValuesRightLeft ())
                {
                    yield return l;
                }
            }
        }

        public IEnumerable<Number> Explode ()
        {
            var imLeft = Parent.Left == this;
            var StoredLeft = Left.Value;
            var StoredRight = Right.Value;
            var TopMost = TopNumber;

            IEnumerable<Number> LeftRight = TopMost.EnumerateValuesLeftRight ();

            var R = LeftRight.TakeWhile (c => c != Left)
                .LastOrDefault ();
            if (R != null)
            {
                R.Value += StoredLeft;
            }
            IEnumerable<Number> RightLeft = TopMost.EnumerateValuesRightLeft ();

            var L = RightLeft.TakeWhile (c => c != Right)
                .LastOrDefault ();
            if (L != null)
            {
                L.Value += StoredRight;
            }
            if (imLeft)
            {
                Parent.Left.Value = 0;
            }
            else
            {
                Parent.Right.Value = 0;
            }
            return Array.Empty<Number> ();
            //return new[] { L?.Parent, R?.Parent, Parent }.Where (n => n != null)
            //    .Distinct ();
        }

        public Number Find (int left, int right)
        {
            var leftToRight = EnumerateNodeLeftRight ()
                .ToList ();
            return leftToRight.FirstOrDefault (n => n?.Left?.Value == left && n?.Right?.Value == right);
        }

        public Number Find (int value)
        {
            var leftToRight = EnumerateNodeLeftRight ()
                .ToList ();
            return leftToRight.FirstOrDefault (n => n?.Left?.Value == value || n?.Right?.Value == value);
        }

        public static Number FromString (string stringInput)
        {
            var Input = new IndexedString (stringInput);

            Number N = new Number ();
            var First = N;
            Input.Consume (1);
            while (Input.Any ())
            {
                var c = Input.Consume (1);
                if (c == "[")
                {
                    if (N.Left == null)
                    {
                        N.Split ();
                        N = N.Left;
                    }
                    else
                    {
                        N.Right = new Number (N);
                        N = N.Right;
                    }
                    continue;
                }
                if (c == "]")
                {
                    N = N.Parent;
                    continue;
                }
                if (c != ",")
                {
                    var number = c;
                    while ("1234567890".Contains (Input.Peek (1)))
                    {
                        number += Input.Consume (1);
                    }
                    if (N.Left == null)
                    {
                        N.Left = new Number (number);
                    }
                    else
                    {
                        N.Right = new Number (number);
                    }
                }
            }
            return First;
        }

        public void Reduce (IEnumerable<Number> numbers)
        {
            foreach (var n in numbers)
            {
                if (n.Value != null)
                {
                    continue;
                }
                n.Reduce ();
            }
        }

        public Number Reduce ()
        {
            // Debug.WriteLine (this.ToString ());
            bool reduced = true;

            var leftToRight = EnumerateNodeLeftRight ()
                .ToList ();
            var Explodable = leftToRight.Where (n => n.Explodable)
                .ToList ();

            while (reduced)
            {
                reduced = false;

                if (Value != null)
                {
                    return this;
                }
                foreach (var n in Explodable)
                {
                    if (n.Explodable)
                    {
                        // Debug.WriteLine ($"Too deep ({n})");
                        var ChangedNodes = n.Explode ();
                        reduced = true;
                        //Debug.WriteLine ($"Explode: {TopNumber}");
                        Reduce (ChangedNodes);
                        Explodable.Remove (n);
                        break;
                    }
                }
                if (reduced)
                {
                    continue;
                }
                leftToRight = EnumerateNodeLeftRight ()
                    .ToList ();
                var Splitable = leftToRight.Where (n => n.Splitable);
                foreach (var n in Splitable)
                {
                    if (n.Left?.Value >= 10)
                    {
                        //Debug.WriteLine ($"Left too big ({n})");
                        var ChangedNodes = n.SplitLeft ();
                        reduced = true;
                        //Debug.WriteLine ($"Split: {TopNumber}");
                        Explodable.AddRange (ChangedNodes.Where (r=>r.Explodable));
                        break;
                    }
                    if (n.Right?.Value >= 10)
                    {
                        //Debug.WriteLine ($"Right too big ({n})");
                        var ChangedNodes = n.SplitRight ();
                        reduced = true;

                        //Debug.WriteLine ($"Split: {TopNumber}");
                        Explodable.AddRange (ChangedNodes.Where (r => r.Explodable));
                        break;
                    }
                }
            }

            return this;
        }

        public void Split ()
        {
            Left = new Number (this);
            Right = new Number (this);
        }

        public Number[] SplitLeft ()
        {
            int value = Left.Value.Value;
            Left.Value = null;
            Left.Left = new Number (Math.Round (value / 2.0, MidpointRounding.ToZero)
                .ToString ());
            Left.Right = new Number (Math.Round (value / 2.0, MidpointRounding.AwayFromZero)
                .ToString ());
            return new[] { Left, this };
        }

        public Number[] SplitRight ()
        {
            int value = Right.Value.Value;
            Right.Value = null;
            Right.Left = new Number (Math.Round (value / 2.0, MidpointRounding.ToZero)
                .ToString ());
            Right.Right = new Number (Math.Round (value / 2.0, MidpointRounding.AwayFromZero)
                .ToString ());
            return new[] { this, Right };
        }

        public static Number Sum (string input)
        {
            var lines = input.Split (Environment.NewLine);
            var n = FromString (lines.First ());
            foreach (var line in lines.Skip (1))
            {
                n = n.Add (FromString (line));
            }
            return n;
        }

        public override string ToString ()
        {
            if (Value != null)
            {
                return Value.ToString ();
            }
            if (Left != null)
            {
                return $"[{Left},{Right}]";
            }
            return "";
        }

        public static int Combination (string input)
        {
            var MaxMagnitude = 1;
            var Lines = input.Split (Environment.NewLine);
            foreach (var line1 in Lines)
            {
                foreach (var line2 in Lines.Where (l => l != line1))
                {
                    var r1  = Number.FromString (line1)
                        .Add (Number.FromString (line2));
                    var r2 = Number.FromString (line2)
                        .Add (Number.FromString (line1));
                    var r1Magnitude = r1.Magnitude;
                    var r2Magnitude = r2.Magnitude;
                    if (r1Magnitude > MaxMagnitude) MaxMagnitude = r1Magnitude;
                    if (r2Magnitude > MaxMagnitude) MaxMagnitude = r2Magnitude;

                }
            }
            return MaxMagnitude;

        }
    }
}