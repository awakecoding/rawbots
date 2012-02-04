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

using Tao.FreeGlut;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	class Game : GameWindow
	{
		Map map;
		int width;
		int height;
		
		public Game() : base(800, 600, GraphicsMode.Default, "Rawbots")
		{
			VSync = VSyncMode.On;

			Glut.glutInit();
			
			width = 50;
			height = 50;
			map = new Map(width, height);
			
			Robot robot;
			
			int x = (width / 2);
			int y = (width / 2);
			
			robot = new Robot(x, y);
			robot.AddWeapon(new NuclearWeapon());
			map.AddRobot(robot);
			
			robot = new Robot(x + 2, y);
			robot.AddWeapon(new PhasersWeapon());
			map.AddRobot(robot);
			
			robot = new Robot(x + 3, y);
			robot.AddElectronics(new Electronics());
			map.AddRobot(robot);
			
			robot = new Robot(x + 4, y);
			robot.AddChassis(new AntiGravChassis());
			map.AddRobot(robot);
			
			Factory factory;
			
			factory = new PhasersWeaponFactory(x + 5, y + 2);
			map.AddFactory(factory);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			
			GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
			GL.Enable(EnableCap.DepthTest);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float) Math.PI / 4, Width / (float) Height, 1.0f, 64.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();
            else if (Keyboard[Key.F1])
                map.SetRenderMode(RenderMode.SOLID_WIRE);
            else if (Keyboard[Key.F2])
                map.SetRenderMode(RenderMode.SOLID);
            else if (Keyboard[Key.F3])
                map.SetRenderMode(RenderMode.WIRE);
            else if (Keyboard[Key.F4])
                ReferencePlane.setVisibleAxis(ReferencePlane.XYZ);
            else if (Keyboard[Key.F5])
                ReferencePlane.setVisibleAxis(ReferencePlane.XZ);
            else if (Keyboard[Key.F6])
                ReferencePlane.setVisibleAxis(ReferencePlane.XY);
            else if (Keyboard[Key.F7])
                ReferencePlane.setVisibleAxis(ReferencePlane.NONE);

            Camera.OnCameraFrame(this);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);
			
			GL.LoadIdentity();
            
            GL.Translate(0.0f, 0.0f, -10.0f);

            Camera.OnCameraUpdate();

            ReferencePlane.setDimensions(50, 50);
            ReferencePlane.render();

            TeamNumber.render();
			
			map.Render();
			
			GL.Flush();
			
			SwapBuffers();
		}

		[STAThread]
		static void Main()
		{
			using (Game game = new Game())
			{
				game.Run(60.0);
			}
		}
	}
}
