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
    internal enum TowerType
    {
        StormTower,
        IceTower
    }

    internal class Tower : Entity
    {
        private Texture2D sprite;
        private TowerType Type { get; }

        public Tower(TowerType type)
        {
            Type = type;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //TODO: Load tower sprite based on tower type
            switch (Type)
            {
                case TowerType.StormTower:
                    sprite = contentManager.Load<Texture2D>("tower1");
                    break;
                case TowerType.IceTower:
                    sprite = contentManager.Load<Texture2D>("tower2");
                    break;
            }
        }

        public override void Draw(SpriteBatch target)
        {
            target.Draw(sprite, new Vector2(Position.X, Position.Y));
        }
    }
}
