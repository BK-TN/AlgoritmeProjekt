﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace AlgoritmeProjekt
{
    internal class Monster : Entity
    {
        public Monster(int x, int y) : base(x, y)
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //TODO: Load monster sprite
        }
    }
}