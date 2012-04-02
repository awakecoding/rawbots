
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

		//private double cylinderRadius; //antenna
		//private double cylinderHeight;
		//private CylinderModel cylinder;

		//private CubeModel cube;
		//private float bodyWidth;
		//private float bodyHeight;

		//private float topWidth;
		//private float topHeight;

		//private float peripheralWidth;
		//private float peripheralHeight;

		//private OBJModel model;

        public RemoteControlUnit()
        {
			//bodyWidth = 2.0f;
			//bodyHeight = 0.5f;

			//topWidth = 1.0f;
			//topHeight = 0.1f;

			//peripheralWidth = 1.0f;
			//peripheralHeight = 0.5f;

			//cylinderHeight = 1.0f;
			//cylinderRadius = 0.08f;
			//cylinder = new CylinderModel(cylinderRadius, cylinderHeight);
			//cylinder.SetColor(0.3f, 0.3f, 0.3f);

			//cube = new CubeModel();
			
			//material = new Material(Material.MaterialType.SHINY_STEEL);
			//cube.AssignMaterial(material);
			//cylinder.AssignMaterial(material);

			model = new OBJModel(Game.resourcePath + "/AI/Brain_AI.obj");

			robotList = new List<Robot>();
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            //cylinder.SetRenderMode(renderMode);
            //cube.SetRenderMode(renderMode);
        }

		//public override void HideTextures

		public void AddRobot(Robot r)
		{
			robotList.Add(r);
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

		public void AttachLight(Light l)
		{
			light = l;
			light.setCutOff(45.0f);
			light.setDirection(0.0f, -0.45f, -0.45f);
		}

        public void Render()
        {
			Robot robotToGrab = null;

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
					Console.WriteLine("Robot " + i + " hit");
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

			if (MovingLeft)
			{
				PosX -= 0.1f;
				light.setDirection(-0.45f, -0.45f, -0.45f);
				Console.WriteLine("RMC (" + PosX + "," + PosY + ")");
			}

			if (MovingRight)
			{
				PosX += 0.1f;
				light.setDirection(0.45f, -0.45f, -0.45f);
				Console.WriteLine("RMC (" + PosX + "," + PosY + ")");
			}

			if (MovingUp)
			{
				PosY += 0.1f;
				light.setDirection(0.0f, -0.45f, -0.45f);
				Console.WriteLine("RMC (" + PosX + "," + PosY + ")");
			}

			if (MovingDown)
			{
				PosY -= 0.1f;
				light.setDirection(0.0f, -0.45f, 0.45f);
				Console.WriteLine("RMC (" + PosX + "," + PosY + ")");
			}

			if (light != null)
			{
				light.setPosition(PosX, HoverHeight, -PosY, 1.0f);
				light.apply();
			}

			GL.Translate(PosX, HoverHeight, -PosY);

			GL.PushMatrix();
			GL.Scale(0.07f, 0.07f, 0.07f);
			model.Render();
			GL.PopMatrix();

			////Scaling to unit size
			//GL.Scale(0.33, 0.33, 0.33);

			//cube.SetColor(0.5f, 0.5f, 0.5f);
			////main component
			//GL.PushMatrix();
			//GL.Scale(bodyWidth, bodyHeight, bodyWidth);
			//cube.render(1.0);
			//GL.PopMatrix();

			//cube.SetColor(0.8f, 0.8f, 0.8f);
			////top component
			//GL.PushMatrix();
			//GL.Translate(0.0, bodyHeight / 2 + topHeight / 2, 0.0);
			//GL.Scale(topWidth, topHeight, topWidth);
			//cube.render(1.0);
			//GL.PopMatrix();

			////antenna
			//GL.PushMatrix();
			//GL.Translate(0.0, bodyHeight / 2, 0.0);
			//GL.Rotate(-90.0, 1.0, 0.0, 0.0);
			//cylinder.render();
			//GL.PopMatrix();

			//cube.SetColor(0.4f, 0.4f, 0.4f);
			////peripheral components
			//GL.PushMatrix();
			//GL.Translate(-bodyWidth / 2, 0.0, bodyWidth / 2);
			//GL.Scale(peripheralWidth, peripheralHeight, peripheralWidth);
			//cube.render(1.0);
			//GL.PopMatrix();

			//GL.PushMatrix();
			//GL.Translate(-bodyWidth / 2, 0.0, -bodyWidth / 2);
			//GL.Scale(peripheralWidth, peripheralHeight, peripheralWidth);
			//cube.render(1.0);
			//GL.PopMatrix();

			//GL.PushMatrix();
			//GL.Translate(bodyWidth / 2, 0.0, bodyWidth / 2);
			//GL.Scale(peripheralWidth, peripheralHeight, peripheralWidth);
			//cube.render(1.0);
			//GL.PopMatrix();

			//GL.PushMatrix();
			//GL.Translate(bodyWidth / 2, 0.0f, -bodyWidth / 2);
			//GL.Scale(peripheralWidth, peripheralHeight, peripheralWidth);
			//cube.render(1.0);
			//GL.PopMatrix();

			//cube.SetColor(0.9f, 0.9f, 0.9f);
			////Team Number
			//GL.PushMatrix();
			//GL.Translate(bodyWidth / 2 + peripheralWidth / 4, peripheralHeight / 2, bodyWidth / 2 + peripheralWidth / 4);
			//GL.Rotate(-90.0f, 1.0f, 0.0f, 0.0f);
			//GL.Scale(0.3f, 0.3f, 0.3f);
			//TeamNumber.Render();
			//GL.PopMatrix();

			Hovering = false;
			MovingLeft = false;
			MovingRight=false;
			MovingUp = false;
			MovingDown = false;
        }
    }
}

