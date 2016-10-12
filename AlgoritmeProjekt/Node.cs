using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AlgoritmeProjekt
{
    internal class Node
    {
        public Node(GridPos position, Node parent, int g, GridPos goal)
        {
            Position = position;
            Parent = parent;
            G = g;
            H = (int)Vector2.Distance(new Vector2(goal.X, goal.Y), new Vector2(position.X, position.Y));
        }

        public Node Parent { get; set; }
        public GridPos Position { get; set; }
        public int G { get; set; }
        public int H { get; }
        public int F { get { return G + H; } }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
