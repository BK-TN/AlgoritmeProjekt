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
    internal class Wall : Entity
    {
        private Texture2D sprite;

        public Wall(int x, int y) : base(x, y)
        {
            Solid = true;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            sprite = contentManager.Load<Texture2D>("Wall");
        }

        public override void Draw(SpriteBatch target)
        {
            target.Draw(sprite, new Vector2(X * 48, Y * 48));
        }
    }
}
