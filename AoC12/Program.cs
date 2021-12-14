using System.Collections.Generic;
using System.Diagnostics;
using Utils;

namespace AoC9
{
    class Program
    {

        public static int Part1 (List<string> toInput)
        {
            var _Cave = new Cave (@"pg-CH
pg-yd
yd-start
fe-hv
bi-CH
CH-yd
end-bi
fe-RY
ng-CH
fe-CH
ng-pg
hv-FL
FL-fe
hv-pg
bi-hv
CH-end
hv-ng
yd-ng
pg-fe
start-ng
end-FL
fe-bi
FL-ks
pg-start");
            // asert
            _Cave.Walk ();
             return _Cave.NumRoutes;
        }


        public static int Part2 (List<string> toInput)
        {
            var _Cave = new Cave (@"pg-CH
pg-yd
yd-start
fe-hv
bi-CH
CH-yd
end-bi
fe-RY
ng-CH
fe-CH
ng-pg
hv-FL
FL-fe
hv-pg
bi-hv
CH-end
hv-ng
yd-ng
pg-fe
start-ng
end-FL
fe-bi
FL-ks
pg-start");
            // asert
            _Cave.Walk ();
            return _Cave.NumRoutes;

        }

        static void Main (string[] args)
        {
            // Music: https://www.youtube.com/watch?v=EHnStnU90Ww
            // Debug.WriteLine (Part1 (Input.Test.ToInput ()));
            Debug.WriteLine (Part2 (Input.Test.ToInput ()));
        }
    }
}
