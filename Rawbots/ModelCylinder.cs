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
                    drawSolid(radius, height, slices, stacks);
                    //Glut.glutSolidCylinder(radius, height, slices, stacks); //Inner Chasis
                    GL.Color3(colorWR, colorWG, colorWB);
                    drawWire(radius, height, slices, stacks);
                    //Glut.glutWireCylinder(radius, height, slices, stacks);
                    break;
                case SOLID:
                    GL.Color3(colorR, colorG, colorB);
                    drawSolid(radius, height, slices, stacks); ;
                    break;
                case WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    drawWire(radius, height, slices, stacks);
                    break;
            }
        }

        private void drawSolid(double radius, double height, int slices, int stacks)
        {
            int i, j;

            double z0, z1;
            double zStep = height / ((stacks > 0) ? stacks : 1);

            double[] sint = null;// = new double[slices+1];
            double[] cost = null;// = new double[slices+1];

            fghCircleTable(ref sint,ref cost, -slices);

            GL.Begin(BeginMode.TriangleFan);
                GL.Normal3(0.0, 0.0, -1.0);
                GL.Vertex3(0.0, 0.0, 0.0);
                for (j = 0; j <= slices; j++)
                    GL.Vertex3(cost[j] * radius, sint[j] * radius, 0.0);
            GL.End();

            GL.Begin(BeginMode.TriangleFan);
                GL.Normal3(0.0, 0.0, 1.0);
                GL.Vertex3(0.0, 0.0, height);
                for (j = slices; j >= 0; j--)
                    GL.Vertex3(cost[j] * radius, sint[j] * radius, height);
            GL.End();

            z0 = 0.0f;
            z1 = zStep;

            for (i = 1; i <= stacks; i++)
            {
                if (i == stacks)
                    z1 = height;

                GL.Begin(BeginMode.QuadStrip);
                for (j = 0; j <= slices; j++)
                {
                    GL.Normal3(cost[j], sint[j], 0.0);
                    GL.Vertex3(cost[j] * radius, sint[j] * radius, z0);
                    GL.Vertex3(cost[j] * radius, sint[j] * radius, z1);
                }
                GL.End();

                z0 = z1; z1 += zStep;
            }

            sint = null;
            cost = null;
            System.GC.Collect();
        }

        private void drawWire(double radius, double height, int slices, int stacks)
        {
            int i, j;

            double z = 0.0f;
            double zStep = height / ((stacks > 0) ? stacks : 1);

            double[] sint = null;// = new double[slices+1];
            double[] cost = null;// = new double[slices+1];

            fghCircleTable(ref sint,ref cost, -slices);

            /* Draw the stacks... */

            for (i = 0; i <= stacks; i++)
            {
                if (i == stacks)
                    z = height;

                GL.Begin(BeginMode.LineLoop);

                for (j = 0; j < slices; j++)
                {
                    GL.Normal3(cost[j], sint[j], 0.0);
                    GL.Vertex3(cost[j] * radius, sint[j] * radius, z);
                }

                GL.End();

                z += zStep;
            }

            /* Draw the slices */

            GL.Begin(BeginMode.Lines);

            for (j = 0; j < slices; j++)
            {
                GL.Normal3(cost[j], sint[j], 0.0);
                GL.Vertex3(cost[j] * radius, sint[j] * radius, 0.0);
                GL.Vertex3(cost[j] * radius, sint[j] * radius, height);
            }

            GL.End();

            sint = null;
            cost = null;
            System.GC.Collect();
        }

        private void fghCircleTable(ref double [] sint, ref double [] cost, int n)
        {
            int i;

            int size = Math.Abs(n);

            double angle = 2*Math.PI/(double)((n==0)?1:n);

            sint = new double[size + 1];
            cost = new double[size + 1];

            sint[0] = 0.0f;
            cost[0] = 1.0f;

            for (i = 1; i < size; i++)
            {
                sint[i] = Math.Sin(angle * i);
                cost[i] = Math.Cos(angle * i);
            }

            sint[size] = sint[0];
            cost[size] = cost[0];
        }
    }
}
