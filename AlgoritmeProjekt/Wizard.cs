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

        private float speed;
        private Vector2 start;
        private Vector2 end;
        private Vector2 pos;
        private int currentPath;
        private Texture2D sprite;
        private List<Vector2> path;
        private List<Key> foundKeys = new List<Key>();
        private bool hasPotion = false;
        private bool hasDeliveredPotion = false;

        private Pathfinder pathfinder;

        public Wizard() : base()
        {
            currentPath = 0;

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

        public void Path(List<Vector2> list)
        {
            currentPath = 0;
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

                if(Vector2.Distance(start,pos) >= distance && currentPath != path.Count )
                {
                    
                    this.Position = end;
                    currentPath++;
                    moving = false;
                }   

            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

          
          if(currentPath <= path.Count -1)
            FollowPath(path.ElementAt(currentPath), deltaTime);         

            foreach (Key key in World.Entities.OfType<Key>())
            {
                if (World.AreOnSameTile(this, key) && !foundKeys.Contains(key))
                {
                    //TODO: Remove key from world
                    foundKeys.Add(key);
                }
            }

            if (!hasPotion && !hasDeliveredPotion)
            {
                foreach (Tower stormTower in World.Entities.OfType<Tower>().Where(a => a.Type == TowerType.StormTower))
                {
                    if (World.AreOnSameTile(this, stormTower))
                    {
                        if (foundKeys.Any(a => a.Type == TowerType.StormTower))
                        {
                            //POTION GET
                            hasPotion = true;
                        }
                    }
                }
            }

            if (hasPotion && !hasDeliveredPotion)
            {
                foreach (Tower iceTower in World.Entities.OfType<Tower>().Where(a => a.Type == TowerType.IceTower))
                {
                    if (World.AreOnSameTile(this, iceTower))
                    {
                        if (foundKeys.Any(a => a.Type == TowerType.IceTower))
                        {
                            //POTION PUT
                            hasPotion = false;
                            hasDeliveredPotion = true;
                        }
                    }
                }
            }

            if (hasDeliveredPotion)
            {
                foreach (Portal portal in World.Entities.OfType<Portal>())
                {
                    if (World.AreOnSameTile(this, portal))
                    {
                        //TODO: gtfo
                    }
                }
            }

            

        }

        public override void Draw(SpriteBatch target)
        {
            target.Draw(sprite, new Vector2(Position.X, Position.Y));
        }
    }
}
