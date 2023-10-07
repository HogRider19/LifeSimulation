﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Base
{
    public struct Position
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;

        public Position(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public static Position operator +(Position a, Position b) 
        {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b)
        {
            return new Position(a.X - b.X, a.Y - b.Y);
        }
    }
}