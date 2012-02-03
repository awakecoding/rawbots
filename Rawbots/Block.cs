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
	public class Block : Model
	{
		bool half;
		CubeModel cube;
		
		public Block(bool half) : base()
		{
			this.half = half;
			cube = new CubeModel();
			cube.SetColor(0.5f, 0.35f, 0.05f);
		}
		
        public void Render()
        {
			GL.PushMatrix();
			
			if (half)
			{
				GL.Scale(1.0f, 0.5f, 1.0f);
				GL.Translate(0.0f, 0.5f, 0.0f);
				cube.render(1.0f);
				GL.Translate(0.0f, -0.5f, 0.0f);
			}
			else
			{
				GL.Translate(0.0f, 0.5f, 0.0f);
				cube.render(1.0f);
				GL.Translate(0.0f, -0.5f, 0.0f);
			}
			
			GL.PopMatrix();
        }
	}
}

