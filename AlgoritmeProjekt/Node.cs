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
        private int g;
        private int h;
        private int f;

        private Node parent;

        //grid position
        private GridPos position;

        public Node(GridPos position, Node parent)
        {
            this.position = position;
            this.parent = parent;
        }

        public Node Parent { get; set; }
        public GridPos Position { get;}
        public int G { get; set; }
        public int H { get; set; }
        public int GetFValue { get { return g + h; }}



    }
}
