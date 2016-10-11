using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AlgoritmeProjekt
{
    internal abstract class Pathfinder
    {
        protected CollisionGrid collisionGrid;

        public Pathfinder(CollisionGrid collisionGrid)
        {
            this.collisionGrid = collisionGrid;
        }

        public abstract Vector2[] FindPath(GridPos start, GridPos goal);
    }
}
