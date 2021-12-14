using System.Collections.Generic;
using System.Linq;

namespace AoC4
{
    public class Boards
    {
        private readonly List<Board> _List;

        public Boards (IEnumerable<string> input)
        {
            _List = input.Select (i => new Board (i)).ToList ();
        }

        public (int num, List<Board> win) Drew (int num)
        {
            List<Board> boards = new List<Board> ();
            foreach (var board in _List)
            {
                var previ = board.HasLine;
                board.Mark (num);
                if (board.HasLine!= previ)
                {
                    boards.Add (board);
                }
            }
            return (num, boards);
        }

        public (int num, List<Board> win) Drew (string drawedNumers)
        {
            foreach (var drawedNumer in drawedNumers.Split (',').Select (int.Parse))
            {
                var result = Drew (drawedNumer);
                if (result.win.Any())
                {
                    return result;
                }
            }
            return (-1, new List<Board> ());
        }

        public (int num, List<Board> win) Last (string drawedNumers)
        {
            (int num, List<Board> win) Last = (-1, null);
            foreach (var drawedNumer in drawedNumers.Split (',').Select (int.Parse))
            {
                var result = Drew (drawedNumer);
                var notHasLine = _List.Where (b => !b.HasLine).ToList ();
                if (notHasLine.Count()==0)
                {
                    return result;
                }
                Last = result;
            }
            return Last;
        }
    }
}