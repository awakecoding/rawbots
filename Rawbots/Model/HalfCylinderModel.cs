/**
 * RawBots: an awesome robot game
 * 
 * Copyright 2012 Marc-Andre Moreau <marcandre.moreau@gmail.com>
 * Copyright 2012 Mark Foo Bonasoro <foo_mark@q8ismobile.com>
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class HalfCylinderModel : Model
	{
		private int slices;
		private int stacks;
		private double radius;
		private double height;
		
		public HalfCylinderModel(double radius, double height)
		{
			slices = 10;
			stacks = 10;
			this.radius = radius;
			this.height = height;
		}
		
        private void CircleTable(ref double [] sint, ref double [] cost, int n)
        {
            int i;

            int size = Math.Abs(n);

            double angle = 2 * Math.PI / (double) ((n == 0) ? 1 : n);

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
		
        private void renderHalfCylinder()
        {
            int i, j;

            double z0, z1;
            double zStep = height / ((stacks > 0) ? stacks : 1);

            double[] sint = null;
            double[] cost = null;

            CircleTable(ref sint, ref cost, -slices);

            GL.Begin(BeginMode.TriangleFan);
                GL.Normal3(0.0, 0.0, -1.0);
                GL.Vertex3(0.0, 0.0, 0.0);
                for (j = slices / 2; j <= slices; j++)
                    GL.Vertex3(cost[j] * radius, sint[j] * radius, 0.0);
            GL.End();
			
            GL.Begin(BeginMode.TriangleFan);
                GL.Normal3(0.0, 0.0, 1.0);
                GL.Vertex3(0.0, 0.0, height);
                for (j = slices; j >= slices / 2; j--)
                    GL.Vertex3(cost[j] * radius, sint[j] * radius, height);
            GL.End();

            z0 = 0.0f;
            z1 = zStep;

            for (i = 1; i <= stacks; i++)
            {
                if (i == stacks)
                    z1 = height;

                GL.Begin(BeginMode.QuadStrip);
                for (j = slices / 2; j <= slices; j++)
                {
					GL.Normal3(cost[j], sint[j], 0.0);
					GL.Vertex3(cost[j] * radius, sint[j] * radius, z0);
					GL.Vertex3(cost[j] * radius, sint[j] * radius, z1);
                }
                GL.End();

                z0 = z1; z1 += zStep;
            }
			
			GL.Begin(BeginMode.Quads);
				GL.Vertex3(-radius, 0.0, 0.0);
				GL.Vertex3(radius, 0.0, 0.0);
				GL.Vertex3(radius, 0.0, height);
				GL.Vertex3(-radius, 0.0, height);
			GL.End();
			
            sint = null;
            cost = null;
            System.GC.Collect();
        }
		
        public void render()
        {
            switch (renderMode)
            {				
                case RenderMode.SOLID:
                    GL.Color3(colorR, colorG, colorB);
                    renderHalfCylinder();
                    break;
				
                case RenderMode.WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    renderHalfCylinder();
                    break;
				
                case RenderMode.SOLID_WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    renderHalfCylinder();
                    GL.Color3(wireColorR, wireColorG, wireColorB);
                    renderHalfCylinder();
                    break;
            }
        }
	}
}

