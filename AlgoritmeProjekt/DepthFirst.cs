using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AlgoritmeProjekt
{
    internal class DepthFirst : Pathfinder
    {
        internal class Node
        {
            public GridPos Position { get; }
            public Node Parent { get; }

            public Node(GridPos position, Node parent)
            {
                Position = position;
                Parent = parent;
            }
        }

        public DepthFirst(CollisionGrid c) : base(c)
        {
        }

        public override GridPos[] FindPath(GridPos start, GridPos goal)
        {
            if (start.X == goal.X && start.Y == goal.Y)
            {
                return new GridPos[] { goal };
            }

            List<Node> nodes = new List<Node>();
            nodes.Add(new Node(start, null));

            Node currentNode = nodes[0];
            while (true)
            {
                //Define points to check for new nodes on
                GridPos[] positionsToCheck = new GridPos[]
                {
                    new GridPos(currentNode.Position.X - 1, currentNode.Position.Y),
                    new GridPos(currentNode.Position.X + 1, currentNode.Position.Y),
                    new GridPos(currentNode.Position.X, currentNode.Position.Y - 1),
                    new GridPos(currentNode.Position.X, currentNode.Position.Y + 1)
                };

                bool newNodeFound = false;

                foreach (GridPos pos in positionsToCheck)
                {
                    if (
                        pos.X >= 0 &&
                        pos.Y >= 0 &&
                        pos.X < collisionGrid.Width &&
                        pos.Y < collisionGrid.Height &&
                        !nodes.Any(a => a.Position.X == pos.X && a.Position.Y == pos.Y) &&
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
                        nodes.Add(newNode);

                        currentNode = newNode;
                        newNodeFound = true;
                        break;
                    }
                }

                if (!newNodeFound)
                {
                    //No valid nodes found!

                    if (currentNode.Parent != null)
                    {
                        //Go back to parent
                        currentNode = currentNode.Parent;
                    }
                    else
                    {
                        //Failed
                        return null;
                    }
                }
            }
        }
    }
}
