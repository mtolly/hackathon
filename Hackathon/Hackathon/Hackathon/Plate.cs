using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hackathon
{
    public class Plate : Microsoft.Xna.Framework.Game
    {
        public Plate(double xOrgin, double yOrgin, double xSpeed, double ySpeed, double yReset, double xReset, Texture2D contents)
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
            plateContents = contents;
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
        public Texture2D plateContents { get; set; }

        public void updatePlate()
        {
            this.x_value += this.x_speed;
            this.y_value += this.y_speed;
            if (this.y_value >= 650)
            {
                this.x_value = this.x_reset;
                //plate.y_value = plate.y_orgin;
                this.y_value = this.y_reset;
            }

            this.in_zone = 450 <= this.y_value && this.y_value <= 600;
        }
    }
}