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
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Input;

namespace Rawbots
{
	class Game : GameWindow
	{
		Robot[] robots;
		
		ElectronicBrain electronicBrain;
		NuclearWarhead nuclearWarhead;
		Phaser phaser;
		Missile missile;
		Cannon cannon;
		AntiGravityChassis antiGravityChassis;
		TrackChassis trackChassis;
		BipodChassis bipodChassis;
		
		public Game() : base(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample")
		{
			VSync = VSyncMode.On;
			
			robots = new Robot[8];
			
			robots[0] = electronicBrain = new ElectronicBrain();
			robots[1] = nuclearWarhead = new NuclearWarhead();
			robots[2] = phaser = new Phaser();
			robots[3] = missile = new Missile();
			robots[4] = cannon = new Cannon();
			robots[5] = antiGravityChassis = new AntiGravityChassis();
			robots[6] = trackChassis = new TrackChassis();
			robots[7] = bipodChassis = new BipodChassis();
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
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);
			
			GL.LoadIdentity();
			//GL.Scale(0.5f,0.5f,1.0f);
			
			GL.Translate(0.0f, 0.0f, -10.0f);
			
			foreach (Robot robot in robots)
			{
				robot.RenderAll();
				GL.Translate(0.5f, 0.0f, 0.0f);
			}
			
			SwapBuffers();
		}

		[STAThread]
		static void Main()
		{
			using (Game game = new Game())
			{
				game.Run(30.0);
			}
		}
	}
}
