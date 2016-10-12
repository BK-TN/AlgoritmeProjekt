using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AlgoritmeProjekt
{
    class Astar : Pathfinder
    {

        private List<Node> openList = new List<Node>();
        private List<Node> closedList = new List<Node>();
        private bool endLoop = false;

        public List<Node> OpenList { get; set; }
        public List<Node> ClosedList { get; set; }


        public Astar(CollisionGrid c) : base(c)
        {
        }

        public override GridPos[] FindPath(GridPos start, GridPos goal)
        {
            if (start.X == goal.X && start.Y == goal.Y)
                return new GridPos[] { goal };


            openList.Add(new Node(start, null));
            
            while (openList.Count > 0)
            {
                Node currentNode = openList.OrderBy(a => a.GetFValue).First();

                closedList.Add(currentNode);
                openList.Remove(currentNode);

                //Check each direction for empty spot
                GridPos[] positionsToCheck = new GridPos[]
                {
                    new GridPos(currentNode.Position.X - 1, currentNode.Position.Y ),
                    new GridPos(currentNode.Position.X + 1, currentNode.Position.Y),
                    new GridPos(currentNode.Position.X, currentNode.Position.Y - 1),
                    new GridPos(currentNode.Position.X, currentNode.Position.Y + 1)
                };

                foreach (GridPos pos in positionsToCheck)
                {
                    if (
                        pos.X >= 0 &&
                        pos.Y >= 0 &&
                        pos.X < collisionGrid.Width &&
                        pos.Y < collisionGrid.Height &&
                        !openList.Any(a => a.Position.X == pos.X && a.Position.Y == pos.Y) &&
                        !collisionGrid.GetTile(pos.X, pos.Y))
                    {
                        if (pos.X == goal.X && pos.Y == goal.Y)
                        {
                            //Goal found! Backtrace to find route
                            List<GridPos> result = new List<GridPos>();
                            Node backtraceNode = currentNode;
                            while (backtraceNode != null)
                            {
                                if (backtraceNode.Parent != null)
                                    result.Add(backtraceNode.Position);
                                backtraceNode = backtraceNode.Parent;
                            }
                            //Reverse the route, then add the endpoint
                            result.Reverse();
                            result.Add(pos);
                            return result.ToArray();
                        }
                        Node newNode = new Node(pos, currentNode);
                        openList.Add(newNode);

                        break;
                    }
                }


            }
            return null;

        }
    }
}
