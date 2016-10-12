using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmeProjekt
{
    internal class CollisionGrid
    {
        private bool[,] tiles;

        public int Width { get { return tiles.GetLength(0); } }
        public int Height { get { return tiles.GetLength(1); } }

        public CollisionGrid(int width, int height)
        {
            tiles = new bool[width, height];
        }

        public bool GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public void Refresh(World world)
        {
            bool[,] tiles = new bool[Width, Height];
            foreach (Entity e in world.Entities)
            {
                if (e.Solid)
                {
                    GridPos pos = world.VectorToGridPos(e.Position);
                    tiles[pos.X, pos.Y] = true;
                }
            }
            this.tiles = tiles;
        }
    }
}
