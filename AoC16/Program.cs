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
            ulong Version = input.ConsumeInt (3);
            ulong typeId = input.ConsumeInt (3);
            return GetOperation (Version, typeId, input);
        }

        public static Operation GetOperation (ulong version, ulong typeId, IndexedString input)
        {
            ulong LenghtTypeId = input.ConsumeInt (1);
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

        private static Operation GetOperationMode11 (ulong version, ulong typeId, IndexedString input)
        {
            int TypeId = 11;
            ulong NumSubPackets = input.ConsumeInt (11);
            ulong consumed = 6 + 1 + 15;
            List<IPacket> Childs = new List<IPacket> ();
            while (Childs.LongCount () < (long) NumSubPackets)
            {
                IPacket generated = Process (input);
                Childs.Add (generated);
                consumed += generated.RecursiveConsumed;
            }
            return new Operation (version, typeId, TypeId, Childs, consumed);
        }

        private static Operation GetOperationMode15 (ulong version, ulong typeId, IndexedString input)
        {
            int TypeId = 15; // consumed 1
            ulong TotaleLength = input.ConsumeInt (15);
            ulong consumed = 6 + 1 + 15;
            List<IPacket> Childs = new List<IPacket> ();
            while (consumed < TotaleLength + (6 + 1 + 15))
            {
                IPacket generated = Process (input);
                Childs.Add (generated);
                consumed += generated.RecursiveConsumed;
            }
            return new Operation (version, typeId, TypeId, Childs, consumed);
        }

        public static Packet GetPacket (ulong version, ulong typeId, IndexedString input)
        {
            bool last;
            string binaryNumber = "";
            ulong consumed = 6;
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

            var P = new Packet (version, typeId, IndexedString.GetInt (binaryNumber), consumed);
            return P;
        }

        public static Packet GetPacket (IndexedString input)
        {
            ulong Version = input.ConsumeInt (3);
            ulong TypeId = input.ConsumeInt (3);
            return GetPacket (Version, TypeId, input);
        }

        public static Packet GetPacket (string input) => GetPacket (new IndexedString (input));

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

    public class Operation : IPacket
    {
        public Operation (ulong version, ulong typeId, int lenTypeId, List<IPacket> childs, ulong consumed)
        {
            Version = version;
            TypeId = typeId;
            LenTypeId = lenTypeId;
            Childs = childs;
            Consumed = consumed;
        }

        public List<IPacket> Childs { get; }

        public ulong Consumed { get; }

        public int LenTypeId { get; }

        public ulong TypeId { get; }

        public ulong RecursiveConsumed =>
            Consumed + Childs.Select (c => c.RecursiveConsumed)
                .Aggregate ((a, b) => a + b);

        public ulong SumRecursiveVersion =>
            Version + Childs.Select (c => c.SumRecursiveVersion)
                .Aggregate ((a, b) => a + b);

        public ulong Version { get; }
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

        public ulong ConsumeInt (int n) => GetInt (Consume (n));

        public static ulong GetInt (IEnumerable<char> input) => GetInt (string.Join ("", input));

        public static ulong GetInt (string input) => Convert.ToUInt64 (input, 2);

        public string Peek (int n) => _Input.Substring (_Index, n);

        public ulong PeekInt (int n) => GetInt (Peek (n));

        public override string ToString () => _Input.Substring (_Index);
    }

    public interface IPacket
    {
        ulong RecursiveConsumed { get; }

        ulong SumRecursiveVersion { get; }

        ulong Version { get; }
    }

    public class Packet : IPacket
    {
        public Packet (ulong version, ulong id, ulong number, ulong consumed)
        {
            Version = version;
            Id = id;
            Number = number;
            Consumed = consumed;
        }

        public ulong Consumed { get; }

        public ulong Id { get; }

        public ulong Number { get; }

        public ulong SumRecursiveVersion => Version;

        public ulong RecursiveConsumed => Consumed;

        public ulong Version { get; }
    }

    class Program
    {
        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=nF00Rb3IFJs
            Debug.WriteLine (Part1 ());
            Debug.WriteLine (Part2 ());
        }

        public static ulong Part1 ()
        {
            Bits B = new Bits (Input.Problem);
            var P = B.Process ();
            return P.SumRecursiveVersion;
            // 356 too low
        }

        public static int Part2 () => 0;
    }
}