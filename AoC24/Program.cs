using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AoC9;
using NUnit.Framework;

namespace AoC24
{
    [DebuggerDisplay ("{W},{X},{Y},{Z}")]
    public readonly struct Registers
    {
        public override string ToString () =>
            $"{W},{X},{Y},{Z}";

        public int W { get; }

        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public Registers (int w, int x, int y, int z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Alu
    {
        private const int W = 0;
        private const int X = 1;
        private const int Y = 2;
        private const int Z = 3;
        private readonly string[] _Instructions;
        private readonly Dictionary<string, Action<string>> _Processor;
        private Queue<int> _Inputs;
        private int[] _Record;

        public Alu (string program)
        {
            _Instructions = program.Split (Environment.NewLine);
            _Record = new[]
            {
                0, 0, 0, 0
            };
            _Processor = new Dictionary<string, Action<string>>
            {
                ["inp"] = instruction => Input (ExtractParameter (instruction)),
                ["add"] = instruction => Add (ExtractParameters (instruction)),
                ["mul"] = instruction => Mul (ExtractParameters (instruction)),
                ["div"] = instruction => Div (ExtractParameters (instruction)),
                ["mod"] = instruction => Mod (ExtractParameters (instruction)),
                ["eql"] = instruction => Eql (ExtractParameters (instruction))
            };
        }

        private void Add ((int a, int val) parameters)
        {
            _Record[parameters.a] = _Record[parameters.a] + parameters.val;
        }

        private void Div ((int a, int val) parameters)
        {
            var divisionResult = (decimal) _Record[parameters.a] / parameters.val;
            Assert.True (divisionResult >= 0);
            _Record[parameters.a] = (int) Math.Truncate (divisionResult);
        }

        private void Eql ((int a, int val) parameters)
        {
            _Record[parameters.a] = _Record[parameters.a] == parameters.val ? 1 : 0;
        }

        private void Execute (string instruction)
        {
            _Processor[instruction.Substring (0, 3)] (instruction);
        }

        public int ExtractParameter (string source)
        {
            var letter = source.Split (' ')[1];
            return IndexFrom (letter);
        }

        public (int idx, int val) ExtractParameters (string source)
        {
            var letter = source.Split (' ');
            // if second param is w,x,y,z, return value from record
            // if not, the second param is a int, return it as int
            int val = ("wxyz".Contains (letter[2])) ? _Record[IndexFrom (letter[2])] : int.Parse (letter[2]);
            return (IndexFrom (letter[1]), val);
        }

        private static int IndexFrom (string letter) =>
            letter[0] - 119; // 119 == 'w'

        private void Input (int idx)
        {
            var val = _Inputs.Dequeue ();
            _Record[idx] = val;
        }

        private void Mod ((int a, int val) parameters)
        {
            if (_Record[parameters.a] < 0 || parameters.val <= 0)
            {
                throw new InvalidOperationException ();
            }
            _Record[parameters.a] = _Record[parameters.a] % parameters.val;
        }

        private void Mul ((int a, int val) parameters)
        {
            _Record[parameters.a] = _Record[parameters.a] * parameters.val;
        }

        public (Registers r, bool Error) Process (string input)
        {
            _Inputs = new Queue<int> (input.Select (c => int.Parse (c.ToString ())));
            bool Err = false;
            try
            {
                foreach (var instruction in _Instructions)
                {
                    Execute (instruction);
                }
            }
            catch (Exception ex) when (ex is DivideByZeroException || ex is InvalidOperationException)
            {
                Err = true;
            }
            return (new Registers (_Record[W], _Record[X], _Record[Y], _Record[Z]), Err);
        }

        public void Reset ()
        {
            _Record = new[]
            {
                0, 0, 0, 0
            };
        }

        public void SetRegister (Registers register)
        {
            _Record[W] = register.W;
            _Record[X] = register.X;
            _Record[Y] = register.Y;
            _Record[Z] = register.Z;
        }
    }

    [DebuggerDisplay ("{Level},({Register}),{Digit}")]
    public struct T
    {
        public int Level { get; }

        public Registers Register { get; }

        public int Digit { get; }

        public T (int level, Registers register, int digit)
        {
            Level = level;
            Register = register;
            Digit = digit;
        }
    }

    class Program
    {
        private static Dictionary<int, List<T>> _TestedCache = new Dictionary<int, List<T>> ();

        private static (bool isValid, string validString) Explore (string acc, int level, Registers register)
        {
            foreach (var digit in "987654321")
            {
                var t = new T (level, register, digit);
                if (_TestedCache[level].Contains (t))
                {
                    // error
                    continue;
                }
                var aul = new Alu (Input.Problem[level]);
                aul.SetRegister (register);
                var result = aul.Process (digit.ToString ());

                if (result.Error)
                {
                    _TestedCache[level].Add (t);
                    continue;
                }

                if (level == 14)
                {
                    // level is zero indexed, and indicates current level. 0 means the most significant digit
                    // 14 means the last level
                    if (result.r.Z != 0)
                    {
                        continue;
                    }
                    if (result.r.Z == 0)
                    {
                        return (true, acc + digit);
                    }
                }

                var isValid = Explore (acc + digit, level + 1, result.r);
                if (isValid.isValid)
                {
                    return isValid;
                }
                _TestedCache[level].Add (t);
            }
            return (false, "");
        }

        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=nF00Rb3IFJs
            Debug.WriteLine (Part1 ());
            Debug.WriteLine (Part2 ());
        }

        public static long Part1 ()
        {
            _TestedCache = new Dictionary<int, List<T>> ();
            for (int i = 0; i < 15; i++)
            {
                _TestedCache.Add (i, new List<T> ());
            }
            var r = Explore ("", 0, new Registers (0, 0, 0, 0));

            Debug.WriteLine ($"Solution: {r.validString}");
            var aul = new Alu (Input.FullProgram);
            var records = aul.Process (r.validString);
            Assert.AreEqual (0, records.r.Z);

            return long.Parse (r.validString);
        }

        public static int Part2 () =>
            0;
    }
}