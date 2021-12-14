using System.Collections.Generic;
using System.Linq;
using Utils;

namespace AoC6
{
    public class Pool
    {
        // private readonly List<Fish> _Pool = new List<Fish> ();
        private Dictionary<int, long> _Dict = new Dictionary<int, long> ();

        public Pool (string s)
        {
            var inputInts = s.Split (',').Select (int.Parse);
            Enumerable.Range (0, 9).ForEach (i => _Dict.Add (i, inputInts.Count (item => item == i)));
        }

        public long AddDay ()
        {
            var newDict = new Dictionary<int, long> ();
            Enumerable.Range (0, 9).ForEach (i => newDict.Add (i, 0));
            int newFishes = 0;
            foreach (var key in _Dict.Keys)
            {
                if (key == 0)
                {
                    newDict[8] += _Dict[key];
                    newDict[6] += _Dict[key];
                }
                else
                {
                    newDict[key - 1] += _Dict[key];
                }
            }
            _Dict = newDict;
            return Count ();
        }

        public long AddDays (int numDays)
        {
            while (numDays > 0)
            {
                AddDay ();
                numDays--;
            }
            return Count ();
        }

        public long Count () =>
            _Dict.Values.Sum ();
    }
}