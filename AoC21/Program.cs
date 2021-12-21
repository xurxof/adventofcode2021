using System.Diagnostics;
using NUnit.Framework;

namespace AoC21
{
    public class LimitedInt
    {
        private readonly int _End;
        private int _Val;

        public LimitedInt (int startValue, int end)
        {
            Assert.IsTrue (startValue <= end);
            _Val = startValue;
            _End = end;
        }

        public int Val => _Val;

        public int GetNext ()
        {
            _Val++;
            if (_Val > _End)
            {
                _Val = 1;
            }
            return _Val;
        }

        public int Increment (int i)
        {
            _Val += i;
            while (_Val > _End)
            {
                _Val -= _End;
            }
            return _Val;
        }
    }

    public class Board
    {
        private readonly LimitedInt _Dice;
        private readonly LimitedInt _PlayerIdentifier;
        private readonly LimitedInt _Player1Position;
        private readonly LimitedInt _Player2Position;
        private readonly int[] _Points = { 0, 0 };
        private readonly int[] _Turns = { 0, 0 };

        public Board (int player1Position, int player2Position)
        {
            _PlayerIdentifier = new LimitedInt (0, 2);
            _Dice = new LimitedInt (0, 100);
            _Player1Position = new LimitedInt (player1Position, 10);
            _Player2Position = new LimitedInt (player2Position, 10);
        }

        public (int Position, int Points, int Turns) Player1 => (_Player1Position.Val, _Points[0], _Turns[0]);

        public (int Position, int Points, int Turns) Player2 => (_Player2Position.Val, _Points[1], _Turns[1]);

        public void Play (int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                var CurrentPlayerPosition = _PlayerIdentifier.GetNext () == 1 ? _Player1Position : _Player2Position;
                CurrentPlayerPosition.Increment (_Dice.GetNext () + _Dice.GetNext () + _Dice.GetNext ());
                _Turns[_PlayerIdentifier.Val - 1] +=3;
                _Points[_PlayerIdentifier.Val - 1] += CurrentPlayerPosition.Val;
            }
        }

        public int PlayAndGetLoserScore ()
        {
            while (_Points[0] < 1000 && _Points[1] < 1000)
            {
                Play(1);
            }
            var winerIdentifier = _PlayerIdentifier.Val;
            var Loser = winerIdentifier == 1 ? Player2 : Player1;
            return (Player1.Turns + Player2.Turns) * Loser.Points;
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
            // asert
            return new Board (6, 9).PlayAndGetLoserScore ();
        }

        public static int Part2 () => 0;
    }
}