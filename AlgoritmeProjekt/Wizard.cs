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
        private float speed = 300;
        private Vector2 start;
        private Vector2 end;
        private Vector2 pos;
        private int currentPath;
        private Texture2D sprite;
        private Texture2D potionSprite;
        private List<Vector2> path;
        private List<Key> foundKeys = new List<Key>();
        private bool hasPotion = false;
        private bool hasDeliveredPotion = false;

        private Pathfinder pathfinder;

        public Wizard(Pathfinder pathfinder) : base()
        {
            this.pathfinder = pathfinder;

            currentPath = 0;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            sprite = contentManager.Load<Texture2D>("wizard");
            potionSprite = contentManager.Load<Texture2D>("potion");
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
                            SetTarget(key);
                        }
                        else
                        {
                            throw new Exception("No storm key");
                        }
                    }
                    else
                    {
                        //Go to tower
                        Tower tower = LookForTower(TowerType.StormTower);
                        if (tower != null)
                        {
                            SetTarget(tower);
                        }
                        else
                        {
                            throw new Exception("No storm tower");
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
                            SetTarget(key);
                        }
                        else
                        {
                            throw new Exception("No ice key");
                        }
                    }
                    else
                    {
                        //Go to tower
                        Tower tower = LookForTower(TowerType.IceTower);
                        if (tower != null)
                        {
                            SetTarget(tower);
                        }
                        else
                        {
                            throw new Exception("No ice tower");
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
                    SetTarget(p);
                }
                else
                {
                    throw new Exception("No portal");
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

            Vector2 moveVector = end - start;
            Vector2 direction = Vector2.Zero;
            if (moveVector != Vector2.Zero)
            {
                direction = Vector2.Normalize(moveVector);
            }

            bool moving = true;

            if (moving == true)
            {
                pos += direction * speed * deltaTime;
                this.Position = pos;

                if (Vector2.Distance(start, pos) >= distance && currentPath != path.Count)

                {
                    this.Position = end;
                    currentPath++;
                    moving = false;
                }

                if (currentPath == path.Count)
                {
                    path = null;
                }
            }
        }

        private void SetTarget(Entity target)
        {
            GridPos[] path = pathfinder.FindPath(World.VectorToGridPos(Position), World.VectorToGridPos(target.Position));
            currentPath = 0;

            if (path != null)
            {
                this.path = path.Select(a => World.GridPosToVector(a)).ToList();
            }
            else
            {
                this.path = null;
                throw new Exception("No path found!!");
            }

            if (this.path != null)
            {
                System.Diagnostics.Debug.WriteLine("New path, " + this.path.Count + " points");
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (path != null && currentPath <= path.Count - 1)
                FollowPath(path[currentPath], deltaTime);

            if (path == null)
            {
                Wat2Do();
            }

            foreach (Key key in World.Entities.OfType<Key>())
            {
                if (World.AreOnSameTile(this, key) && !foundKeys.Contains(key))
                {
                    World.RemoveEntity(key);
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
            if (hasPotion)
            {
                target.Draw(potionSprite, new Vector2(Position.X + 30, Position.Y + 30));
            }
        }
    }
}
