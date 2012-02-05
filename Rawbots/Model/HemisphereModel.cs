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
	public class HemisphereModel : Model
	{
		private int p;
		private int q;
		private float radius;
		
		public HemisphereModel(float radius) : base()
		{
			p = 10;
			q = 10;
			this.radius = radius;
		}
		
		public int LongitudinalSlices { get { return p; } set { p = value; } }
		public int LatitudinalSlices { get { return q; } set { q = value; } }
		public float Radius { get { return radius; } set { radius = value; } }
		
		private void renderHemisphere(bool solid)
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

            GL.PolygonMode(MaterialFace.Front, (PolygonMode)state[0]);
            GL.PolygonMode(MaterialFace.Back, (PolygonMode)state[1]);

			GL.PopMatrix();
		}
		
        public virtual void render()
        {
            float[] color = { 0.0f, 0.0f, 0.0f, 0.0f };
            GL.GetFloat(GetPName.CurrentColor, color);

            switch (renderMode)
            {
                case RenderMode.SOLID:
                    GL.Color3(colorR, colorG, colorB);
                    renderHemisphere(true);
                	break;
				
                case RenderMode.WIRE:
					GL.Color3(colorR, colorG, colorB);
					renderHemisphere(false);
					break;
				
				case RenderMode.SOLID_WIRE:
					GL.Color3(colorR, colorG, colorB);
					renderHemisphere(true);
					GL.Color3(wireColorR, wireColorG, wireColorB);
                	renderHemisphere(false);
					break;
            }

            GL.Color4(color);
        }
	}
}

