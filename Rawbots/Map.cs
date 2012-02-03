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

namespace Rawbots
{
	public class Map
	{
		int width;
		int height;
		Terrain terrain;
		
		public Terrain Terrain { get { return terrain; } }
		
		public Map(int width, int height)
		{
			this.width = width;
			this.height = height;
			terrain = new Terrain(this.width, this.height);
		}
		
        public void Render()
        {
            GL.PushMatrix();
			
			terrain.BeginRender();
			
            GL.Translate(-width / 2.0f, 0.0f, height / 2.0f);
			
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GL.Translate(i * 1.0f, 0.0f, j * -1.0f);
					terrain.RenderTile(i, j);
                    GL.Translate(-i * 1.0f, 0.0f, j * 1.0f);
                }
            }
            
			terrain.EndRender();
			
            GL.PopMatrix();
        }
	}
}

