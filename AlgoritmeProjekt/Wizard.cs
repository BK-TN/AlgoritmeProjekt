using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;

namespace AlgoritmeProjekt
{
    internal class Wizard : Entity
    {
        float X;
        float Y;
        int CurrentPath;
        private Texture2D sprite;
        List<Vector2> path;
        public Wizard() : base()
        {
            CurrentPath =0;

            path = new List<Vector2>();
            path.Add(new Vector2(150, 10));
            path.Add(new Vector2(100, 100));
            path.Add(new Vector2(250, 100));
            path.Add(new Vector2(10, 100));
            path.Add(new Vector2(10, 10));
            
        }


        public override void LoadContent(ContentManager contentManager)
        {
            sprite = contentManager.Load<Texture2D>("wizard");
        }

        public void WizardPath(List<Vector2> list)
        {
            CurrentPath = 0;
            path = list;

        }
        public void goToPoint(Vector2 tal, float deltaTime)
        {
            
            if (tal.X < X)
            {
                X-= 35*deltaTime;
                this.Position = new Vector2(X, Y);
            }
            if (tal.X > X)
            {
                X += 35*deltaTime;
                this.Position = new Vector2(X, Y);
            }
            if (tal.Y < Y)
            {
                Y-= 35*deltaTime ;
                this.Position = new Vector2(X, Y);
            }
            if (tal.Y > Y)
            {
                Y += 35*deltaTime;
                
                this.Position = new Vector2(X, Y);
            }
            

            if (Vector2.Distance(tal, new Vector2(X,Y)) <= 1 && CurrentPath != path.Count - 1)
            {
            this.Position = new Vector2(tal.X, tal.Y);
            CurrentPath++;
            }
               

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        
            goToPoint(path.ElementAt(CurrentPath), deltaTime);
                       
            this.Position = new Vector2(X,Y);       
        }
        public override void Draw(SpriteBatch target)
        {
            target.Draw(sprite, new Vector2(Position.X, Position.Y));
        }

    }
}
