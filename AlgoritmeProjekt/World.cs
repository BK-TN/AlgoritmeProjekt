using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public World(ContentManager contentManager, int width, int height)
        {
            this.contentManager = contentManager;
            collisionGrid = new CollisionGrid(width, height);
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
    }
}
