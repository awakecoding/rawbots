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
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class Robot
	{
		Chassis chassis;
		Weapon[] weapons;
		Electronics electronics;
		
		public Robot()
		{
			chassis = null;
			weapons = null;
			electronics = null;
		}
		
		public void AddChassis(Chassis chassis)
		{
			this.chassis = chassis;
		}
		
		public void AddWeapon(Weapon weapon)
		{
			if (weapons == null)
			{
				weapons = new Weapon[1];
				weapons[0] = weapon;
			}
			else
			{
				Array.Resize(ref weapons, weapons.Length + 1);
				weapons[weapons.Length - 1] = weapon;
			}
		}
		
		public void AddElectronics(Electronics electronics)
		{
			this.electronics = electronics;
		}
		
		public void Push()
		{
			GL.PushMatrix();
		}
		
		public void Pop()
		{
			GL.PopMatrix();
		}
		
		public void RenderAll()
		{
			Push();
			Render();
			Pop();
		}
		
		public void Render()
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

