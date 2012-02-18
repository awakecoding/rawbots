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
	public class RemoteControlUnit
	{
		private double cylinderRadius; //antenna
		private double cylinderHeight;
		private CylinderModel cylinder;
		
		private CubeModel cube;
		private float bodyWidth;
		private float bodyHeight;
		
		private float topWidth;
		private float topHeight;
		
		private float peripheralWidth;
		private float peripheralHeight;
		
		public RemoteControlUnit()
		{
			bodyWidth = 0.6f;
			bodyHeight = 0.2f;
			
			peripheralWidth = 0.4f;
			peripheralHeight = 0.2f;
			
			cylinderHeight = 0.5f;
			cylinderRadius = 0.1f;
			cylinder = new CylinderModel(cylinderRadius, cylinderHeight);
			cylinder.SetColor(0.7f, 0.6f, 0.75f);
			
			cube = new CubeModel();
		
		}
		
		public void Render()
		{
			//main component
			GL.PushMatrix();
			GL.Scale(bodyWidth, bodyHeight, bodyWidth);
			cube.render(1.0);
				
				//top component
				GL.PushMatrix();
				GL.Scale(topWidth, topHeight, topWidth);
				GL.Translate(0.0, bodyHeight/2 + topHeight/2, 0.0);
				cube.render(1.0);
				GL.PopMatrix();
			
				//antenna
				GL.PushMatrix();
				GL.Translate(0.0, bodyHeight, 0.0);
				GL.Rotate(-90.0, 1.0, 0.0, 0.0);
				cylinder.render();
				GL.PopMatrix();
			
				//peripheral components
				GL.PushMatrix();
				GL.Translate(-bodyWidth/2, 0.0, bodyWidth);
				GL.Scale(peripheralWidth, peripheralHeight, peripheralWidth);
				cube.render(1.0);
				GL.PopMatrix();
			
				GL.PushMatrix();
				GL.Translate(-bodyWidth/2, 0.0, -bodyWidth);
				GL.Scale(peripheralWidth, peripheralHeight, peripheralWidth);
				cube.render(1.0);
				GL.PopMatrix();
			
				GL.PushMatrix();
				GL.Translate(bodyWidth/2, 0.0, bodyWidth);
				GL.Scale(peripheralWidth, peripheralHeight, peripheralWidth);
				cube.render(1.0);
				GL.PopMatrix();
			
				GL.PushMatrix();
				GL.Translate(bodyWidth/2, 0.0, -bodyWidth);
				GL.Scale(peripheralWidth, peripheralHeight, peripheralWidth);
				cube.render(1.0);
				GL.PopMatrix();
	
		}
	}
}

