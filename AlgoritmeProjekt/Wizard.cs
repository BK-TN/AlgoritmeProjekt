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
        private float X;
        private float Y;
        int CurrentPath;
        private Texture2D sprite;
        private float speed;
        private Vector2 start;
        private Vector2 end;
        private Vector2 pos;

        List<Vector2> path;
        public Wizard() : base()
        {
            CurrentPath = 0;
            speed = 60;
            path = new List<Vector2>();
   
        }


        public override void LoadContent(ContentManager contentManager)
        {
            sprite = contentManager.Load<Texture2D>("wizard");
        }

        public void Path(List<Vector2> list)
        {
            CurrentPath = 0;
            path = list;

        }
        public void FollowPath(Vector2 point, float deltaTime)
        {
            start = this.Position;

            pos = start;

            end = point;

            float distance = Vector2.Distance(start, end);

            Vector2 direction = Vector2.Normalize(end - start);
           
            bool moving = true;

            if(moving == true)
            {
                pos += direction * speed * deltaTime;
                this.Position = pos;

                if(Vector2.Distance(start,pos) >= distance && CurrentPath != path.Count )
                {
                    
                    this.Position = end;
                    CurrentPath++;
                    moving = false;
                }   
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
          
          if(CurrentPath <= path.Count -1)
            FollowPath(path.ElementAt(CurrentPath), deltaTime);         
        }
        public override void Draw(SpriteBatch target)
        {
            target.Draw(sprite, new Vector2(Position.X, Position.Y));
        }

    }
}
