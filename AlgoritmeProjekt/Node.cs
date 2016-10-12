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

        public Node(GridPos position, Node parent, int g, GridPos goal)
        {
            Position = position;
            this.parent = parent;
            h = (int)Vector2.Distance(new Vector2(goal.X, goal.Y), new Vector2(position.X, position.Y));
            this.g = g;
        }

        public Node Parent { get; set; }
        public GridPos Position { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int GetFValue { get { return g + h; }}

    }
}
