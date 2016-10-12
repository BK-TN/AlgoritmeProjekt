using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AlgoritmeProjekt
{
    enum GridType { Sart, Goal, Solid, Empty}

   class Node
    {
        private int g = 0;
        private int h;
        private int f;

        private Node parent;

        //grid position
        private Point position;

        public Node(Point position, int h, int g)
        {
            this.position = position;
        }

        public Node Parent { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get; }
    }
}
