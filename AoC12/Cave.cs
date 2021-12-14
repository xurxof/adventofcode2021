using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace AoC9
{
    public class Cave
    {
        public List<(string s, string e)> _List = new List<(string s, string e)> ();
        private List<string> _Routes = new List<string> ();

        private readonly Dictionary<string, int> _SmallCavesCounter =
            new Dictionary<string, int> ();

        public Cave (string input)
        {
            var SmallCaves = new List<string> ();
            foreach (var line in input.Split (Environment.NewLine).OrderBy (a => a))
            {
                var startend = line.Split ('-');
                _List.Add ((startend[0], startend[1]));

                _List.Add ((startend[1], startend[0]));
                if (startend[0].IsLowercase ())
                {
                    SmallCaves.Add (startend[0]);
                }
                if (startend[1].IsLowercase ())
                {
                    SmallCaves.Add (startend[1]);
                }
            }
            _List = _List.Distinct ().OrderBy (l => l).ToList ();
            SmallCaves.Distinct ()
                .Where (l => l != "start" && l != "end")
                .OrderBy (l => l)
                .ForEach (l => _SmallCavesCounter.Add (l, 0));
        }

        public int NumRoutes { get; private set; }

        //public void Walk ()
        //{
        //    Walk ("start", "");
        //}

        //private void Walk (string startPointName, string route, int allowRevisit)
        //{
        //    route += "," + startPointName;
        //    //var revisited = route.Split (',')
        //    //    .Where (c => _SmallCaves.Contains (c))
        //    //    .GroupBy (c => c)
        //    //    .Where (c => c.Count () >= allowRevisit)
        //    //    .ToList ();

        //    if (_SmallCavesCounter.ContainsKey (startPointName))
        //    {
        //        if (_SmallCavesCounter[startPointName] > allowRevisit)
        //        {
        //            return;
        //        }
        //        if (allowRevisit > 1 &&
        //            _SmallCavesCounter.Values.Count (v => v == allowRevisit) > 1)
        //        {
        //            return;
        //        }
        //    }

        //    if (startPointName == "end")
        //    {
        //        if (!_Routes.Contains (route))
        //        {
        //            NumRoutes++;
        //            _Routes.Add (route);
        //            return;
        //        }
        //        return;
        //    }
        //    // initial
        //    var candidatesegments =
        //        _List.Where (l => l.s == startPointName && l.e != "start").ToList ();
        //    if (allowRevisit != 1)
        //    {
        //        candidatesegments = candidatesegments.Except (_SmallCavesCounter
        //            .Where (kv => kv.Value >= allowRevisit)
        //            .Select (kv => kv.Key)).ToList ();
        //    }
        //    foreach (var segment in candidatesegments)
        //    {
        //        //if (segment.e.ToLowerInvariant () == segment.e && allowRevisit==1 && route.Contains (segment.e))
        //        //{
        //        //    continue;
        //        //}

        //        if (_SmallCavesCounter.ContainsKey (segment.e))
        //            _SmallCavesCounter[segment.e]++;
        //        Walk (segment.e, route, allowRevisit);

        //        if (_SmallCavesCounter.ContainsKey (segment.e))
        //            _SmallCavesCounter[segment.e]--;
        //    }
        //}
        
        private void Walk2 (string startPointName, string route)
        {
            //route += "," + startPointName;
            //var revisited = route.Split (',')
            //    .Where (c => _SmallCaves.Contains (c))
            //    .GroupBy (c => c)
            //    .Where (c => c.Count () >= allowRevisit)
            //    .ToList ();

            //if (_SmallCavesCounter.ContainsKey (startPointName))
            //{
            //    if (_SmallCavesCounter[startPointName] > 2)
            //    {
            //        return;
            //    } 
            //    if(_SmallCavesCounter.Values.Count (v => v == 2) > 1)
            //    {
            //        return;
            //    }
            //}

            if (startPointName == "end")
            {
                //if (!_Routes.Contains (route))
                //{
                    NumRoutes++;
                    //_Routes.Add (route);
                    //if (route.Split (',').Where (c => c.IsLowercase ()).GroupBy (c => c).Any (c => c.Count () > 2))
                    //    return;
                    //return;
                //}
                return;
            }
            // initial
            var candidatesegments =
                _List.Where (l => l.s == startPointName && l.e != "start").ToList ();

            var revisited2times = _SmallCavesCounter.Where (kv => kv.Value >= 2).Select (kv => kv.Key);
            if (revisited2times.Any ())
            {
                var revisited1times = _SmallCavesCounter.Where (kv => kv.Value == 1)
                    .Select (kv => kv.Key);
                candidatesegments = candidatesegments
                    .Where (c => !revisited2times.Contains (c.e))
                    .ToList ();
                candidatesegments = candidatesegments
                    .Where (c => !revisited1times.Contains (c.e))
                    .ToList ();
            }
            foreach (var segment in candidatesegments)
            {
                //if (segment.e.ToLowerInvariant () == segment.e && allowRevisit==1 && route.Contains (segment.e))
                //{
                //    continue;
                //}

                if (_SmallCavesCounter.ContainsKey (segment.e))
                    _SmallCavesCounter[segment.e]++;
                Walk2 (segment.e, route);

                if (_SmallCavesCounter.ContainsKey (segment.e))
                    _SmallCavesCounter[segment.e]--;
            }
        }
        public void Walk ()
        {
             
            Walk2 ("start", "");
            
            _Routes = _Routes.OrderBy (a => a).ToList ();
        }
    }
}