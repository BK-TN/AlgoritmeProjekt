using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace AlgoritmeProjekt
{
    internal class Wall : Entity
    {
        public Wall(int x, int y) : base(x, y)
        {
            Solid = true;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //TODO: Load wall sprite
        }
    }
}
