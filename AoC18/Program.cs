using System.Diagnostics;

namespace AoC18
{
    class Program
    {
        static void Main (string[] args)
        {
            // Music: teho
            Debug.WriteLine (Part1 ());
            Debug.WriteLine (Part2 ());
        }

        public static int Part1 ()
        {
            var N = Number.Sum (Input.Problem);

            return N.Magnitude;
        }

        public static int Part2 ()
        {
            var Magnitude = Number.Combination (Input.Problem);
            return Magnitude;
        }
    }
}