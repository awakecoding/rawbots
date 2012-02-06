
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
			
			bodyWidth = 2.0f;
			bodyHeight = 0.2f;
			
			tracksWidth = 1.0f;
			tracksHeight = 0.2f;
			
			topWidth = 1.0f;
			topHeight = 0.1f;
			
			cube = new CubeModel();
			cube.SetRenderMode(RenderMode.SOLID_WIRE);
			
		}
		
		private void drawBox(float Xtranslate, float Ytranslate, float Ztranslate, float Xscale, float Yscale, float Zscale)
		{
			GL.Translate(Xtranslate, Ytranslate, Ztranslate);
			GL.Scale(Xscale, Yscale, Zscale);
			cube.render(1.0f);
		}
		
		public override void Render()
		{
			//Unit size
			GL.Scale (0.33, 0.33, 0.33);
			
			cube.SetColor(0.3f, 0.3f, 0.3f);
			//central box
			GL.PushMatrix();
			GL.Scale(bodyWidth, bodyHeight, bodyWidth);
			cube.render(1.0);
			GL.PopMatrix();
			
			cube.SetColor(0.4f, 0.4f, 0.4f);
			//top box
			GL.PushMatrix();
			drawBox(0.0f, bodyHeight/2, 0.0f, topWidth, topHeight, topWidth);
			GL.PopMatrix();
			
			cube.SetColor(0.2f, 0.2f, 0.2f);
			//Peripheral tracks
			GL.PushMatrix();
			drawBox(-bodyWidth/2, -bodyHeight, -bodyWidth/2, tracksWidth, tracksHeight, tracksWidth);
			GL.PopMatrix();
			
			GL.PushMatrix();
			drawBox(bodyWidth/2, -bodyHeight, bodyWidth/2, tracksWidth, tracksHeight, tracksWidth);
			GL.PopMatrix();
			
			GL.PushMatrix();
			drawBox(-bodyWidth/2, -bodyHeight, bodyWidth/2, tracksWidth, tracksHeight, tracksWidth);
			GL.PopMatrix();
			
			GL.PushMatrix();
			drawBox(bodyWidth/2, -bodyHeight, -bodyWidth/2, tracksWidth, tracksHeight, tracksWidth);
			GL.PopMatrix();
			
			cube.SetColor(0.9f, 0.9f, 0.9f);
			//Team Number
			GL.PushMatrix();
			GL.Translate(0.0, bodyHeight, 0.0);
			GL.Rotate(-90.0, 1.0, 0.0, 0.0);
			GL.Scale(0.5, 0.5, 0.5);
			TeamNumber.Render();
			GL.PopMatrix();
				
			
			
			
		}
	}
}
