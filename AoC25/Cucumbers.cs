using System;

namespace AoC25
{
    public class Cucumbers
    {
        private readonly int _NumCols;
        private readonly int _NumRows;
        private char[,] _Arr;
        private char[,] _NextStep;

        public Cucumbers (string input)
        {
            var lines = input.Split (Environment.NewLine);
            _Arr = new char[lines.Length, lines[0].Length];
            _NextStep = new char[lines.Length, lines[0].Length];
            _NumRows = lines.Length;
            _NumCols = lines[0].Length;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    _Arr[i, j] = lines[i][j];
                    _NextStep[i, j] = '.';
                }
            }
        }

        private int Down (int row)
        {
            if (row + 1 == _NumRows)
            {
                return 0;
            }
            return row + 1;
        }

        private int Right (int col)
        {
            if (col + 1 == _NumCols)
            {
                return 0;
            }
            return col + 1;
        }

        public bool Step ()
        {
            // east face
            bool Changed = false;
            for (int row = 0; row < _NumRows; row++)
            {
                for (int col = 0; col < _NumCols; col++)
                {
                    if (_Arr[row, col] == '>')
                    {
                        var rightCol = Right (col);
                        if (_Arr[row, rightCol] == '.')
                        {
                            _NextStep[row, rightCol] = '>';
                            _NextStep[row, col] = '.';
                            Changed = true;
                        }
                        else _NextStep[row, col] = '>';

                    }
                }
            }
            // south face
            for (int row = 0; row < _NumRows; row++)
            {
                for (int col = 0; col < _NumCols; col++)
                {
                    if (_Arr[row, col] == 'v')
                    {
                        var downRow = Down (row);
                        if (_NextStep[downRow, col] == '.' && _Arr[downRow, col] != 'v')
                        {
                            _NextStep[row, col] = '.';
                            _NextStep[downRow, col] = 'v';
                            Changed = true;
                        }
                        else _NextStep[row, col] = 'v';
                    }
                }
            }
            _Arr = (char[,]) _NextStep.Clone ();
            _NextStep = new char[_NumRows, _NumCols];
            for (int row = 0; row < _NumRows; row++)
            {
                for (int col = 0; col < _NumCols; col++)
                {
                    _NextStep[row, col] = '.';
                }
            }
            return Changed;
        }

        public override string ToString ()
        {
            var s = "";
            for (int row = 0; row < _NumRows; row++)
            {
                for (int col = 0; col < _NumCols; col++)
                {
                    s += _Arr[row, col];
                }
                s += Environment.NewLine;
            }
            return s.Trim ();
        }

        public int StepUntil ()
        {
            bool changed;
            int i = 0;
            do
            {
                changed = Step ();

                i++;

            } while (changed);
            return i;
        }
    }
}