using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Timers;

namespace AlgoritmeProjekt
{
    class Wizard : Entity
    {
        int i;
        
        List<Point> path;
        public Wizard(int x, int y) : base(x, y)
        {
            i = -1;
            path = new List<Point>();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public void WizardPath(List<Point> list)
        {
            path = list;

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            foreach( Point tal in path)
            {
                while(tal.X < this.X)
                {
                    this.X--;
                }
                while(tal.X > this.X)
                {
                    this.X++;
                }
                while (tal.Y < this.Y)
                {
                    this.Y--;
                }
                while (tal.Y > this.Y)
                {
                    this.Y++;
                }

            }
            
        }

    }
}
