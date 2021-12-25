using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using AoC9;

namespace AoC25
{
    class Program
    {

        public static int Part1 ()
        {
            Cucumbers c = new Cucumbers (Input.Problem);
            var i=c.StepUntil ();
            Debug.WriteLine (i);
            // 503 too low
            return i;
        }


        public static int Part2 ()
        {
            return 0;

        }

        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=nF00Rb3IFJs
            Debug.WriteLine (Part1 ());
            Debug.WriteLine (Part2 ());
        }
    }
}
