﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlgoritmeProjekt
{
    internal class Monster : Entity
    {
        private Texture2D sprite;

        public Monster() { }

        public override void LoadContent(ContentManager contentManager)
        {
            //TODO: Load monster sprite
            sprite = contentManager.Load<Texture2D>("monster");
        }

        public override void Draw(SpriteBatch target)
        {
            target.Draw(sprite, new Vector2(Position.X, Position.Y));
        }
    }
}
