using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlgoritmeProjekt
{
    internal class World
    {
        private List<Entity> entities = new List<Entity>();

        public ReadOnlyCollection<Entity> Entities { get { return new ReadOnlyCollection<Entity>(entities); } }

        private ContentManager contentManager;
        private CollisionGrid collisionGrid;

        public CollisionGrid CollisionGrid { get { return collisionGrid; } }
        public int TileSize { get; }

        public World(ContentManager contentManager, int width, int height, int tileSize)
        {
            this.contentManager = contentManager;
            collisionGrid = new CollisionGrid(width, height);
            TileSize = tileSize;
        }

        public void AddEntity(Entity e)
        {
            if (!entities.Contains(e))
            {
                e.LoadContent(contentManager);
                e.World = this;
                entities.Add(e);
            }
        }

        public void Update(float deltaTime)
        {
            collisionGrid.Refresh(this);

            foreach (Entity e in entities)
            {
                e.Update(deltaTime);
            }
        }

        public void Draw(SpriteBatch target)
        {
            foreach (Entity e in entities)
            {
                e.Draw(target);
            }
        }

        public Vector2 GridPosToVector(GridPos pos)
        {
            return new Vector2(pos.X * TileSize, pos.Y * TileSize);
        }

        public Vector2 GridPosToVector(int x, int y)
        {
            return GridPosToVector(new GridPos(x, y));
        }

        public GridPos VectorToGridPos(Vector2 vector)
        {
            return new GridPos((int)vector.X / TileSize, (int)vector.Y / TileSize);
        }

        public bool AreOnSameTile(Entity one, Entity two)
        {
            GridPos onePos = VectorToGridPos(one.Position);
            GridPos twoPos = VectorToGridPos(two.Position);

            return onePos.X == twoPos.X && onePos.Y == twoPos.Y;
        }
    }
}
