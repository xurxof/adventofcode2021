using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2
{
    class Program
    {
        private static int ExecuteMovements (IEnumerable<(string action, int value)> movements)
        {
            int depth = 0;
            int pos = 0;
            var commandTrans = new Dictionary<string, Action<int>>
            {
                {
                    "up", v => depth -= v
                },
                {
                    "down", v => depth += v
                },
                {
                    "forward", v => pos += v
                }
            };
            foreach (var move in movements)
            {
                commandTrans[move.action] (move.value);
            }
            return depth * pos;
        }


        private static int ExecuteMovements2 (IEnumerable<(string action, int value)> movements)
        {
            int depth = 0;
            int pos = 0;
            int aim = 0;
            var commandTrans = new Dictionary<string, Action<int>>
            {
                {
                    "up", v => aim -= v
                },
                {
                    "down", v => aim += v
                },
                {
                    "forward", v =>
                    {
                        pos += v;
                        depth += (aim * v);
                    }
                }
            };
            foreach (var move in movements)
            {
                commandTrans[move.action] (move.value);
            }
            return depth * pos;
        }


        static void Main (string[] args)
        {
            var movements = Prepare (Input.P1_Test);
            var r = ExecuteMovements (movements);
            movements = Prepare (Input.P1_Input);
            r = ExecuteMovements (movements);
            //
            movements = Prepare (Input.P1_Test);
            r = ExecuteMovements2 (movements);
            movements = Prepare (Input.P1_Input);
            r = ExecuteMovements2 (movements);
        }

        private static IEnumerable<(string action, int value)> Prepare (string input) =>
            input.Split (Environment.NewLine).Select (s => s.Split (' ')).Select (s => (s[0], int.Parse (s[1])));
    }
}