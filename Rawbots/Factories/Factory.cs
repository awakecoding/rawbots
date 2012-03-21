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

		private OBJModel full;
		private OBJModel half;

		private void Init()
		{
			robotPart = null;
			blockFrontLeft = new Block(true);
			blockFrontRight = new Block(true);
			blockBackLeft = new Block(false);
			blockBackMiddle = new Block(false);
			blockBackRight = new Block(false);

			//model = new OBJModel(Game.resourcePath + "/Factory/Factory.obj");
			full = new OBJModel(Game.resourcePath + "/Factory/light_khaki_bldg.obj");
			half = new OBJModel(Game.resourcePath + "/Factory/light_khaki_bldg_half.obj");
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

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            blockFrontLeft.SetRenderMode(renderMode);
            blockFrontRight.SetRenderMode(renderMode);
            blockBackLeft.SetRenderMode(renderMode);
            blockBackMiddle.SetRenderMode(renderMode);
            blockBackRight.SetRenderMode(renderMode);

            if (robotPart != null)
                robotPart.SetRenderMode(renderMode);
        }

		public void Render()
		{
			GL.PushMatrix();

			//model.Render();

			GL.Translate(-1.0f, 0.0f, 0.0f);
			full.Render();
			//blockBackLeft.Render();
			GL.Translate(0.0f, 0.0f, 1.0f);
			half.Render();
			//blockFrontLeft.Render();
			GL.Translate(1.0f, 0.0f, -1.0f);
			full.Render();
			//blockBackMiddle.Render();
			GL.Translate(1.0f, 0.0f, 0.0f);
			full.Render();
			//blockBackRight.Render();
			GL.Translate(0.0f, 0.0f, 1.0f);
			half.Render();
			//blockFrontRight.Render();
			GL.Translate(-1.0f, 0.0f, -1.0f);

			if (robotPart != null)
			{
				GL.Translate(0.0f, 1.0f, 0.0f);
				robotPart.RenderAll();
				GL.Translate(0.0f, -1.0f, 0.0f);
			}
			
			///* team number */
			
			//GL.PushMatrix();
			
			//GL.Scale(0.3f, 0.3f, 0.3f);
			//GL.Translate(0.0f, 1.0f, 2.0f);
			//TeamNumber.Render();
			//GL.Translate(0.0f, -1.0f, -1.0f);
			
			//GL.PopMatrix();

			GL.PopMatrix();
		}
	}
}

