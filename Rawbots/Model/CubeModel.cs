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
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using Tao.FreeGlut;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public class CubeModel : Model
    {
		public CubeModel() : base()
		{
		}
		
		private void renderCube(double size, bool solid)
		{
			size /= 2;
			BeginMode beginMode;
			
			beginMode = (solid) ? BeginMode.Quads : BeginMode.LineLoop;
			
			GL.PushMatrix();
			
			GL.Begin(beginMode);
				GL.Normal3(1.0, 0.0, 0.0);
				GL.Vertex3(+size, -size, +size);
				GL.Vertex3(+size, -size, -size);
				GL.Vertex3(+size, +size, -size);
				GL.Vertex3(+size, +size, +size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(0.0, 1.0, 0.0);
				GL.Vertex3(+size, +size, +size);
				GL.Vertex3(+size, +size, -size);
				GL.Vertex3(-size, +size, -size);
				GL.Vertex3(-size, +size, +size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(0.0, 0.0, 1.0);
				GL.Vertex3(+size, +size, +size);
				GL.Vertex3(-size, +size, +size);
				GL.Vertex3(-size, -size, +size);
				GL.Vertex3(+size, -size, +size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(-1.0, 0.0, 0.0);
				GL.Vertex3(-size, -size, +size);
				GL.Vertex3(-size, +size, +size);
				GL.Vertex3(-size, +size, -size);
				GL.Vertex3(-size, -size, -size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(0.0,-1.0, 0.0);
				GL.Vertex3(-size, -size, +size);
				GL.Vertex3(-size, -size, -size);
				GL.Vertex3(+size, -size, -size);
				GL.Vertex3(+size, -size, +size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(0.0, 0.0, -1.0);
				GL.Vertex3(-size, -size, -size);
				GL.Vertex3(-size, +size, -size);
				GL.Vertex3(+size, +size, -size);
				GL.Vertex3(+size, -size, -size);
			GL.End();
			
			GL.PopMatrix();
		}
		
        public void render(double size)
        {
            switch (renderMode)
            {				
                case RenderMode.SOLID:
                    GL.Color3(colorR, colorG, colorB);
					renderCube(size, true);
                	break;
				
                case RenderMode.WIRE:
                    GL.Color3(wireColorR, wireColorG, wireColorB);
					renderCube(size, false);
					break;
				
				case RenderMode.SOLID_WIRE:
					GL.Color3(colorR, colorG, colorB);
					renderCube(size, true);
					GL.Color3(wireColorR, wireColorG, wireColorB);
                	renderCube(size, false);
					break;
            }
        }
    }
}
