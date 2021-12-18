using NUnit.Framework;

namespace AoC16.Test
{
    [TestFixture]
    class Test
    {
        [Test]
        public void Test_InputBinary ()
        {
            // asert
            Bits B = new Bits ("D2FE28");
            Assert.AreEqual ("110100101111111000101000", B.InputBinary);
        }

        [Test]
        public void Test_GetIntl ()
        {
            // asert

            Assert.AreEqual (6, IndexedString.GetInt ("110"));
        }

        [Test]
        public void Test_GetPacket ()
        {
            // asert
            Literal P = Bits.GetPacket ("110100101111111000101000");
            Assert.AreEqual (6, P.Version);
            Assert.AreEqual (4, P.Id);
            Assert.AreEqual (2021, P.Number);
        }

        [Test]
        public void Test_GetOperation15 ()
        {
            // asert
            Operation O = Bits.GetOperation ("00111000000000000110111101000101001010010001001000000000");
            Assert.AreEqual (1, O.Version);
            Assert.AreEqual (6, O.TypeId);
            Assert.AreEqual (15, O.LenTypeId);
            Assert.AreEqual (49, O.Consumed);
            Assert.AreEqual (2, O.Childs.Count);
            Assert.AreEqual (10, ((Literal) O.Childs[0]).Number);
            Assert.AreEqual (20, ((Literal) O.Childs[1]).Number);

        }


        [Test]
        public void Test_GetOperation11 ()
        {
            // asert
            Operation O = Bits.GetOperation ("11101110000000001101010000001100100000100011000001100000");
            Assert.AreEqual (7, O.Version);
            Assert.AreEqual (3, O.TypeId);
            Assert.AreEqual (11, O.LenTypeId);
            Assert.AreEqual (3, O.Childs.Count);
            Assert.AreEqual (1, ((Literal) O.Childs[0]).Number);
            Assert.AreEqual (2, ((Literal) O.Childs[1]).Number);

            Assert.AreEqual (3, ((Literal) O.Childs[2]).Number);

        }

        [Test]
        public void Test_GetFirstTest ()
        {
            // asert
            Bits B = new Bits ("8A004A801A8002F478");
            IPacket P = B.Process ();
            Assert.AreEqual (16, P.SumRecursiveVersion);  

        }


        [Test]
        public void Test_GetSecondTest ()
        {
            // asert
            Bits B = new Bits ("620080001611562C8802118E34");
            IPacket P = B.Process ();
            Assert.AreEqual (12, P.SumRecursiveVersion);

        }


        [Test]
        public void Test_GetThirdTest ()
        {
            // asert
            Bits B = new Bits ("C0015000016115A2E0802F182340");
            IPacket P = B.Process ();
            Assert.AreEqual (23, P.SumRecursiveVersion);

        }


        [Test]
        public void Test_GetFourthTest ()
        {
            // asert
            Bits B = new Bits ("A0016C880162017C3686B18A3D4780");
            IPacket P = B.Process ();
            Assert.AreEqual (31, P.SumRecursiveVersion);

        }
    }
}