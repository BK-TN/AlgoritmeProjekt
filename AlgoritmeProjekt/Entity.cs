using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlgoritmeProjekt
{
    internal abstract class Entity
    {
        private World world;

        public Vector2 Position { get; set; }
        public bool Solid { get; protected set; }
        public World World
        {
            get
            {
                return world;
            }
            set
            {
                if (world == null)
                    world = value;
                else
                    throw new InvalidOperationException("World is already set");
            }
        }

        public Entity()
        {
        }

        public abstract void LoadContent(ContentManager contentManager);

        public virtual void Update(float deltaTime)
        {
        }

        public virtual void Draw(SpriteBatch target)
        {
        }
    }
}
