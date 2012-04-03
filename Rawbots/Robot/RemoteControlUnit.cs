
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
using System.Collections.Generic;

namespace Rawbots
{
    public class RemoteControlUnit : Model
    {
        public float PosX { get; set; }
        public float PosY { get; set; }
		public bool Hovering = false;
		public bool MovingLeft = false;
		public bool MovingRight = false;
		public bool MovingUp = false;
		public bool MovingDown = false;
		public bool GrabRobot = false;
		public float HoverHeight = 0.1f;
		public float MIN_HOVER_HEIGHT = 0.2f;
		public float MAX_HOVER_HEIGHT = 3.0f;

		public Light light;
		public List<Robot> robotList;

        public RemoteControlUnit()
        {
			PosX = PosY = 0.0f;
			model = new OBJModel(Game.resourcePath + "/AI/Brain_AI.obj");
			robotList = new List<Robot>();
		}

		public RemoteControlUnit(int x, int y)
		{
			PosX = x;
			PosY = y;
			model = new OBJModel(Game.resourcePath + "/AI/Brain_AI.obj");
			robotList = new List<Robot>();
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);
        }

		public void AddRobot(Robot robot)
		{
			robotList.Add(robot);
		}

		public void Hover()
		{
			Hovering = true;
		}

		public void MoveLeft()
		{
			MovingLeft = true;
		}

		public void MoveRight()
		{
			MovingRight = true;
		}

		public void MoveUp()
		{
			MovingUp = true;
		}

		public void MoveDown()
		{
			MovingDown = true;
		}

		public void GrabARobot()
		{
			GrabRobot = true;
		}

		public Robot GetRobot()
		{
			return robotToGrab;
		}

		public void AttachLight(Light light)
		{
			this.light = light;
			this.light.setCutOff(45.0f);
			this.light.setDirection(0.0f, -0.45f, -0.45f);
		}
		
		Robot robotToGrab;

        public void Render()
        {
			robotToGrab = null;

			for (int i = 0; i < robotList.Count; i++)
			{
				Robot r = robotList[i];

				float invY = -(r.PosY);

				float[] UpperLeft = new float[] { PosX, PosY };
				float[] UpperRight = new float[] { PosX + 1.0f, PosY };
				float[] LowerLeft = new float[] { PosX, PosY + 1.0f };
				float[] LowerRight = new float[] { PosX + 1.0f, PosY + 1.0f };

				//if (UpperLeft[0] >= r.PosX && UpperLeft[0] <= r.PosX + 1.0f)
				//    if (UpperLeft[1] >= invY && UpperLeft[1] <= invY + 1.0f)
				//        Console.WriteLine("Robot Hit Upper Left");

				//if (UpperRight[0] >= r.PosX && UpperRight[0] <= r.PosX + 1.0f)
				//    if (UpperRight[1] >= invY && UpperRight[1] <= invY + 1.0f)
				//        Console.WriteLine("Robot Hit Upper Right");

				//if (LowerRight[0] >= r.PosX && LowerRight[0] <= r.PosX + 1.0f)
				//    if (LowerRight[1] >= invY && LowerRight[1] <= invY + 1.0f)
				//        Console.WriteLine("Robot Hit Lower Right");

				//if (LowerLeft[0] >= r.PosX && LowerLeft[0] <= r.PosX + 1.0f)
				//    if (LowerLeft[1] >= invY && LowerLeft[1] <= invY + 1.0f)
				//        Console.WriteLine("Robot Hit Lower Left");

				if (Utility.Collision.IntersectionTest2D(PosX, PosY, 1.0f, 1.0f,
													 r.PosX, invY, 1.0f, 1.0f))
				{
					//Console.WriteLine("Robot " + i + " hit");
					robotToGrab = robotList[i];
				}
			}

			float minHeight = MIN_HOVER_HEIGHT;

			if (Hovering)
			{
				if (HoverHeight <= MAX_HOVER_HEIGHT)
					HoverHeight += 0.1f;
			}
			else
			{
				if (robotToGrab != null)
					minHeight += robotToGrab.GetHeight();

				if (HoverHeight > minHeight)
					HoverHeight -= 0.1f;
			}

			float rotation = 0.0f;

			if (MovingLeft)
			{
				PosX -= 0.1f;
				light.setDirection(-0.45f, -0.45f, -0.45f);
				//Console.WriteLine("RMC (" + PosX + "," + PosY + ")");
				rotation = 90.0f;
			}

			if (MovingRight)
			{
				PosX += 0.1f;
				light.setDirection(0.45f, -0.45f, -0.45f);
				//Console.WriteLine("RMC (" + PosX + "," + PosY + ")");
				rotation = -90.0f;
			}

			if (MovingUp)
			{
				PosY += 0.1f;
				light.setDirection(0.0f, -0.45f, -0.45f);
				//Console.WriteLine("RMC (" + PosX + "," + PosY + ")");
			}

			if (MovingDown)
			{
				PosY -= 0.1f;
				light.setDirection(0.0f, -0.45f, 0.45f);
				//Console.WriteLine("RMC (" + PosX + "," + PosY + ")");
				rotation = 180.0f;
			}

			if (light != null)
			{
				light.setPosition(PosX, HoverHeight, -PosY, 1.0f);
				light.apply();
			}

			if (robotToGrab != null && GrabRobot && robotToGrab.IsFriendly())
			{
				//Console.WriteLine("Grabbing Robot");
				robotToGrab.PosX = PosX; robotToGrab.PosY = -PosY;
				robotToGrab.Angle = rotation;
			}

			GL.Translate(PosX, HoverHeight, -PosY);

			GL.PushMatrix();
			GL.Scale(0.07f, 0.07f, 0.07f);
			model.Render();
			GL.PopMatrix();

			GrabRobot = false;
			Hovering = false;
			MovingLeft = false;
			MovingRight = false;
			MovingUp = false;
			MovingDown = false;
        }
    }
}

