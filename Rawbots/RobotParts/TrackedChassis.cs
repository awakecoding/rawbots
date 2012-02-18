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
	public class TrackedChassis : Chassis
	{

		private const double radius = 0.1;
		private const double height = 0.2;
		
		private CubeModel[] tcComponent;
		private CylinderModel[] wheelEnd;
		private const int bottomEnd = 0;
		private const int topEnd = 1;
		private const int TOTAL_ENDS = 2;
		
		public TrackedChassis()
		{
			/* 
			 * Considerably more manoeuvrable than
			 * bipods but twice the resource units.
			 */
			
			wheelEnd = new CylinderModel[TOTAL_ENDS];
            for (int i = 0; i < TOTAL_ENDS; i++)
                 wheelEnd[i] = new CylinderModel(radius, height);
			
			tcComponent = new CubeModel[TOTAL_ENDS];
            for (int i = 0; i < TOTAL_ENDS; i++)
                 tcComponent[i] = new CubeModel();

		}

        /*public override void setRenderMode(int mode)
        {
            
        }*/

		public override void Render()
		{
			//Team Number
            GL.PushMatrix();
			
            GL.Translate(0.0f, 0.1f, 0.0f);
            GL.Scale(0.2f, 0.2f, 0.2f);
            GL.Rotate(-90.0, 1.0f, 0.0f, 0.0f);

            TeamNumber.render();
            
			GL.PopMatrix();
			GL.PushMatrix();

			GL.Scale(0.6f, 0.1f, 1.0f);
			
			// Draw the middle part of the chassis.
			tcComponent[topEnd].render(1.0f);
			
			GL.PopMatrix ();
			GL.PushMatrix();
			
			GL.Translate(-0.4f, 0.0f, 0.0f);
			
			drawWheel();
			
			GL.PopMatrix();
			GL.PushMatrix();
			
			GL.Translate(0.4f, 0.0f, 0.0f);
			
			drawWheel();
			
			GL.PopMatrix();
		}
		
		public void drawWheel()
		{
			GL.PushMatrix();
			
			GL.Translate(-0.1f, 0.0f,0.5f);
			GL.Rotate(90.0f, 0.0f, 1.0f, 0.0f);
			wheelEnd[topEnd].SetColor(0.22f, 0.3f, 0.5f);
			
			//Draw the other round end of the wheel.
			wheelEnd[topEnd].render();
			
			GL.PopMatrix();
			GL.PushMatrix();
			
			GL.Translate(-0.1f, 0.0f, -0.5f);
			GL.Rotate(90.0f, 0.0f, 1.0f, 0.0f);
			wheelEnd[bottomEnd].SetColor(0.22f, 0.3f, 0.5f);
			
			// Draw one round end of the wheel.
			wheelEnd[bottomEnd].render();
			
			GL.PopMatrix();
			GL.PushMatrix();
			
			GL.Scale(0.2f, 0.2f, 1.0f);
			tcComponent[bottomEnd].SetColor(0.22f, 0.3f, 0.5f);
			
			// Draw the rectangular part of the wheel.
			tcComponent[bottomEnd].render(1.0f);
			
			GL.PopMatrix();
		}
			
	}
}