﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace AlgoritmeProjekt
{
    internal class Tree : Entity
    {
        public Tree(int x, int y) : base(x, y)
        {
            Solid = true;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //TODO: Load tree sprite
        }
    }
}
