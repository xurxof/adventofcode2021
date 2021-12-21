using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Utils;

namespace AoC9
{
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
                Debug.WriteLine ($"Iteration: {i}");

                var rowLowerIndex = _Array.LowerBound (0)+1;
                var rowUpperIndex = _Array.UpperBound (0)-1;

                var colLowerIndex = _Array.LowerBound (1)+1;
                var colUperIndex = _Array.UpperBound (1)-1;
                for (int row = rowLowerIndex; row <= rowUpperIndex; row++)
                {
                    for (int col = colLowerIndex; col <= colUperIndex; col++)
                    {
                        var r = CalculateEnhalcement (row, col);
                        if (_NextValue[row, col] != r)
                        {
                            _NextValue[row, col] = r;
                        }
                    }
                }
                _NextValue.DefaultValue = _NextValue[_Array.LowerBound (0), _Array.LowerBound (1)];
                _Array = _NextValue;
                _NextValue = new NegativeIndexedArray<char> (_Array.Rows, _Array.Cols, _Array.Margin, _Array.DefaultValue);
                
            }
        }
    }
}