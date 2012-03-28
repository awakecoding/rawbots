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
using System.IO;
using System.Collections;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using Tao.OpenGl;

using QuickFont;

namespace Rawbots
{
	class Game : GameWindow
	{
		Map map;
		int mapWidth;
		int mapHeight;
		bool useFonts = false;
        bool cameraEnabled = true;
		string baseTitle = "Rawbots";

		QFont font;
		QFont monoFont;

		Config config;
		public static string resourcePath;

		Robot activeRobot;

		Camera camera;
		Camera globalCamera = new Camera(0.0f, 10.0f, 25.0f);
		RobotCamera robotCamera = new RobotCamera(0.0f, 1.0f, 0.0f);

        Camera lightCamera1, lightCamera2, lightCamera3, lightCamera4;

		bool cameraHelp = false;
		
		int renderModeCount;
		int shadingModeCount;

		float[] ambientLight = {1.0f, 1.0f, 1.0f}; // white bright light
		bool ambientLights = true;
		
		bool bottomLeftCornerLight = true;
		bool bottomRightCornerLight = true;
		bool topRightCornerLight = true;
		bool topLeftCornerLight = true;
		bool allPostLights = true;

		string cameraHelpText =
			"W: Move Up\r\n" +
			"A: Move Left\r\n" +
			"S: Move Down\r\n" +
			"D: Move Right\r\n" +
			"Q: Roll Left\r\n" +
			"E: Roll Right\r\n" +
			"Left: Rotate Left\r\n" +
			"Right: Rotate Right\r\n" +
			"Up: Rotate Down\r\n" +
			"Down: Rotate Down\r\n" +
			"Tab: Change Camera\r\n" +
			"Escape: Exit Game\r\n";

		Glu.GLUquadric quadric;
		Texture tex;

		public Game() : base(800, 600, GraphicsMode.Default, "Rawbots")
		{
			VSync = VSyncMode.On;

			renderModeCount = 0;
			shadingModeCount = 0;

			if (IsWindows())
				useFonts = true;

			config = Config.Load();

			this.Width = config.ScreenWidth;
			this.Height = config.ScreenHeight;

			if (config.Fullscreen)
				this.WindowState = WindowState.Fullscreen;

			resourcePath = DetectResourcePath();

			mapWidth = 50;
			mapHeight = 50;
			map = new Map(mapWidth, mapHeight);

			camera = globalCamera;
			Mouse.Move += new EventHandler<MouseMoveEventArgs>(OnMouseMove);
			Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(OnKeyDown);
			Keyboard.KeyUp += new EventHandler<KeyboardKeyEventArgs>(OnKeyUp);
			
			Console.WriteLine("{0}", resourcePath);

			if (useFonts)
			{
				font = new QFont(resourcePath + "/Fonts/Ubuntu-R.ttf", 16);
				font.Options.Colour = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
				font.Options.DropShadowActive = false;
	
				monoFont = new QFont(resourcePath + "/Fonts/UbuntuMono-R.ttf", 16);
				monoFont.Options.Colour = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
				monoFont.Options.DropShadowActive = false;
			}

			GL.Disable(EnableCap.Texture2D);

			Robot robot;

            int x = 0;
            int y = 0;
			
			robot = new Robot(x + 1, y + 1);
			robot.AddChassis(new BipodChassis());
			robot.AddWeapon(new MissilesWeapon());
            map.AddRobot(robot);

            Light light = new Light(LightName.Light7);
            robot.AddLight(light);

			activeRobot = robot;
			robotCamera.Attach(robot);

			robot = new Robot(x + 3, y + 1);
			robot.AddWeapon(new NuclearWeapon());
			map.AddRobot(robot);
			
			robot = new Robot(x + 5, y + 1);
			robot.AddWeapon(new PhasersWeapon());
			map.AddRobot(robot);

            robot = new Robot(x + 7, y + 1);
            robot.AddWeapon(new MissilesWeapon());
            map.AddRobot(robot);

            robot = new Robot(x + 9, y + 1);
            robot.AddWeapon(new CannonWeapon());
            map.AddRobot(robot);

            robot = new Robot(x + 11, y + 1);
            robot.AddChassis(new AntiGravChassis());
            map.AddRobot(robot);

            robot = new Robot(x + 13, y + 1);
            robot.AddChassis(new TrackedChassis());
            map.AddRobot(robot);

            robot = new Robot(x + 15, y + 1);
            robot.AddChassis(new BipodChassis());
            map.AddRobot(robot);

            Tile tile = new LightRubbleTile();
            map.SetTile(tile, x + 17, y + 1);

            tile = new MediumRubbleTile();
            map.SetTile(tile, x + 19, y + 1);

            tile = new HeavyRubbleTile();
            map.SetTile(tile, x + 21, y + 1);

            Pit pit = new Pit();
            pit.setVisible(Pit.NORTH);
            map.SetTile(pit, x + 23, y + 1);

            pit = new Pit();
            pit.setVisible(Pit.EAST);
            map.SetTile(pit, x + 25, y + 1);

            pit = new Pit();
            pit.setVisible(Pit.WEST);
            map.SetTile(pit, x + 27, y + 1);

            pit = new Pit();
            pit.setVisible(Pit.SOUTH);
            map.SetTile(pit, x + 29, y + 1);

            pit = new Pit();
            pit.setVisible(Pit.EAST + Pit.WEST);
            map.SetTile(pit, x + 31, y + 1);

            pit = new Pit();
            pit.setVisible(Pit.NORTH + Pit.SOUTH);
            map.SetTile(pit, x + 33, y + 1);
			
			Factory factory;
			
			factory = new AntiGravChassisFactory(x + 2, y + 5);
			map.AddFactory(factory);

            factory = new BipodChassisFactory(x + 7, y + 5);
            map.AddFactory(factory);

            factory = new CannonWeaponFactory(x + 12, y + 5);
            map.AddFactory(factory);

            factory = new ElectronicsFactory(x + 17, y + 5);
            map.AddFactory(factory);

            factory = new MissilesWeaponFactory(x + 22, y + 5);
            map.AddFactory(factory);

            factory = new NuclearWeaponFactory(x + 27, y + 5);
            map.AddFactory(factory);

            factory = new PhasersWeaponFactory(x + 32, y + 5);
            map.AddFactory(factory);

            factory = new TrackedChassisFactory(x + 37, y + 5);
            map.AddFactory(factory);

            Base b = new Base(x + 45, y + 5);
            map.AddBase(b);

            Block block = new Block(false, x + 35, y + 1);
            map.AddBlock(block);

            block = new Block(true, x + 37, y + 1);
            map.AddBlock(block);

            BlockSquareHole bsh = new BlockSquareHole(false, x + 39, y + 1);
            map.AddBlock(bsh);

            bsh = new BlockSquareHole(true, x + 41, y + 1);
            map.AddBlock(bsh);

			Boundary boundary = new Boundary();

			for (int i = 1; i < mapHeight - 1; i++)
			{
				map.SetTile(boundary, x, y + i);
				map.SetTile(boundary, x + i, y + mapHeight - 1);
				map.SetTile(boundary, x + mapHeight - 1, y + i);
			}
			
            robot = new Robot(x + 10, y + 10);
            robot.AddChassis(new TrackedChassis());
            robot.AddWeapon(new MissilesWeapon());
            robot.AddElectronics(new Electronics());

            map.AddRobot(robot);
			
			LightPost lightpost = new LightPost(4);
			map.SetTile(lightpost, x, y);

            light = new Light(LightName.Light0);
            light.setCutOff(45.0f);
            light.lookAt(0.0f, 6.0f, 0.0f,
                         2.0f*(float)Math.Sqrt(2.0f), 0.0f, -2.0f*(float)Math.Sqrt(2.0f),
                         1.0f, 1.0f, 1.0f);
            
            lightpost.AddLight(light);

            lightCamera1 = new Camera(0.0f, 6.0f, 0.0f,
                         2.0f * (float)Math.Sqrt(2.0f), 0.0f, -2.0f * (float)Math.Sqrt(2.0f),
                         0.0f, 1.0f, 0.0f);

            lightpost = new LightPost(3);
			map.SetTile(lightpost, x + 49, y);

            light = new Light(LightName.Light1);
            light.setCutOff(45.0f);
            light.lookAt(0.0f, 6.0f, 0.0f,
                         -2.0f * (float)Math.Sqrt(2.0f), 0.0f, -2.0f * (float)Math.Sqrt(2.0f),
                        0.0f, 0.0f, 1.0f);
            lightpost.AddLight(light);

            lightCamera2 = new Camera(49.0f, 6.0f, 0.0f,
                         49.0f - 2.0f * (float)Math.Sqrt(2.0f), 0.0f, -2.0f * (float)Math.Sqrt(2.0f),
                         0.0f, 1.0f, 0.0f);

			lightpost = new LightPost(2);
			map.SetTile(lightpost, x + 49, y + 49);

            light = new Light(LightName.Light2);
            light.setCutOff(45.0f);
            light.lookAt(0.0f, 6.0f, 0.0f,
                         -2.0f * (float)Math.Sqrt(2.0f), 0.0f, 2.0f * (float)Math.Sqrt(2.0f),
                         1.0f, 1.0f, 1.0f);
            lightpost.AddLight(light);

            lightCamera3 = new Camera(49.0f, 6.0f, -49.0f,
                         49.0f - 2.0f * (float)Math.Sqrt(2.0f), 0.0f, -49.0f + 2.0f * (float)Math.Sqrt(2.0f),
                         0.0f, 1.0f, 0.0f);

			lightpost = new LightPost(1);
			map.SetTile(lightpost, x, y + 49);

            light = new Light(LightName.Light3);
            light.setCutOff(45.0f);
            light.lookAt(0.0f, 6.0f, 0.0f,
                         2.0f * (float)Math.Sqrt(2.0f), 0.0f, 2.0f * (float)Math.Sqrt(2.0f),
                         1.0f, 1.0f, 1.0f);
            lightpost.AddLight(light);

            lightCamera4 = new Camera(0.0f, 6.0f, -49.0f,
                         2.0f * (float)Math.Sqrt(2.0f), 0.0f, -49.0f + 2.0f * (float)Math.Sqrt(2.0f),
                         0.0f, 1.0f, 0.0f);

            this.Title = this.baseTitle;

			PrintHelp();

			GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Texture2D);

			quadric = Glu.gluNewQuadric();
			Glu.gluQuadricNormals(quadric, Glu.GLU_SMOOTH);  
			Glu.gluQuadricTexture(quadric, Gl.GL_TRUE);
			tex = Texture.AcquireTexture(Game.resourcePath + "/space3.jpg");
		}

		public static bool IsWindows()
		{
			int platform = (int) Environment.OSVersion.Platform;

			if ((platform == 4) || (platform == 6) || (platform == 128))
				return false;

			return true;
		}

		public static string GetPathSeparator()
		{
			if (IsWindows())
				return "\\";
			else
				return "/";
		}

		private string DetectResourcePath()
		{
			string path;
			string parent;
			string separator;
			string cwd = Directory.GetCurrentDirectory();

			separator = GetPathSeparator();

			path = cwd + separator + "Resources";

			if (Directory.Exists(path))
				return path;

			parent = Directory.GetParent(cwd).FullName;
			path = parent + separator + "Resources";

			if (Directory.Exists(path))
				return path;

			parent = Directory.GetParent(parent).FullName;
			path = parent + separator + "Resources";

			if (Directory.Exists(path))
				return path;

			return cwd;
		}

		public void PrintHelp()
		{
            Console.WriteLine("Press ESC to Quit Program.");
            Console.WriteLine("R to toggle between Wire/Solid, Solid and Wire Render Modes");
			Console.WriteLine("T to toggle between Flat and Smooth Shading Modes");
			Console.WriteLine("Y to turn the ambient lights on and off");
			Console.WriteLine("U to turn the bottom left lightpost on and off");
			Console.WriteLine("I to turn the bottom right lightpost on and off");
			Console.WriteLine("O to turn the top right lightpost on and off");
			Console.WriteLine("P to turn the top left lightpost on and off");
			Console.WriteLine("L to turn the all lightposts on and off");
			Console.WriteLine("B to switch between Sky Box and Sky Sphere");
            Console.WriteLine("F4 (Show XYZ Plane), F5 (Show XZ Plane), F6 (Show XY Plane), F7 (Show Nothing)");
            Console.WriteLine("F11 (Enable Camera), F12 (Disable Camera)");
			Console.WriteLine("Camera Controls, W/S (Up/Down), A/D (Left/Right), Q/E (Roll L/R)");
			Console.WriteLine("I/K (Adv Forward), J/L (Strafe L/R)");
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

			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float) Math.PI / 4, Width / (float) Height, 1.0f, /*64.0f*/120.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
		}

		public void OnMouseMove(object sender, MouseMoveEventArgs args)
		{
			camera.MouseDeltaMotion(args.XDelta, args.YDelta);
		}

		public void OnKeyDown(object sender, KeyboardKeyEventArgs args)
		{
			switch (args.Key)
			{
				case Key.H:
					cameraHelp = (cameraHelp) ? false : true;
					break;

				case Key.Tab:

                    if (camera == globalCamera)
                        camera = robotCamera;
                    else if (camera == robotCamera)
                        camera = lightCamera1;
                    else if (camera == lightCamera1)
                        camera = lightCamera2;
                    else if (camera == lightCamera2)
                        camera = lightCamera3;
                    else if (camera == lightCamera3)
                        camera = lightCamera4;
                    else if (camera == lightCamera4)
                        camera = globalCamera;
					break;

				case Key.F:
					if (Keyboard[Key.ControlLeft])
					{
						/* toggle fullscreen */

						if (this.WindowState == WindowState.Fullscreen)
							this.WindowState = WindowState.Normal;
						else
							this.WindowState = WindowState.Fullscreen;
					}
					else
					{
						/* toggle light */
						camera.PerformActions(Camera.Action.TOGGLE_LIGHT);
					}
					break;

				case Key.Number5:
					if (ambientLights)
					{
						ambientLights = false;
						setGlobalAmbientLight(0.0f, 0.0f, 0.0f, 1.0f);
					}
					else
					{
						ambientLights = true;
						setGlobalAmbientLight(0.2f, 0.2f, 0.2f, 1.0f);
					}
					break;

				case Key.Number6:
					if (bottomLeftCornerLight)
					{
						bottomLeftCornerLight = true;
						GL.Disable(EnableCap.Light0);
					}
					else
					{
						bottomLeftCornerLight = true;
						GL.Enable(EnableCap.Light0);
					}
					break;

				case Key.Number7:
					if (bottomRightCornerLight)
					{
						bottomRightCornerLight = true;
						GL.Disable(EnableCap.Light1);
					}
					else
					{
						bottomRightCornerLight = true;
						GL.Enable(EnableCap.Light1);
					}
					break;

				case Key.Number8:
					if (topRightCornerLight)
					{
						topRightCornerLight = true;
						GL.Disable(EnableCap.Light2);
					}
					else
					{
						topRightCornerLight = true;
						GL.Enable(EnableCap.Light2);
					}
					break;

				case Key.Number9:
					if (topLeftCornerLight)
					{
						topLeftCornerLight = true;
						GL.Disable(EnableCap.Light3);
					}
					else
					{
						topLeftCornerLight = true;
						GL.Enable(EnableCap.Light3);
					}
					break;

				case Key.Number0:
					if (allPostLights)
					{
						allPostLights = false;
						GL.Disable(EnableCap.Light3);
						GL.Disable(EnableCap.Light2);
						GL.Disable(EnableCap.Light1);
						GL.Disable(EnableCap.Light0);
					}
					else
					{
						allPostLights = true;
						GL.Enable(EnableCap.Light3);
						GL.Enable(EnableCap.Light2);
						GL.Enable(EnableCap.Light1);
						GL.Enable(EnableCap.Light0);
					}
					break;

				case Key.J:
					camera.PerformActions(Camera.Action.TILT_LEFT | Camera.Action.ACTIVE);
					break;

				case Key.L:
					camera.PerformActions(Camera.Action.TILT_RIGHT | Camera.Action.ACTIVE);
					break;

				case Key.I:
					camera.PerformActions(Camera.Action.TILT_UP | Camera.Action.ACTIVE);
					break;

				case Key.K:
					camera.PerformActions(Camera.Action.TILT_DOWN | Camera.Action.ACTIVE);
					break;
				case Key.B:
					SkyBoxSphere.ChangeEnvironment();
					break;
			}
		}
		
		public void OnKeyUp(object sender, KeyboardKeyEventArgs args)
		{
				switch (args.Key)
				{
					case Key.R:
						renderModeCount = ++renderModeCount % 3;

						switch (renderModeCount)
						{
							case 0:
								map.SetRenderMode(RenderMode.SOLID_WIRE);
								break;
						
							case 1:
								map.SetRenderMode(RenderMode.SOLID);
								break;

							case 2:
								map.SetRenderMode(RenderMode.WIRE);
								break;
						}
						break;
				
					case Key.T:
						shadingModeCount = ++shadingModeCount % 2;
					
						switch (shadingModeCount)
						{
							case 0:
								GL.ShadeModel(ShadingModel.Flat);
								break;

							case 1:
								GL.ShadeModel(ShadingModel.Smooth);
								break;
						}
						break;

					case Key.J:
						camera.PerformActions(Camera.Action.TILT_LEFT);
						break;

					case Key.L:
						camera.PerformActions(Camera.Action.TILT_RIGHT);
						break;

					case Key.I:
						camera.PerformActions(Camera.Action.TILT_UP);
						break;

					case Key.K:
						camera.PerformActions(Camera.Action.TILT_DOWN);
						break;

					case Key.Z:
						map.HideTextures();
						break;
					case Key.X:
						map.ShowTextures();
						break;
				}
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();
            else if (Keyboard[Key.F4])
                ReferencePlane.setVisibleAxis(ReferencePlane.XYZ);
            else if (Keyboard[Key.F5])
                ReferencePlane.setVisibleAxis(ReferencePlane.XZ);
            else if (Keyboard[Key.F6])
                ReferencePlane.setVisibleAxis(ReferencePlane.XY);
            else if (Keyboard[Key.F7])
                ReferencePlane.setVisibleAxis(ReferencePlane.NONE);
            else if (Keyboard[Key.F11])
                cameraEnabled = false;
            else if (Keyboard[Key.F12])
                cameraEnabled = true;

			if (cameraEnabled)
			{
				Camera.Action action = Camera.Action.NONE;

				if (Keyboard[Key.Up])
					action |= Camera.Action.MOVE_UP;
				if (Keyboard[Key.Down])
					action |= Camera.Action.MOVE_DOWN;
				if (Keyboard[Key.Left])
					action |= Camera.Action.MOVE_LEFT;
				if (Keyboard[Key.Right])
					action |= Camera.Action.MOVE_RIGHT;
				if (Keyboard[Key.W])
					action |= Camera.Action.ROTATE_UP;
				if (Keyboard[Key.S])
					action |= Camera.Action.ROTATE_DOWN;
				if (Keyboard[Key.D])
					action |= Camera.Action.ROTATE_RIGHT;
				if (Keyboard[Key.A])
					action |= Camera.Action.ROTATE_LEFT;
				if (Keyboard[Key.Q])
					action |= Camera.Action.ROLL_LEFT;
				if (Keyboard[Key.E])
					action |= Camera.Action.ROLL_RIGHT;

                if (action != Camera.Action.NONE)
                    camera.PerformActions(action);
                else
                    camera.IdleAction();
			}	
		}

		float[] globLight = { 1.0f, 1.0f, 1.0f, 1.0f };

        public void setGlobalAmbientLight(float r, float g, float b, float a)
        {
            globLight[0] = r; globLight[1] = g; globLight[2] = b; globLight[3] = a;
        }

		protected override void OnRenderFrame(FrameEventArgs e)
		{
            int startTime = Environment.TickCount & Int32.MaxValue;

			base.OnRenderFrame(e);

            GL.LightModel(LightModelParameter.LightModelAmbient, globLight);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.MatrixMode(MatrixMode.Modelview);

			camera.SetView();
		
			SkyBoxSphere.Render(camera);
            
			ReferencePlane.setDimensions(50, 50);
            ReferencePlane.render();

            map.Render();

			int totalTime = (Environment.TickCount & Int32.MaxValue) - startTime;

			int fps = 0;

			if (totalTime > 0)
				fps = 1000 / totalTime;

			Title = this.baseTitle + " FPS: " + fps;
			
			if (useFonts)
			{
				QFont.Begin();
				
				GL.PushMatrix();
				GL.Translate(0.0, 0.0, 0.0);
				font.Print(Title, QFontAlignment.Left);
				GL.PopMatrix();
	
				if (cameraHelp)
				{
					GL.PushMatrix();
					GL.Translate(config.ScreenWidth * 0.75, 0.0, 0.0);
					monoFont.Print(cameraHelpText, QFontAlignment.Left);
					GL.PopMatrix();
				}
				
				QFont.End();
				GL.Disable(EnableCap.Texture2D);
			}

			GL.Flush();

			SwapBuffers();
        }

		[STAThread]
		static void Main()
		{
			using (Game game = new Game())
			{
				game.Run();
			}
		}
	}
}
