using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmeProjekt
{
    internal struct GridPos
    {
        public int X { get; set; }
        public int Y { get; set; }

        public GridPos(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
