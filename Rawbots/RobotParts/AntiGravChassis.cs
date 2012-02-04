<<<<<<< HEAD
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
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class AntiGravChassis : Chassis
	{
		private float bodyWidth;
		private float bodyHeight;
		
		private float tracksWidth;
		private float tracksHeight;
		
		private float topWidth;
		private float topHeight;
		
		private CubeModel cube;
			
		public AntiGravChassis()
		{
			/*
			 * By far the best system, it simply flies over the
			 * ground whatever its difficulties. This is the
			 * only chassis that can span ravines!
			 */
			
			bodyWidth = 0.475f;
			bodyHeight = 0.2f;
			
			tracksWidth = 0.35f;
			tracksHeight = 0.2f;
			
			topWidth = 0.4f;
			topHeight = 0.1f;
			
		}
		
		private void drawBox(float Xtranslate, float Ytranslate, float Ztranslate, float Xscale, float Yscale, float Zscale)
		{
			GL.Translate(Xtranslate, Ytranslate, Ztranslate);
			GL.Scale(Xscale, Yscale, Zscale);
			cube = new CubeModel();
		}
		
		public override void Render()
		{
			//central box
			GL.PushMatrix();
			GL.Scale(bodyWidth, bodyHeight, bodyWidth);
			cube = new CubeModel();
				
				//top box
				GL.PushMatrix();
				drawBox(0.0f, bodyHeight, 0.0f, topWidth, topHeight, topWidth);
					//Team Number
					GL.PushMatrix();
					GL.Scale(0.3f, 0.3f, 0.3f);
					GL.Translate(0.0f, 0.7f, 1.25f);
					TeamNumber.render();
					GL.Translate(0.0f, -0.7f, -1.25f);
					GL.PopMatrix();
				GL.PopMatrix();
			
				//Peripheral tracks
				GL.PushMatrix();
				drawBox(-bodyWidth/2, -bodyHeight, bodyWidth/2, tracksWidth, tracksHeight, tracksWidth);
				GL.PopMatrix();
			
				GL.PushMatrix();
				drawBox(bodyWidth/2, -bodyHeight, bodyWidth/2, tracksWidth, tracksHeight, tracksWidth);
				GL.PopMatrix();
			
				GL.PushMatrix();
				drawBox(-bodyWidth/2, bodyHeight, bodyWidth/2, tracksWidth, tracksHeight, tracksWidth);
				GL.PopMatrix();
			
				GL.PushMatrix();
				drawBox(bodyWidth/2, bodyHeight, bodyWidth/2, tracksWidth, tracksHeight, tracksWidth);
				GL.PopMatrix();
				
			GL.PopMatrix();
			
			
		}
	}
}

=======
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
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class AntiGravChassis : Chassis
	{
		public AntiGravChassis()
		{
			/*
			 * By far the best system, it simply flies over the
			 * ground whatever its difficulties. This is the
			 * only chassis that can span ravines!
			 */
		}

		public override void Render()
		{
			GL.Begin(BeginMode.Triangles);

			GL.Color3(1.0f, 1.0f, 0.0f);
			GL.Vertex3(-1.0f, -1.0f, 4.0f);
			GL.Color3(1.0f, 0.0f, 0.0f);
			GL.Vertex3(1.0f, -1.0f, 4.0f);
			GL.Color3(0.2f, 0.9f, 1.0f);
			GL.Vertex3(0.0f, 1.0f, 4.0f);
			
			GL.End();
		}
	}
}

>>>>>>> 73b383fe7fbe53ca457d1bbdd1eeb05f56365e18
