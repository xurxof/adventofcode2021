using System.Diagnostics;
using AoC20;

namespace AoC9
{

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
            Scanner S = new Scanner (Input.Problem, 10);

            S.EnhalceImage (2);

            // asert
            return S.LightPixels;
            
        }

        public static int Part2 ()
        {
            Scanner S = new Scanner (Input.Problem, 1000);

            S.EnhalceImage (50);
            // 7073 too low - 
            // 91717 too hight
            // asert
            return S.LightPixels;
        }
    }
}