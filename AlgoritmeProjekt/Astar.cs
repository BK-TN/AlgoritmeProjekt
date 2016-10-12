using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AlgoritmeProjekt
{
    internal class Astar : Pathfinder
    {
        public Astar(CollisionGrid c) : base(c)
        {
        }

        public override GridPos[] FindPath(GridPos start, GridPos goal)
        {
            List<Node> closedList = new List<Node>();
            List<Node> openList = new List<Node>();

            if (start.X == goal.X && start.Y == goal.Y)
                return new GridPos[] { goal };

            openList.Add(new Node(start, null, 0, goal));

            while (openList.Count > 0)
            {
                Node currentNode = openList.OrderBy(a => a.F).First();

                closedList.Add(currentNode);
                openList.Remove(currentNode);

                if (currentNode.Position.X == goal.X && currentNode.Position.Y == goal.Y)
                {
                    //Goal found! Backtrace to find route
                    List<GridPos> result = new List<GridPos>();
                    Node backtraceNode = currentNode;
                    while (backtraceNode != null)
                    {
                        if (backtraceNode.Parent != null) //If not first node
                            result.Add(backtraceNode.Position);
                        backtraceNode = backtraceNode.Parent;
                    }
                    //Reverse the route, then add the endpoint
                    result.Reverse();
                    return result.ToArray();
                }

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
                        !collisionGrid.GetTile(pos.X, pos.Y) &&
                        !closedList.Any(a => a.Position.X == pos.X && a.Position.Y == pos.Y))
                    {
                        Node nodeAtPoint = openList.FirstOrDefault(a => a.Position.X == pos.X && a.Position.Y == pos.Y);
                        if (nodeAtPoint == null)
                        {
                            Node newNode = new Node(pos, currentNode, currentNode.G + 10, goal);
                            openList.Add(newNode);
                        }
                        else
                        {
                            int newG = currentNode.G + 10;
                            if (newG < nodeAtPoint.G)
                            {
                                nodeAtPoint.G = newG;
                                nodeAtPoint.Parent = currentNode;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
