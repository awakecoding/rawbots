using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using Tao.FreeGlut;

namespace Rawbots
{
    class ModelCylinder
    {
        public const int OUTLINEDSOLID = 0;
        public const int SOLID = 1;
        public const int WIRE = 2;
        private int mode = OUTLINEDSOLID;

        private float colorR = 0.22f;
        private float colorG = 0.22f;
        private float colorB = 0.22f;

        private float colorWR = 1.0f;
        private float colorWG = 0.0f;
        private float colorWB = 0.0f;

        public void setRenderMode(int mode)
        {
            this.mode = mode;
        }

        public void setColor(float r, float g, float b)
        {
            colorR = r;
            colorG = g;
            colorB = b;
        }

        public void setWireColor(float r, float g, float b)
        {
            colorWR = r;
            colorWG = g;
            colorWB = b;
        }

        public void render(double radius, double height, int slices, int stacks)
        {
            switch (mode)
            {
                case OUTLINEDSOLID:
                    GL.Color3(colorR, colorG, colorB);
                    Glut.glutSolidCylinder(radius, height, slices, stacks); //Inner Chasis
                    GL.Color3(colorWR, colorWG, colorWB);
                    Glut.glutWireCylinder(radius, height, slices, stacks);
                    break;
                case SOLID:
                    GL.Color3(colorR, colorG, colorB);
                    Glut.glutSolidCylinder(radius, height, slices, stacks); ;
                    break;
                case WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    Glut.glutWireCylinder(radius, height, slices, stacks);
                    break;
            }
        }
    }
}
