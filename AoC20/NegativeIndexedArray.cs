using System;
using System.Diagnostics;

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

        public T this[int row, int col]
        {
            get
            {
                // if (row + _Margin < 0 || row + _Margin > _Array.GetUpperBound (0) || col + _Margin < 0 || col + _Margin > _Array.GetUpperBound (1))
                //    return _Default;
                EnsureSize (row, col);
                return _Array[row + _Margin, col + _Margin];
            }
            set
            {
                EnsureSize (row-1, col-1);
                EnsureSize (row-1, col+1);
                EnsureSize (row+1, col-1);
                EnsureSize (row+1, col+1);
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

        public int LightPixels
        {
            get
            {
                int count = 0;
                for (int row = _Array.GetLowerBound (0); row <= _Array.GetUpperBound (0); row++)
                {
                    for (int col = _Array.GetLowerBound (1); col <= _Array.GetUpperBound (1); col++)
                    {
                        if (Convert.ToChar (_Array[row, col]) == '1') count++;
                    }
                }
                return count;
            }
        }

        private void UpgradeArray (T[,] copy)
        {
            Debug.WriteLine ($"Old array: {_Array.GetUpperBound (0)}, {_Array.GetUpperBound (1)} - {this.LightPixels}");
            var oldMargin = _Margin;
            CreateArray (Rows, Cols, _Margin * 2, _Default);
            // copy original array
            for (int row = 0; row <= copy.GetUpperBound (0); row++)
            {
                for (int col = 0; col <= copy.GetUpperBound (1); col++)
                {
                    _Array[row + oldMargin, col + oldMargin] = copy[row, col];
                }
            }
            Debug.WriteLine ($"New array: {_Array.GetUpperBound (0)}, {_Array.GetUpperBound (1)} - {this.LightPixels}");
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
}