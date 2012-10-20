using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hackathon
{
    public class Plate : Microsoft.Xna.Framework.Game
    {
        public Plate(int xOrgin, int yOrgin, double xSpeed, double ySpeed)
        {
            x_value = xOrgin;
            y_value = yOrgin;
            x_speed = xSpeed;
            y_speed = ySpeed;
            x_orgin = xOrgin;
            y_orgin = yOrgin;
        }
        public int x_value { get; set; }
        public int y_value { get; set; }
        public double x_speed { get; set; }
        public double y_speed { get; set; }
        public int x_orgin { get; set; }
        public int y_orgin { get; set; }
    }
}