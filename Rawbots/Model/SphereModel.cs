/**
 * RawBots: an awesome robot game
 * 
 * Copyright 2012 Marc-Andre Moreau <marcandre.moreau@gmail.com>
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using OpenTK.Graphics.OpenGL;
using Tao.FreeGlut;

namespace Rawbots
{
	public class SphereModel : Model
	{		
		private int p;
		private int q;
		private float radius;
		
		public SphereModel(float radius) : base()
		{
			p = 10;
			q = 10;
			this.radius = radius;
		}
		
		public int LongitudinalSlices { get { return p; } set { p = value; } }
		public int LatitudinalSlices { get { return q; } set { q = value; } }
		public float Radius { get { return radius; } set { radius = value; } }
		
		private void renderSphere(bool solid)
		{
			GL.PushMatrix();

            int[] state = new int[2];
            GL.GetInteger(GetPName.PolygonMode, state);
			
            GL.PolygonMode(MaterialFace.FrontAndBack,
			               (solid) ? PolygonMode.Fill : PolygonMode.Line);

			for (int j = 0; j < q; j++)
			{
				GL.Begin(BeginMode.TriangleStrip);
		
				for (int i = 0; i <= p; i++)
				{				
					GL.Vertex3(radius * Math.Cos((float) (j + 1) / q * Math.PI / 2.0) * Math.Cos(2.0 * (float) i/p * Math.PI),
					           radius * Math.Sin((float) (j + 1) / q * Math.PI / 2.0),
					           radius * Math.Cos((float) (j + 1) / q * Math.PI / 2.0) * Math.Sin(2.0 * (float) i/p * Math.PI));
		
					GL.Vertex3(radius * Math.Cos((float) j/q * Math.PI / 2.0) * Math.Cos(2.0 * (float) i/p * Math.PI),
					           radius * Math.Sin((float) j/q * Math.PI / 2.0),
					           radius * Math.Cos((float) j/q * Math.PI / 2.0) * Math.Sin(2.0 * (float) i/p * Math.PI));         
				}
		
				GL.End();
			}
			
			for (int j = 0; j < q; j++)
			{
				GL.Begin(BeginMode.TriangleStrip);
		
				for (int i = 0; i <= p; i++)
				{				
					GL.Vertex3(radius * Math.Cos((float) (j + 1) / q * Math.PI / 2.0) * Math.Cos(2.0 * (float) i/p * Math.PI),
					           -(radius * Math.Sin((float) (j + 1) / q * Math.PI / 2.0)),
					           radius * Math.Cos((float) (j + 1) / q * Math.PI / 2.0) * Math.Sin(2.0 * (float) i/p * Math.PI));
		
					GL.Vertex3(radius * Math.Cos((float) j/q * Math.PI / 2.0) * Math.Cos(2.0 * (float) i/p * Math.PI),
					           -(radius * Math.Sin((float) j/q * Math.PI / 2.0)),
					           radius * Math.Cos((float) j/q * Math.PI / 2.0) * Math.Sin(2.0 * (float) i/p * Math.PI));         
				}
		
				GL.End();
			}

            GL.PolygonMode(MaterialFace.Front, (PolygonMode)state[0]);
            GL.PolygonMode(MaterialFace.Back, (PolygonMode)state[1]);

			GL.PopMatrix();
		}

		public void renderSolidSphere(double radius, int slices, int stacks)
		{ 
			int i,j;

			/* Adjust z and radius as stacks are drawn. */

			double z0,z1;
			double r0,r1;

			if (sint == null || cost == null)
				CircleTable(ref sint, ref cost, -slices);
			else if (sint != null || cost != null)
				if (sint.Length != Math.Abs(slices) + 1)
					CircleTable(ref sint, ref cost, -slices);

			if (sint2 == null || cost2 == null)
				CircleTable(ref sint2, ref cost2, stacks * 2);
			else if (sint2 != null || cost2 != null)
				if(sint2.Length != Math.Abs(slices) + 1)
					CircleTable(ref sint2, ref cost2, stacks * 2);

			/* The top stack is covered with a triangle fan */

			z0 = 1.0;
			z1 = cost2[(stacks>0)?1:0];
			r0 = 0.0;
			r1 = sint2[(stacks>0)?1:0];

			GL.Begin(BeginMode.TriangleFan);

				GL.Normal3(0.0f,0.0f,1.0f);
				GL.Vertex3(0.0f,0.0f,radius);

				for (j=slices; j>=0; j--)
				{
					GL.Normal3(cost[j]*r1,        sint[j]*r1,        z1       );
					GL.Vertex3(cost[j]*r1*radius, sint[j]*r1*radius, z1*radius);
				}

			GL.End();

			/* Cover each stack with a quad strip, except the top and bottom stacks */

			for( i=1; i<stacks-1; i++ )
			{
				z0 = z1; z1 = cost2[i+1];
				r0 = r1; r1 = sint2[i+1];

				GL.Begin(BeginMode.QuadStrip);

					for(j=0; j<=slices; j++)
					{
						GL.Normal3(cost[j]*r1,        sint[j]*r1,        z1       );
						GL.Vertex3(cost[j]*r1*radius, sint[j]*r1*radius, z1*radius);
						GL.Normal3(cost[j]*r0,        sint[j]*r0,        z0       );
						GL.Vertex3(cost[j]*r0*radius, sint[j]*r0*radius, z0*radius);
					}

				GL.End();
			}

			/* The bottom stack is covered with a triangle fan */

			z0 = z1;
			r0 = r1;

			GL.Begin(BeginMode.TriangleFan);

				GL.Normal3(0.0f,0.0f,-1.0f);
				GL.Vertex3(0.0f,0.0f,-radius);

				for (j=0; j<=slices; j++)
				{
					GL.Normal3(cost[j]*r0,        sint[j]*r0,        z0       );
					GL.Vertex3(cost[j]*r0*radius, sint[j]*r0*radius, z0*radius);
				}

			GL.End();
		}

		public void renderWireSphere(double radius, int slices, int stacks)
		{ 
			int i,j;

			/* Adjust z and radius as stacks and slices are drawn. */

			double r;
			double x,y,z;

			if (sint == null || cost == null)
				CircleTable(ref sint, ref cost, -slices);
			else if (sint != null || cost != null)
				if (sint.Length != Math.Abs(slices) + 1)
					CircleTable(ref sint, ref cost, -slices);

			if (sint2 == null || cost2 == null)
				CircleTable(ref sint2, ref cost2, stacks * 2);
			else if (sint2 != null || cost2 != null)
				if (sint2.Length != Math.Abs(slices) + 1)
					CircleTable(ref sint2, ref cost2, stacks * 2);

			/* Draw a line loop for each stack */

			for (i=1; i<stacks; i++)
			{
				z = cost2[i];
				r = sint2[i];

				GL.Begin(BeginMode.LineLoop);

					for(j=0; j<=slices; j++)
					{
						x = cost[j];
						y = sint[j];

						GL.Normal3(x,y,z);
						GL.Vertex3(x*r*radius,y*r*radius,z*radius);
					}

				GL.End();
			}

			/* Draw a line loop for each slice */

			for (i=0; i<slices; i++)
			{
				GL.Begin(BeginMode.LineStrip);

					for(j=0; j<=stacks; j++)
					{
						x = cost[i]*sint2[j];
						y = sint[i]*sint2[j];
						z = cost2[j];

						GL.Normal3(x,y,z);
						GL.Vertex3(x*radius,y*radius,z*radius);
					}

				GL.End();
			}
		}

        public virtual void render()
        {
            if (material != null)
                material.apply();

            switch (renderMode)
            {
                case RenderMode.SOLID:
                    GL.Color3(colorR, colorG, colorB);
					renderSolidSphere(Radius, LongitudinalSlices, LatitudinalSlices);
					break;
				
                case RenderMode.WIRE:
					GL.Color3(colorR, colorG, colorB);
					renderWireSphere(Radius, LongitudinalSlices, LatitudinalSlices);
                    break;
				
				case RenderMode.SOLID_WIRE:
					GL.Color3(colorR, colorG, colorB);
					renderSolidSphere(Radius, LongitudinalSlices, LatitudinalSlices);
					GL.Color3(wireColorR, wireColorG, wireColorB);
					renderWireSphere(Radius, LongitudinalSlices, LatitudinalSlices);
					break;
            }

            Material.unapply();
        }
	}
}

