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
		Robot[] robots;
		
		public Game() : base(800, 600, GraphicsMode.Default, "Rawbots")
		{
			VSync = VSyncMode.On;

			Glut.glutInit();

			robots = new Robot[8];
			
			robots[0] = new Robot();
			robots[0].AddWeapon(new NuclearWeapon());
			robots[0].AddWeapon(new PhasersWeapon());
			//robots[0].AddElectronics(new Electronics());
			
			robots[1] = new Robot();
			robots[1].AddWeapon(new PhasersWeapon());
			robots[1].AddElectronics(new Electronics());
		}

        private void setRenderMode(Model.RenderMode renderMode)
        {
            for (int i = 0; i < robots.Length; i++)
			{
                if(robots[i] != null)
                    robots[i].setRenderMode(renderMode);
			}
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
                setRenderMode(Model.RenderMode.SOLID_WIRE);
            else if (Keyboard[Key.F2])
                setRenderMode(Model.RenderMode.SOLID);
            else if (Keyboard[Key.F3])
                setRenderMode(Model.RenderMode.WIRE);

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

            foreach (Robot robot in robots)
            {
                if (robot != null)
                    robot.RenderAll();
				
                GL.Translate(2.5f, 0.0f, 0.0f);
            }
			
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
