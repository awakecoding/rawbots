// Released to the public domain. Use, modify and relicense at will.

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

		/// <summary>Load resources here.</summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
			GL.Enable(EnableCap.DepthTest);
		}

		/// <summary>
		/// Called when your window is resized. Set your viewport here. It is also
		/// a good place to set up your projection matrix (which probably changes
		/// along when the aspect ratio of your window).
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
		}

		/// <summary>
		/// Called when it is time to setup the next frame. Add you game logic here.
		/// </summary>
		/// <param name="e">Contains timing information for framerate independent logic.</param>
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			if (Keyboard[Key.Escape])
				Exit();
		}

		/// <summary>
		/// Called when it is time to render the next frame. Add your rendering code here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);
			
			GL.LoadIdentity();
			//GL.Scale(0.5f,0.5f,1.0f);
			
			foreach (Robot robot in robots)
			{
				robot.RenderAll();
				GL.Translate(0.5f, 0.0f, -2.0f);
			}
			
			SwapBuffers();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
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
