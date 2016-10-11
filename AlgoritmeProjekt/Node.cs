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
        private int h = 0;
        private int f;

        private Node parent;

        //grid position
        private Point pos;

        public Node(Point pos)
        {
            this.pos = pos;
        }

        public Node Parent { get; set; }
        public int G { get; set; }
        
    }
}
