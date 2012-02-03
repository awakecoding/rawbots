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
	public class Factory : Model
	{
		Block blockFrontLeft;
		Block blockFrontRight;
		Block blockBackLeft;
		Block blockBackMiddle;
		Block blockBackRight;
		
		protected RobotPart robotPart;
		
		public int PosX { get; set; }
		public int PosY { get; set; }
		
		private void Init()
		{
			robotPart = null;
			blockFrontLeft = new Block(true);
			blockFrontRight = new Block(true);
			blockBackLeft = new Block(false);
			blockBackMiddle = new Block(false);
			blockBackRight = new Block(false);
		}
		
		public Factory() : base()
		{
			Init();
			PosX = 0;
			PosY = 0;
		}
		
		public Factory(int x, int y) : base()
		{
			Init();
			PosX = x;
			PosY = y;
		}
		
		public Factory(int x, int y, RobotPart robotPart) : base()
		{
			Init();
			PosX = x;
			PosY = y;
			this.robotPart = robotPart;
		}
		
		public void Render()
		{
			GL.PushMatrix();
			
			GL.Translate(-1.0f, 0.0f, 0.0f);
			blockBackLeft.Render();
			GL.Translate(0.0f, 0.0f, 1.0f);
			blockFrontLeft.Render();
			GL.Translate(1.0f, 0.0f, -1.0f);
			blockBackMiddle.Render();
			GL.Translate(1.0f, 0.0f, 0.0f);
			blockBackRight.Render();
			GL.Translate(0.0f, 0.0f, 1.0f);
			blockFrontRight.Render();
			GL.Translate(-1.0f, 0.0f, -1.0f);
			
			if (robotPart != null)
			{
				GL.Translate(0.0f, 1.0f, 0.0f);
				robotPart.RenderAll();
				GL.Translate(0.0f, -1.0f, 0.0f);
			}
			
			/* team number */
			
			GL.PushMatrix();
			
			GL.Scale(0.3f, 0.3f, 0.3f);
			GL.Translate(0.0f, 1.0f, 2.0f);
			TeamNumber.render();
			GL.Translate(0.0f, -1.0f, -1.0f);
			
			GL.PopMatrix();

			GL.PopMatrix();
		}
	}
}

