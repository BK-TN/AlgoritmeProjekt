using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace AlgoritmeProjekt
{
    internal enum TowerType
    {
        StormTower,
        IceTower
    }

    internal class Tower : Entity
    {
        private TowerType Type { get; }

        public Tower(int x, int y, TowerType type) : base(x, y)
        {
            Type = type;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //TODO: Load tower sprite based on tower type
        }
    }
}
