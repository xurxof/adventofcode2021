using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Utils
{
    [DebuggerDisplay ("{ToString()}")]
    public class IndexedString
    {
        private readonly string _Input;
        private int _Index;

        public IndexedString (string input)
        {
            _Input = input;
            _Index = 0;
        }

        public bool Any () => _Index < _Input.Length;

        public string Consume (int n)
        {
            var r = _Input.Substring (_Index, n);
            _Index += n;
            return r;
        }

        public int ConsumeInt (int n) => GetInt (Consume (n));

        public static int GetInt (IEnumerable<char> input) => GetInt (string.Join ("", input));

        public static int GetInt (string input) => Convert.ToInt32 (input, 2);

        public static long GetLong (string input) => Convert.ToInt64 (input, 2);

        public string Peek (int n) => _Input.Substring (_Index, n);

        public int PeekInt (int n) => GetInt (Peek (n));

        public override string ToString () => _Input.Substring (_Index);
    }
}