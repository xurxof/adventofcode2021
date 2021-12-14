using System.Collections.Generic;
using System.Diagnostics;

namespace AoC11
{
    class Program
    { 

        public static int Part2 (List<string> toInput)
        {
            return 0;

        }

        static void Main (string[] args)
        {
            // Music:https://www.youtube.com/watch?v=a6MU-bwuOvo
            var _Cave = new Cave (Input.Problem);

            Debug.WriteLine (_Cave.Steps (100));
            _Cave = new Cave (Input.Problem);

            Debug.WriteLine (_Cave.StepsUntilAllBright ());
            
        }
    }
}
