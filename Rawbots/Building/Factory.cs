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
			blockFrontLeft = new HalfPlainBlock();
			blockFrontRight = new HalfPlainBlock();
			blockBackLeft = new FullPlainBlock();
			blockBackMiddle = new FullPlainBlock();
			blockBackRight = new FullPlainBlock();

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

			/**
			 * Factory
			 * _____________
			 * |_F_|_M_|_F_|
			 * |_H_|   |_H_|
			 * 
			 * F = full
			 * H = half
			 * M = full + robot part
			 * 
			 */

			full.Render(); /* Full (Left) */
			GL.Translate(1.0f, 0.0f, 0.0f);
			full.Render(); /* Full (Middle) */
			GL.Translate(1.0f, 0.0f, 0.0f);
			full.Render(); /* Full (Right) */
			GL.Translate(-2.0f, 0.0f, 1.0f);
			half.Render(); /* Half (left) */
			GL.Translate(2.0f, 0.0f, 0.0f);
			half.Render(); /* Half (Right) */
			GL.Translate(-2.0f, 0.0f, -1.0f);

			if (robotPart != null)
			{
				GL.Translate(1.0f, 1.0f, 0.0f);
				robotPart.RenderAll();
				GL.Translate(-1.0f, -1.0f, 0.0f);
			}

			GL.PopMatrix();
		}
	}
}

