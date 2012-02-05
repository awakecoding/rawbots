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
		
		private ModelCube[] tcComponent;
		private ModelCylinder[] wheelEnd;
		private const int bottomEnd = 0;
		private const int topEnd = 1;
		private const int TOTAL_ENDS = 2;
		
		public TrackedChassis()
		{
			/* 
			 * Considerably more manoeuvrable than
			 * bipods but twice the resource units.
			 */
			
			wheelEnd = new ModelCylinder[TOTAL_ENDS];
            for (int i = 0; i < TOTAL_ENDS; i++)
                 wheelEnd[i] = new ModelCylinder();
			
			tcComponent = new ModelCube[TOTAL_ENDS];
            for (int i = 0; i < TOTAL_ENDS; i++)
                 tcComponent[i] = new ModelCube();

		}

        public override void setRenderMode(int mode)
        {
            
        }

		public override void Render()
		{
			/*GL.Begin(BeginMode.Triangles);

			GL.Color3(1.0f, 1.0f, 0.0f);
			GL.Vertex3(-1.0f, -1.0f, 4.0f);
			GL.Color3(1.0f, 0.0f, 0.0f);
			GL.Vertex3(1.0f, -1.0f, 4.0f);
			GL.Color3(0.2f, 0.9f, 1.0f);
			GL.Vertex3(0.0f, 1.0f, 4.0f);
			
			GL.End();*/
			
			GL.PushMatrix();
			
			GL.Scale(0.5f, 0.5f, 1.0f);
			
			tcComponent[topEnd].render(1.0f);
			
			GL.PopMatrix ();
			GL.PushMatrix();
			
			GL.Translate(-0.5f, 0.0f, 0.0f);
			
			drawWheel();
			
			GL.PopMatrix();
			GL.PushMatrix();
			
			GL.Translate(0.5f, 0.0f, 0.0f);
			
			drawWheel();
			
			GL.PopMatrix();
		}
		
		public void drawWheel()
		{
			GL.PushMatrix();
			
			GL.Translate(0.0f, 0.0f,1.0f);
			
			wheelEnd[topEnd].render(0.5, 1.0, 8, 1);
			
			GL.PopMatrix();
			GL.PushMatrix();
			
			GL.Translate (0.0f, 0.0f, -1.0f);
			
			wheelEnd[bottomEnd].render(0.5, 1.0,  8, 1);
			
			GL.PopMatrix();
			GL.PushMatrix();
			
			GL.Scale(0.25f, 1.0f, 1.0f);
			
			tcComponent[bottomEnd].render(1.0f);
			
			GL.PopMatrix();
		}
			
	}
}