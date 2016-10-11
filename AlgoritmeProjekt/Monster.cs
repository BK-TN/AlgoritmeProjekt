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
    internal class Monster : Entity
    {
        private Texture2D sprite;
        private bool visible = false;

        public Monster()
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {
            sprite = contentManager.Load<Texture2D>("monster");
        }

        public override void Draw(SpriteBatch target)
        {
            if (visible)
                target.Draw(sprite, new Vector2(Position.X, Position.Y));
        }

        public override void Update(float deltaTime)
        {
            foreach (Wizard wiz in World.Entities.OfType<Wizard>())
            {
                if (World.AreOnSameTile(this, wiz))
                {
                    //Wizard has now stepped on monster
                    Solid = true;
                    visible = true;
                }
            }
        }
    }
}
