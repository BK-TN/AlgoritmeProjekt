﻿using System;
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

        public World(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public void AddEntity(Entity e)
        {
            if (!entities.Contains(e))
            {
                e.LoadContent(contentManager);
                entities.Add(e);
            }
        }

        public void Update(float deltaTime)
        {
        }

        public void Draw(SpriteBatch target)
        {
        }
    }
}