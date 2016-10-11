using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace AlgoritmeProjekt
{
    internal class Monster : Entity
    {
        public override void LoadContent(ContentManager contentManager)
        {
            //TODO: Load monster sprite
        }

        public override void Update(float deltaTime)
        {
            foreach (Wizard wiz in World.Entities.OfType<Wizard>())
            {
                int wizX = ((int)(wiz.Position.X / 48)) * 48;
                int wizY = ((int)(wiz.Position.Y / 48)) * 48;

                int myX = ((int)(Position.X / 48)) * 48;
                int myY = ((int)(Position.Y / 48)) * 48;

                if (wizX == myX && wizY == myY)
                {
                    //Wizard has now stepped on monster
                    Solid = true;
                }
            }
        }
    }
}
