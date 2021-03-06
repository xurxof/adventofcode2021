using System;
using System.Collections.Generic;
using System.Text;

namespace AoC20
{
    public static class Input
    {
        public static string Test = @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###";


        public static string Problem = @"#.#.####.####.#.#.#.....##.#..#####...##..#.#..#.####.#.#...#.#.#.#...#########.#.....#.#...##.##.#.####...##.#..##..##.###.##...#.#.#.##.##.#.#.#..#..#..#..##..##..##..#.#...#.#...#..#....#....#.##.##..###.....####.#.####...########.#.##.#.#.#.....#..##..##..###....#.###..###......#..#####..##..#..#.##..#..#..##.#.###.#.....#....#..####..####....#..##.#..####.#...##.###..#.....###..#..#..##...#####.#.....#..##..##..####.###.#.##..####.###.##...##..#...###.####...###....###.#..#.#.####.#...##.......##...#..

.#..##....##.....##....##...##.###.####..##.#.#.#.#....#.#..#..###.#....##..##..#######..#.##.#.##.#
......#.#.##...#.#..#..##.##.###..#.#.#....#..#...#..##...#####.####..####.###...##...##.##....###..
#.##.#..#.##.#.#.###.#.....##..##.#...##....#......#....#.#.###.....#.###..#.#..##...##..##.#.##....
......##.#.#.#.......#####.#....##.##...#.##.##....#..##..##..##.#..#..##..###..#.#.#.#...##..#..#..
.#.....####..##..#.##.....####....#.....##..#.#.#.#...#...##.#.##.#.#..###...##..#.#.##......#..#.##
...#..#..##...#.#...#....##..####..##.###.###..####.##.##..##.###..#.#.##.#......#.#.#.#..####..#...
....#...#...###.##.####.#....#..##.#.##..####..##.....#......###...#.####...#####.#.#......####.#...
....#.....######.....##.##...#.#.#.##..#...####...##...##...#.#.#...###..###.##..#.#.#....#.##...###
.##.######.##.##.######.#....###.#...###.###.#.#..#..##..###.#..#..#.#.######.##.##...#.#.####.#.###
..#..###..#..####.#.#.#...###...####....#....###.#.#.###.##.#.######.##..#.#....##.#.#.....##.####..
.###.###.......#.##.#....#..##.##.##.....##.#..##.#..##.##..#...###...#..#...##.#..#.#.####..##.....
..##.###.##........##.#.##...##.####..##.#...####..###..#..##########.#...###.#...##.##.#...#...##.#
##........#..#####..#.####...##..#..###...#.##.###......#..###.#.#.....###.#.....##...####...#....#.
#.###..#####.#.#..##...###..######...###..##..#.##..##.##.##...##.###..###..##..#.###....##....#..#.
...##.#.###..#..######..#..#..##.#...#...####.####.#...####..#.###..###.##.#####.....##.###.#.###...
..#...#....###....#.#....##.#.##.#####.###.#....##.##.##.#.#.##.####.#..##.#..#.##.##..##...####..##
#.######.##....#..####....##...#####.##...#.##..#.#..##.....###.##...##.######.#..#..#.####.#.##.#.#
#.######....##.....#..##....####.#.#...#..#...#.#.#.#.#.#..#.#..###.....#..###.##.....#######..###.#
..##.#..##..###.#.###...#.......#.###..###.#.###.#######.#..###..#..##.#.#...####..#..#.....#..####.
##.#.#.#....#.....#....#.#.##.#..##.####......####....#..##...#..####.#####.###.#....#####..###.#.#.
.#.##...#...#.##..##.###...###.##.#...#.#.#....###.#..#...#.#..###..#.#.#.#.###..####..########.#.#.
....###.#.#.#.....#..##.#.#.#.#..######.#.#.##..#.###.####.#..#.#######.##.#...#.##.####.#.#..#....#
##.#.##...########.#.#.#...#.#.#.#..##..#...##.#...####..#...#.##...##...#..#...#.###....#.#.#.#.#..
#.......#..###.###..###.##.###..###..#......##.##.#...#####...#...##.#..##.#.###..#.###.####.###.##.
..#....##.....##..#....#..##.#.#..#.##.#.##.##.##########.....####..##.###..#..#.#....##...#..#....#
..##.#..##..#.#####...####..#.##.###.....####..##..##...####.#..#.##..#...#...#.....#..###..##...##.
....#..###...#...#.###.#..#..##.####..#..#.###.....##...#########.#...###..#.#.#.....##..###...#..#.
###.#.##.#..#.###.#.#..###.##..###...###....#.#..#.............#..#.##.####.....###.###.###....#....
..#.###.#....###..#.###.##.#..#.###..##.#...##...#.#.###..#.#.##....#.##.#.#..#..#......###.##.##.#.
##..####..#.#.#...##....#.##..#.......##...#.......###.####..####.#..####.#...###...#...#........###
#.#...###.#.#.###....#.#..##.##....##.#..#.#..#.##.#.#####..###..#...#..######....##.##....###.###..
#.#.#..####..#.##..##.##.######.####.#.##...##..##..####...##.#.###...##.###...#.##...##...####.#..#
.##.#..##..##...#.##.#.####.....#.#..#.#####.###..###.#.#..##....#..#..###.#.....##....###.#########
#####.##.#.########.#..###.###.#.#.##..#.###.#..#...#.#.#.#.##..#....###.#...###..#.#..##...##.#....
.#.#..#.#######.###..##...##..#..###.#..#####.#..#...#####...#.......#..#.######..##.....##.#.#...##
#.....#..###.##.#.###.#.........#.#.#.#####.##.###########.#...###.#..#..#..#.##...#....#..#....#.#.
#.###...#.#..#.##.###...####.##.##.##..#..#.####.##.#####.....###...##.###.#..#..#..#.##.####.####..
##...##..#..#######..#####......#..#..##..#..#..#.###.##.##.....#####.#.###..###..#..#.#######...#.#
.#...###...#..##.......#####.###.#...##...##.##..#.##.####.......#.#...#...#..###.##.....##..#..#..#
#..#.##..#..##...#.#######....#...#.###.#.##...##..#..#.#.#.#...#.###.###.##......#..#.####.#..#####
........##...###..#.#...###.###.###.#..#..##..#.##..##..##...#...#.##.#.#.####.....####....##.##...#
#.##.###.##.#.#.#.#.#..#......###.######.#..#..#.#.#...#.#.##..#....##.##.##.#.#....#.......##..#.#.
#####.#####.#.#..#.#......##.####...##..##.#.#.#..#....####..######.###..##.##...##.######.##....#..
.##.#..###...#########...##....###.##.#####....###.##.####..##..#.....#.#......##....###.#####..##.#
.#...##.##.#..###..###......####.###........###.#.####..#..####..#.#...######.##.####..#.###.#......
#.#.##.##.####.#.##.#..##...####..#.#.##.#..#.#.....##.......#####..#####......#.......#....##.##.##
####.#.#.##...#.####.##.#...#.###..##.##....###.#####..#######..##.##.##....###.######..#......#.###
#.##...##.#.##.####.####.##.#..#########...##.#..##...#.#.#.####....####.#..#....###...##..####.#...
..###..###..###...#..##...##.#.#.#..##....#.###.#####.........#.#....#.#####.##....#.##...#..##..#.#
.#.#.#.#..#...#..##.#...######.##.###....#..#...##.#..#.##.#.#.#..#######..##.#..#....##..#....##.#.
##.#.###..##...#..##.##...###.##.#.##....#..#...#..##...##..####.....###.##.##.....#..##..##..##.##.
##.##..#.#..#..#####..#.#...#....#.##..#...########.#######..#.#.###.#...#..####.#..##.#.###.##..#..
#.#.#...#.#.###...##.###.##.#..#..#..#..#.##.###.#..#######...#..#.##.#...#.##.#...##.#.##..#..#.###
#.#.#####.###...#..#..########..##..#.####.###...#.#.#..#..#.#...#.....##.##.#####.#..####..####...#
.#..##....##.#.##.#.....#..#..####.#.##.###.....#.#..##...#..####..#####...#.....#.##..###.##.###.#.
####..#..###..###.#.####..####..#.##.###..#.###...####.#....######...#.#...#.#...####...##.####.#.##
.###..######......#...##.###.###.###..##.......#..###..##.##.##....#..#..#.##.#.##..#..#..#..#..#.#.
##..##.##.####.####.#..#.....#.#......##.#####.#...##...####.#####.###.#...###.###..##.###..#..##.#.
.#..###.....##.###.#....##.##.....#..#.#....#.####..####.###..#..##..###.....##.##.#.######.###.#..#
#.....##....#..#.#.#..#..###...#..#..#..####.##.#..#.##....##.#.###..#..#.....#.....#.##...##.....##
###...#.#...#.#..#####.###.#.........#.###.....#....#####....#.#....##.#.#.##...#...#.#...#.##....##
..##..#..#.#####.##.##.#######...#.#####.##..##.....#####.####.#.####..#.#.#..#...#.#.#.#....#.#....
###.#..##.####....#..#.##.#.####..#..##...#..#..##.#....#...#####..##...#.#######.#.####......######
.#..##.....##..#..#.##..#.#########..##.#######...#.##.#.##...#.###..##..#..#.###.#..#...##.#.#.###.
.##...######...####.#.#####.#.#..##.#...##.#...####..####...#..###...###.....#.##.#.#....###..##....
#.##...#..#..#.#.#.#....#....####.#.#.#.####..####...#.#.##..#....##.....#..#....###.###.#.#####....
#.#.##.##..#.#...#....##..#...#.#.##..#.#....##...#..##.######.....#..###.#####.##..#.#.####.#...#.#
#.###..##.#.#..#.#..#.###.#....#..##...#.#.#..#.#..#####.##.##.....#.#.###..######...#.#.##...###..#
.##.#...###...#...##.####..###.....####.#.####..##.##..#.###.#..####...#.###.#.##...#.#..##..#...#..
.###.#.#..#....##.##..##..###.####.##..##...#####.#.#...##.#..#...#...##.###...##..#......#...#.####
.......##.#..#.#.###.####..#.#.##.########..#.###..#.#......#....##..#.###..#..#....##..##..#......#
#.#..#...####.##..#..#.#..###.......##..#..#....#.#....#.#.#..##.##..#....#...#####.####.##.##...#.#
..#.....#####..#...###.####.##.#..##.#..#..########.#..#..##.###.#.##..#.######....##....##.###.#.##
#.#.#.#..#..#.#.#.#.#...#####.##.#.#.####..##..###.###.##.#.#..###..#.#.#....#...##.#...###...#.#.#.
#.#.#..#.#.#..###.#.#....#.....#.#.###.......##...##....#....##.#.####.#.###...#.##.#..####.#...###.
#...##.#####.##.#..#..###..###.#.#...##..##...#...#...##..##...#......#...#..##.#.##.....#...#..#.##
#..##.#...#..#.#.####...##.######...#..##.##.##...#####.....#.#.#.#.##.####....#.#...####.##.#.##...
##..#.##..#.####.#.####..#..##.###.....##.#...##.#..#.#..#.#...#...#..#..###.##..##..#.###..###.##.#
.##.#..##.#########.##...........##..#..##...##.#.....##.###...###.####.#.#.##...##....##.#.#.#.#.##
#...#...###.###....#####....#.#...##.####.##..#.###..#.#.#..##...###.##..####..###.###.##......#####
.#..#.##.##.##...#.##...#.....#.##.##.#.#.#..#...#.#.##....###.###.#.#.#.##.#..##########....####.#.
..#..#.###..###.#..#.#.#..#.#.###....####.#####.##.###..#..####...#......#####..###........#..##...#
#.#.#.#.##..######.....##..#.##.#####...##......#.#.#..####.#..##.#####.....#.##.##..####.##.#.##...
...##...#..##...##...##..#....#..#####.#####.#.##.#......##.##...####.#####..##...#...#..####..##..#
.##.#.###########...###...##..#....#...#...###.##.###...######..##.....#..#.######.#####.#..#.#.####
..#....##.......##.##.....##...##....#.###.#..#...##..#...#....###.##.##..###.####..###..##.##..##.#
###....#.#.....#...#####.#.#....#.##..##########.#.######....###..###.##.#####.####..###.#.#.#.###.#
#.##.##..#..#####.....#..#.####.###.#...##..#.....###..#...######.##..##.###.##..#####....##.#####..
#...#..###.#....##..##.###.###..#.#...##..#.###....######..#..###.#..###.##....#.#..#...#.#..###.#.#
#..##..#.#.....#.....###.##.#...#...###..#.#####..##......#..#.##..###.#..##.###...####...##..#....#
###...######..#.#...#...#..#..#..####.##.##.##.##.##..##.###.#.#.##...#.#..#..#...####...#.###.#.###
..#.#.##..#.###.....#...#..##..##...#..#.....####...#.#..........##.....###.#..##.#.##...##.#..#..#.
#.##....###.#.##...###.#...#.#.#..#.###.#####.#######..#....##..##.####.##..#..#..#####...####..#.##
#...#.####..##.###.#...##......#.####...#..##.....#.##..##.#.##...#...##.#.##.#.##.......##..###.###
....###.##..#.#..###..#....#..#.#.#####...#....#.##...##..#.#..#..########.#.#...##....#.#.#..##.##.
..#......#.#.#.#.#...#.#.####..##.#####.##.......###....#.#.#.#....#....#######...#.#...#....#.##..#
.##.#.#...#...##...#...########.##.#.#.#.##...#..#.##..##.#.###..##..#####.#.#.....#...#..###.....#.
#...#....####..#..#####..#.##.....#.#.#...######......#.#.###...#.####.#......##...####.#.#.##.###.#
.###.....###...##.###..#.####.###.#...###.#....#..#..#.#.##.##....###.##..##..#....#####..###..##.##
#...#.##.##.#...#.....#..####..#.#..#.#.#.###..#.......###...#.....#...##.##...#.#####.#.#....#...##";
    }
}
