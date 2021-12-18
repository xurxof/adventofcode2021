using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC16
{
    public class Bits
    {
        public Bits (string input)
        {
            // as in https://stackoverflow.com/questions/6617284/c-sharp-how-convert-large-hex-string-to-binary
            InputBinary = string.Join (string.Empty,
                input.Select (c => Convert.ToString (Convert.ToInt32 (c.ToString (), 16), 2)
                    .PadLeft (4, '0')));
        }

        public string InputBinary { get; set; }

        public static Operation GetOperation (string input) => GetOperation (new IndexedString (input));

        public static Operation GetOperation (IndexedString input)
        {
            int Version = input.ConsumeInt (3);
            int typeId = input.ConsumeInt (3);
            return GetOperation (Version, typeId, input);
        }

        public static Operation GetOperation (int version, int typeId, IndexedString input)
        {
            int LenghtTypeId = input.ConsumeInt (1);
            if (LenghtTypeId == 0)
            {
                return GetOperationMode15 (version, typeId, input);
            }

            if (LenghtTypeId == 1)
            {
                return GetOperationMode11 (version, typeId, input);
            }
            throw new InvalidOperationException ();
        }

        private static Operation GetOperationMode11 (int version, int typeId, IndexedString input)
        {
            int TypeId = 11;
            int NumSubPackets = input.ConsumeInt (11);
            int consumed = 6 + 1 + 15;
            List<IPacket> Childs = new List<IPacket> ();
            while (Childs.Count < NumSubPackets)
            {
                IPacket generated = Process (input);
                Childs.Add (generated);
                consumed += generated.RecursiveConsumed;
            }
            return new Operation (version, typeId, TypeId, Childs, consumed);
        }

        private static Operation GetOperationMode15 (int version, int typeId, IndexedString input)
        {
            int TypeId = 15; // consumed 1
            int TotaleLength = input.ConsumeInt (15);
            int consumed = 6 + 1 + 15;
            List<IPacket> Childs = new List<IPacket> ();
            while (consumed < TotaleLength + (6 + 1 + 15))
            {
                IPacket generated = Process (input);
                Childs.Add (generated);
                consumed += generated.RecursiveConsumed;
            }
            return new Operation (version, typeId, TypeId, Childs, consumed);
        }

        public static Literal GetPacket (int version, int typeId, IndexedString input)
        {
            bool last;
            string binaryNumber = "";
            int consumed = 6;
            do
            {
                var firstChar = input.Consume (1);

                binaryNumber += input.Consume (4);
                last = firstChar == "0";
                consumed += 5;
            } while (!last);
            // calculamos 0 sobrantes hasta 

            //var padding = 4 - (consumed % 4);
            //if (padding != 4)
            //{
            //    input.Consume (padding);
            //    consumed += padding;
            //}

            var P = new Literal (version, typeId, IndexedString.GetLong (binaryNumber), consumed);
            return P;
        }

        public static Literal GetPacket (IndexedString input)
        {
            int Version = input.ConsumeInt (3);
            int TypeId = input.ConsumeInt (3);
            return GetPacket (Version, TypeId, input);
        }

        public static Literal GetPacket (string input) => GetPacket (new IndexedString (input));

        public IPacket Process () => Process (new IndexedString (InputBinary));

        private static IPacket Process (IndexedString input)
        {
            var Version = input.ConsumeInt (3);
            var TypeId = input.ConsumeInt (3);
            if (TypeId == 4)
            {
                // literal value
                return GetPacket (Version, TypeId, input);
            }
            return GetOperation (Version, TypeId, input);
        }
    }

    [DebuggerDisplay ("{ToString()}")]
    public class Operation : IPacket
    {
        public override string ToString () => $"Operation V: {Version} RV: {SumRecursiveVersion}";

        public Operation (int version, int typeId, int lenTypeId, List<IPacket> childs, int consumed)
        {
            Version = version;
            TypeId = typeId;
            LenTypeId = lenTypeId;
            Childs = childs;
            Consumed = consumed;
        }

        public List<IPacket> Childs { get; }

        public int Consumed { get; }

        public int LenTypeId { get; }

        public int TypeId { get; }

        public int RecursiveConsumed => Consumed + Childs.Sum (c => c.RecursiveConsumed);

        public int SumRecursiveVersion => Version + Childs.Sum (c => c.SumRecursiveVersion);

        public int Version { get; }
    }

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

    public interface IPacket
    {
        int RecursiveConsumed { get; }

        int SumRecursiveVersion { get; }

        int Version { get; }
    }

    [DebuggerDisplay ("{ToString()}")]
    public class Literal : IPacket
    {
        public override string ToString () => $"Literal V: {Version}";

        public Literal (int version, int id, long number, int consumed)
        {
            Version = version;
            Id = id;
            Number = number;
            Consumed = consumed;
        }

        public int Consumed { get; }

        public int Id { get; }

        public long Number { get; }

        public int RecursiveConsumed => Consumed;

        public int SumRecursiveVersion => Version;

        public int Version { get; }
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
            Bits B = new Bits (Input.Problem);
            var P = B.Process ();
            return P.SumRecursiveVersion;
            // 356 too low
        }

        public static int Part2 () => 0;
    }
}