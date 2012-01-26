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
	public class NuclearWeapon : Weapon
	{
		public NuclearWeapon()
		{
		}
		
		public override int getCost()
		{
			return 20;
		}
		
		public override int getRange()
		{
			return 8;
		}
		
		public override int getLethality()
		{
			/*
			 * Nuclear weapons destroy all robots and factories within an 8 mile
			 * radius of the robot carrying the device - this includes the carrying robot.
			 * This is currently the only method we have to destroy factories and war bases!
			 */
			
			return 0;
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

