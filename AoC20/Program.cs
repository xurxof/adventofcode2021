using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AoC20;
using Utils;

namespace AoC9
{
    public class NegativeIndexedArray<T>
    {
        private T[,] _Array;
        private int _Margin;
        private T _Default;
         

        public NegativeIndexedArray (int rows, int cols, int margin, T defaultValue)
        {
            CreateArray (rows, cols, margin, defaultValue);
        }

        private void CreateArray (int rows, int cols, int margin, T defaultValue)
        {
            Rows = rows;
            Cols = cols;
            _Margin = margin;
            _Array = new T[rows + (margin * 2), cols + (margin * 2)];
            _Default = defaultValue;
            for (int i = 0; i <= _Array.GetUpperBound (0); i++)
            {
                for (int j = 0; j <= _Array.GetUpperBound (1); j++)
                {
                    _Array[i, j] = defaultValue;
                }
            }
        }

        public T this [int row, int col]
        {
            get
            {
                if (row + _Margin < 0 || row + _Margin > _Array.GetUpperBound (0) || col + _Margin < 0 || col + _Margin > _Array.GetUpperBound (1))
                    return _Default;
                // EnsureSize (row, col);
                return _Array[row + _Margin, col + _Margin];
            }
            set
            {

                EnsureSize (row, col);
                _Array[row + _Margin, col + _Margin] = value;
            }
        }

        private void EnsureSize (int row, int col)
        {
            while (row + _Margin < 0 || row + _Margin > _Array.GetUpperBound (0) || col + _Margin < 0 || col + _Margin > _Array.GetUpperBound (1))
            {
                var _Copy = _Array;
                UpgradeArray (_Copy);
            }
        }

        private void UpgradeArray (T[,] copy)
        {
            CreateArray (Rows, Cols, _Margin*2, _Default);
            // copy original array
            for (int row = 0; row <= copy.GetUpperBound (0); row++)
            {
                for (int col = 0; col <= copy.GetUpperBound (1); col++)
                {
                    _Array[row, col] = copy[row, col];
                }
            }
            Debug.WriteLine ($"New array: {_Array.ToString ()}");
        }

        public int Rows { get; private set; }

        public int Cols { get; private set; }

        public int Margin => _Margin;

        public T DefaultValue
        {
            get => _Default;
            set => _Default = value;
        }

        internal int LowerBound (int dimension) => -_Margin;

        internal int UpperBound (int dimension) => _Array.GetUpperBound (dimension) - _Margin;
    }

    public class Scanner
    {
        private static List<char> _Algorithm;
        private static NegativeIndexedArray<char> _Array;
        private static NegativeIndexedArray<char> _NextValue;

        public Scanner (string input, decimal percent)
        {
            var lines = input.Split (Environment.NewLine);
            _Algorithm = lines[0]
                .ToCharArray ()
                .Select (c => c == '.' ? '0' : '1')
                .ToList ();
            var imageLines = lines.Skip (2)
                .ToList ();
            var margin = (int) (Math.Max (imageLines[0]
                    .Length,
                imageLines.Count) * percent / 100);
            _Array = new NegativeIndexedArray<char> (imageLines[0]
                    .Length,
                imageLines.Count,
                margin,
                '0');
            _NextValue = new NegativeIndexedArray<char> (imageLines[0]
                    .Length,
                imageLines.Count,
                margin,
                '0');
            foreach (var line in imageLines.Select ((val, row) => new { row, val }))
            {
                foreach (var c in line.val.Select ((val, col) => new { col, val }))
                {
                    _Array[line.row, c.col] = c.val == '.' ? '0' : '1';
                }
            }
        } 
        public int LightPixels
        {
            get
            {
                int count = 0;
                for (int row = _Array.LowerBound (0); row <= _Array.UpperBound (0); row++)
                {
                    for (int col = _Array.LowerBound (1); col <= _Array.UpperBound (1); col++)
                    {
                        if(_Array[row,col]=='1') count++;
                    }
                }
                return count;
            }
        }

        public char CalculateEnhalcement (int row, int col)
        {
            var binaryString = NeighborsString (row, col);
            var decimalInteger = Convert.ToInt32 (binaryString, 2);
            return _Algorithm[decimalInteger];
        }

        private (int row, int col)[] GetNeighborsCoords (int row, int col) =>
            new[] { (row - 1, col - 1), (row - 1, col), (row - 1, col + 1), (row, col - 1), (row, col), (row, col + 1), (row + 1, col - 1), (row + 1, col), (row + 1, col + 1) };

        public string NeighborsString (int row, int col)
        {
            var r = GetNeighborsCoords (row, col)
                .Select (n => _Array[n.row, n.col]
                    .ToString ())
                .ConcatStrings ();
            return r;
        }

        public void EnhalceImage (int numIterations)
        {
            for (int i = 0; i < numIterations; i++)
            {


                for (int row = _Array.LowerBound (0); row <= _Array.UpperBound (0); row++)
                {
                    for (int col = _Array.LowerBound (1); col <= _Array.UpperBound (1); col++)
                    {
                        _NextValue[row, col] = CalculateEnhalcement (row, col);
                    }
                }
                _NextValue.DefaultValue = _NextValue[_Array.LowerBound (0), _Array.LowerBound (1)];
                _Array = _NextValue;
                _NextValue = new NegativeIndexedArray<char> (_Array.Rows, _Array.Cols, _Array.Margin, _Array.DefaultValue);
                
            }
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

        public static int Part1 ()
        {
            Scanner S = new Scanner (Input.Problem, 10);

            S.EnhalceImage (2);

            // asert
            return S.LightPixels;
            
        }

        public static int Part2 () => 0;
    }
}