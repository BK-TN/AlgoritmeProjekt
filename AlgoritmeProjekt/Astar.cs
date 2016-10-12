using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AlgoritmeProjekt
{
    class Astar
    {
        
        private List<Node> opdenList = new List<Node>();
        private List<Node> closedList = new List<Node>();

        public List<Node> OpenList { get; set; }
        public List<Node> ClosedList { get; set; }

        private Node startNode;
        
        public Astar()
        {
            OpenList.Add(startNode);

        }

        public void Update()
        {
            //TODO add loop to check neighbouring fields & calculate F value
        }

        public void CalculateHValue()
        {
            
            //n.H = 10 * (Math.Abs(/*currentX - targetX*/) + abs(/*currentY - targetY*/));
        }
    }
}
