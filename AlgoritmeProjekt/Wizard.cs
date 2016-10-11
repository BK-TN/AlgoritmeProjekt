using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlgoritmeProjekt
{
    internal class Wizard : Entity
    {
        private float X;
        private float Y;
        private int CurrentPath;
        private Texture2D sprite;
        private List<Vector2> path;

        private List<Key> foundKeys = new List<Key>();
        private bool hasPotion = false;
        private bool hasDeliveredPotion = false;

        private Pathfinder pathfinder;

        public Wizard() : base()
        {
            CurrentPath = 0;

            //pathfinder = new Pathfinder(collisionGrid);

            path = new List<Vector2>();
            path.Add(new Vector2(100, 400));
            path.Add(new Vector2(400, 400));

            //Wat2Do();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            sprite = contentManager.Load<Texture2D>("wizard");
        }

        private void Wat2Do()
        {
            if (!hasDeliveredPotion)
            {
                if (!hasPotion)
                {
                    //Step 1: Find potion
                    if (!foundKeys.Any(a => a.Type == TowerType.StormTower))
                    {
                        //Find storm tower key
                        Key key = LookForKey(TowerType.StormTower);
                        if (key != null)
                        {
                            path = pathfinder.FindPath(Position, key.Position).ToList();
                        }
                    }
                    else
                    {
                        //Go to tower
                        Tower tower = LookForTower(TowerType.StormTower);
                        if (tower != null)
                        {
                            path = pathfinder.FindPath(Position, tower.Position).ToList();
                        }
                    }
                }
                else
                {
                    //Step 2: Deliver potion
                    if (!foundKeys.Any(a => a.Type == TowerType.IceTower))
                    {
                        //Find ice tower key
                        Key key = LookForKey(TowerType.IceTower);
                        if (key != null)
                        {
                            path = pathfinder.FindPath(Position, key.Position).ToList();
                        }
                    }
                    else
                    {
                        //Go to tower
                        Tower tower = LookForTower(TowerType.IceTower);
                        if (tower != null)
                        {
                            path = pathfinder.FindPath(Position, tower.Position).ToList();
                        }
                    }
                }
            }
            else
            {
                //Step 3: Return to base
                Portal p = World.Entities.OfType<Portal>().FirstOrDefault();
                if (p != null)
                {
                    path = pathfinder.FindPath(Position, p.Position).ToList();
                }
            }
        }

        private Key LookForKey(TowerType type)
        {
            return World.Entities.OfType<Key>().FirstOrDefault(a => a.Type == type);
        }

        private Tower LookForTower(TowerType type)
        {
            return World.Entities.OfType<Tower>().FirstOrDefault(a => a.Type == type);
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
                X -= 35 * deltaTime;
                this.Position = new Vector2(X, Y);
            }
            if (tal.X > X)
            {
                X += 35 * deltaTime;
                this.Position = new Vector2(X, Y);
            }
            if (tal.Y < Y)
            {
                Y -= 35 * deltaTime;
                this.Position = new Vector2(X, Y);
            }
            if (tal.Y > Y)
            {
                Y += 35 * deltaTime;

                this.Position = new Vector2(X, Y);
            }

            if (Vector2.Distance(tal, new Vector2(X, Y)) <= 1 && CurrentPath != path.Count - 1)
            {
                this.Position = new Vector2(tal.X, tal.Y);
                CurrentPath++;
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            goToPoint(path.ElementAt(CurrentPath), deltaTime);

            this.Position = new Vector2(X, Y);
        }

        public override void Draw(SpriteBatch target)
        {
            target.Draw(sprite, new Vector2(Position.X, Position.Y));
        }
    }
}
