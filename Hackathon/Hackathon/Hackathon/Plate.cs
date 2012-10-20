using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hackathon
{
    public class Plate : Microsoft.Xna.Framework.Game
    {
        public Plate(double xOrgin, double yOrgin, double xSpeed, double ySpeed)
        {
            in_zone = false;
            x_value = xOrgin;
            y_value = yOrgin;
            x_speed = xSpeed;
            y_speed = ySpeed;
            x_orgin = xOrgin;
            y_orgin = yOrgin;
        }
        public bool in_zone { get; set; }
        public double x_value { get; set; }
        public double y_value { get; set; }
        public double x_speed { get; set; }
        public double y_speed { get; set; }
        public double x_orgin { get; set; }
        public double y_orgin { get; set; }
    }
}