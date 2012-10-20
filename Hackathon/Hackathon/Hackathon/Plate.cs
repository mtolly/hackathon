using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hackathon
{
    public class Plate : Microsoft.Xna.Framework.Game
    {
        public Plate(double xOrgin, double yOrgin, double xSpeed, double ySpeed, double yReset, double xReset)
        {
            in_zone = false;
            y_reset = yReset;
            x_reset = xReset;
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
        public double y_reset { get; set; }
        public double x_reset { get; set; }
    }
}