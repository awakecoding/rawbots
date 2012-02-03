using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class Pit : Tile
    {
        public Plane plane;
        
        public const int NORTH = 1;
        public const int SOUTH = 2;
        public const int EAST = 4;
        public const int WEST = 8;

        public int sides = 0;

        public Pit()
        {
            plane = new Plane();
            sides = NORTH + SOUTH + EAST + WEST;
        }

        public void setVisible(int sides)
        {
            this.sides = sides;
        }

        public override void Render()
        {
            sides = NORTH + SOUTH;

            if ((sides & WEST) == WEST)
            {
                GL.PushMatrix();

                GL.Translate(-0.333f, 0.0f, 0.0f);

                //Draw West Side
                drawSide();

                GL.PopMatrix();
            }

            if ((sides & EAST) == EAST)
            {
                GL.PushMatrix();

                GL.Translate(0.333f, 0.0f, 0.0f);

                //Draw East Side
                drawSide();

                GL.PopMatrix();
            }

            if ((sides & NORTH) == NORTH)
            {
                GL.PushMatrix();

                GL.Translate(0.0f, 0.0f, -0.333f);
                GL.Rotate(90.0f, 0.0f, 1.0f, 0.0f);

                //Draw North Side
                drawSide();

                GL.PopMatrix();
            }

            if ((sides & SOUTH) == SOUTH)
            {
                GL.PushMatrix();

                GL.Translate(0.0f, 0.0f, 0.333f);
                GL.Rotate(90.0f, 0.0f, 1.0f, 0.0f);

                //Draw South Side
                drawSide();

                GL.PopMatrix();
            }
        }

        private void drawSide()
        {
            GL.PushMatrix();

            GL.Scale(0.333f, 1.0f, 1.0f);

            plane.render(1.0f);

            GL.PopMatrix();
        }
    }
}
