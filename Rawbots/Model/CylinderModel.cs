﻿/**
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
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class CylinderModel : Model
    {
		private int slices;
		private int stacks;
		private double radius;
		private double height;
		
		public CylinderModel(double radius, double height)
		{
			slices = 8;
			stacks = 1;
			this.radius = radius;
			this.height = height;
		}

        public void setSlices(int n)
        {
            this.slices = n;
        }

        public void setStacks(int n)
        {
            this.stacks = n;
        }
		
		public double getHeight()
		{
			return height;
		}
		
		public void setHeight(double d)
		{
			this.height = d;	
		}
		
		public double getRadius()
		{
			return radius;
		}
		
		public void setRadius(double d)
		{
			this.radius = d;	
		}
			
        private void renderSolidCylinder()
        {
            int i, j;

            double z0, z1;
            double zStep = height / ((stacks > 0) ? stacks : 1);

			if(sint == null || cost == null) //if they are blank slates, then create a new table
				CircleTable(ref sint,ref cost, -slices);

			if (sint != null || cost != null) //if they were already created...
				if (sint.Length != Math.Abs(slices) + 1) //but.. the size was different
					CircleTable(ref sint, ref cost, -slices);

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
        }

        private void renderWireCylinder()
        {
            int i, j;

            double z = 0.0f;
            double zStep = height / ((stacks > 0) ? stacks : 1);

            double[] sint = null;
            double[] cost = null;

			if (sint == null || cost == null) //if they are blank slates, then create a new table
				CircleTable(ref sint,ref cost, -slices);
			else if (sint != null || cost != null)
				if (sint.Length != Math.Abs(slices) + 1)
					CircleTable(ref sint, ref cost, -slices);

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
        }
		
        public void render()
        {
            if (material != null)
                material.apply();

            switch (renderMode)
            {				
                case RenderMode.SOLID:
                    GL.Color3(colorR, colorG, colorB);
                    renderSolidCylinder();
                    break;
				
                case RenderMode.WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    renderWireCylinder();
                    break;
				
                case RenderMode.SOLID_WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    renderSolidCylinder();
                    GL.Color3(wireColorR, wireColorG, wireColorB);
                    renderWireCylinder();
                    break;
            }

            Material.unapply();
        }
    }
}
