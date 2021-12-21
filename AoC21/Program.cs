using System.Diagnostics;
using NUnit.Framework;

namespace AoC21
{
    [DebuggerDisplay("{Val}")]
    [DebuggerStepThrough]
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

        public LimitedInt (int startValue, int end, int intiValue) : this (startValue, end) => _Val = intiValue;

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

    public class BoardDirac
    {
        // static values to store wins
        // maybe a bit nasty, but more legible than returns winners and sum
        public static ulong _Wins1;
        public static ulong _Wins2;
        public static ulong _Universes;
        private readonly LimitedInt _Dice;
        private readonly int[] _DiceThrows ;
        private readonly LimitedInt _Player1Position;
        private readonly LimitedInt _Player2Position;
        private readonly LimitedInt _PlayerIdentifier;
        private readonly int[] _Points;
        private readonly int[] _Turns ;
        private readonly int _NumThrows;

        [DebuggerStepThrough]
        public BoardDirac (int player1Position, int player2Position)
        {
            
            _DiceThrows = new[] { 0, 0, 0 };
            _Points = new[] { 0, 0 };
            _Turns = new[] { 0, 0 };
            _PlayerIdentifier = new LimitedInt (0, 2);
            _Dice = new LimitedInt (0, 100);
            _Player1Position = new LimitedInt (player1Position, 10);
            _Player2Position = new LimitedInt (player2Position, 10);
        }

        public BoardDirac (BoardDirac parent, int diceValue)
        {
            _Universes++;
            _PlayerIdentifier = parent._PlayerIdentifier;
            _Player1Position = parent._Player1Position;
            _Player2Position = parent._Player2Position;
            _Points = (int[])parent._Points.Clone ();
            _Turns = (int[]) parent._Turns.Clone ();
            _DiceThrows = (int[]) parent._DiceThrows.Clone ();
            _Dice = new LimitedInt (1, 100, diceValue);

            //

            _NumThrows = parent._NumThrows + 1;
            _DiceThrows[_NumThrows - 1] = diceValue;
            if (_NumThrows == 3)
            {
                var CurrentPlayerPosition = _PlayerIdentifier.GetNext () == 1 ? _Player1Position : _Player2Position;
                CurrentPlayerPosition.Increment (_DiceThrows[0] + _DiceThrows[1] + _DiceThrows[2]);
                _Turns[_PlayerIdentifier.Val - 1] += 3;
                _Points[_PlayerIdentifier.Val - 1] += CurrentPlayerPosition.Val;
                _NumThrows = 0;
                _DiceThrows = new[] { 0, 0, 0 };
                // check if wins
                if (CheckIfWins ())
                {
                    return;
                }
                //
            }
            PlayOnce ();
            
        }

        public BoardDirac Child1 { get; set; }

        public BoardDirac Child2 { get; set; }

        public BoardDirac Child3 { get; set; }

        public ulong Wins1 => _Wins1;

        public ulong Wins2 => _Wins2;

        private bool CheckIfWins ()
        {
            if (_Points[0] >= 6)
            {
                _Wins1++;
                return true;
            }
            if (_Points[1] >= 6)
            {
                _Wins2++;
                return true;
            }

            return false;
        }

        public void PlayOnce ()
        {
            // cada vez que se tira el dado se genera un nuevo estado con tres tiradas de dado
            Child1 = new BoardDirac (this, _Dice.GetNext ());
            Child2 = new BoardDirac (this, _Dice.GetNext ());
            Child3 = new BoardDirac (this, _Dice.GetNext ());
        }
    }

    public class Board
    {
        private readonly LimitedInt _Dice;
        private readonly LimitedInt _Player1Position;
        private readonly LimitedInt _Player2Position;
        private readonly LimitedInt _PlayerIdentifier;
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
                _Turns[_PlayerIdentifier.Val - 1] += 3;
                _Points[_PlayerIdentifier.Val - 1] += CurrentPlayerPosition.Val;
            }
        }

        public int PlayAndGetLoserScore ()
        {
            while (_Points[0] < 1000 && _Points[1] < 1000)
            {
                Play (1);
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

        public static int Part1 () =>
            // asert
            new Board (6, 9).PlayAndGetLoserScore ();

        public static int Part2 () => 0;
    }
}