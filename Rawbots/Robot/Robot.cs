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
	public class Robot : Model
	{
		Chassis chassis;
		Weapon[] weapons;
		Electronics electronics;

		private Light light;
		private bool lightOn;

		private float posX;
		private float posY;
		private int mapPosX;
		private int mapPosY;
		private float finalTotalHeight = 0.0f;

		private int life = 100; //life meter

		private bool friend = false; //Identifies whether friend or foe

		private int waitTime = 4000; //Wait for 4 seconds
		private int startTime = 0;

		private const int INIT = 0;
		private const int INIT_WAITING = 1;
		private const int WAITING = 2;
		private const int INIT_MOVING = 3;
		private const int MOVING = 4;
		private const int POSSESSED = 5;
		private int state = INIT;
		private int prevState;

		public int MapPosX
		{
			get { return mapPosX; }
			
			set
			{
				mapPosX = value;
				posX = (float) mapPosX;
			}
		}

		public int MapPosY
		{
			get { return mapPosY; }

			set
			{
				mapPosY = value;
				posY = (float) (-1 * mapPosY);
			}
		}

		public float PosX
		{
			get { return posX; }

			set
			{
				posX = value;
				mapPosX = (int) posX;
			}
		}

		public float PosY
		{
			get { return posY; }

			set
			{
				posY = value;
				mapPosY = (int) ((-1.0) * posY);
			}
		}

		public float Angle { get; set; }

		private void Init()
		{
			chassis = null;
			weapons = null;
			electronics = null;
			lightOn = false;
			Angle = 0.0f;

			light = new Light(LightName.Light2);
		}
		
		public Robot()
		{
			Init();
			MapPosX = 0;
			MapPosY = 0;
		}
		
		public Robot(int x, int y)
		{
			Init();			
			MapPosX = x;
			MapPosY = y;
		}

		public void UpdateState()
		{
			int currTime = 0;

			switch (state)
			{ 
				case INIT:
					state = INIT_WAITING;
					break;
				case INIT_WAITING:
					startTime = Environment.TickCount & Int32.MaxValue;
					state = WAITING;
					break;
				case WAITING:
					currTime = Environment.TickCount & Int32.MaxValue;
					if (currTime - startTime > waitTime)
						state = INIT_MOVING;
					break;
				case INIT_MOVING:
					//Here we try to find the shortest path to our friend
					//We will use Dijktra's algorithm to find it

					break;
				case MOVING:
					break;
				case POSSESSED:
					break;
			}
		}

		public void ToggleLight()
		{
			lightOn = (lightOn) ? false : true;
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

        public void AddLight(Light l)
        {
            light = l;
            lightOn = true;
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
			if (!show)
				return;

			Push();
			Render();
			Pop();
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            if (chassis != null)
                chassis.SetRenderMode(renderMode);

            if (weapons != null)
			{
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i].SetRenderMode(renderMode);
			}
            
            if (electronics != null)
                electronics.SetRenderMode(renderMode);
        }

		public override void ShowTextures()
		{
			if (chassis != null)
				chassis.ShowTextures();

			if (weapons != null)
				for (int i = 0; i < weapons.Length; i++)
					weapons[i].ShowTextures();

			if (electronics != null)
				electronics.ShowTextures();
		}

		public override void HideTextures()
		{
			if (chassis != null)
				chassis.HideTextures();

			if (weapons != null)
				for (int i = 0; i < weapons.Length; i++)
					weapons[i].HideTextures();

			if (electronics != null)
				electronics.HideTextures();
		}

		public Projectile FireProjectile(float dirx, float diry)
		{
			Projectile projectile = new Projectile(this, PosX, PosY, dirx, diry);

			return projectile;
		}

		public void Render()
		{
			GL.Translate(PosX * 1.0f, 0.0f, PosY * 1.0f);

			if (!show)
				return;

			GL.PushMatrix();

            float totalHeight = 0.0f;

			GL.Rotate(Angle, 0.0f, 1.0f, 0.0f);

            if (chassis != null)
            {
                Push();
                totalHeight += chassis.GetHeight();
                chassis.RenderAll();
                Pop();
            }

            if (weapons != null)
			{
                for (int i = 0; i < weapons.Length; i++)
                {
                    Weapon w = weapons[i];
					
                    if (w != null)
                    {
                        Push();
                        GL.Translate(0.0f, totalHeight, 0.0f);
                        totalHeight += w.GetHeight();
                        w.RenderAll();
                        Pop();
                    }
                }
			}
			
			if (electronics != null)
			{
				Push();
                GL.Translate(0.0f, totalHeight, 0.0f);
				totalHeight += electronics.GetHeight();
                electronics.RenderAll();
				Pop();
			}

            if (lightOn)
            {
                if (light != null)
                {
                    light.setCutOff(45.0f);
                    light.lookAt(0.0f, totalHeight/2, -1.0f,
                                    0.0f, 0.0f, -(totalHeight/2)/(float)Math.Tan((20.0f*Math.PI)/180.0f),
                                    0.0f, 1.0f, 0.0f);
                    light.apply();
                }
            }

			finalTotalHeight = totalHeight;

			GL.PopMatrix();

			GL.Translate(PosX * 1.0f, 0.0f, PosY * -1.0f);
		}

		public void MarkFriendly()
		{
			friend = true;
		}

		public bool IsFriendly()
		{
			return friend;
		}

		public bool IsDead()
		{
			return !(life > 0);
		}

		public bool ProjectileTest(Projectile projectile)
		{
			if (Utility.Collision.IntersectionTest2D(projectile.PosX, projectile.PosY, 0.25f, 0.25f, PosX, PosY, 1.0f, 1.0f) && projectile.self != this)
			{
				Console.WriteLine("Robot got hit!");
				life -= 10;
				return true;
			}

			return false;
		}

		public bool RobotCollisionTest(Map map, float x, float y)
		{
			if (map.IsColliding((int)Math.Ceiling(x), (int)Math.Ceiling(y)))
				return true;
			if (map.IsColliding((int)Math.Floor(x), (int)Math.Floor(y)))
				return true;

			return false;
		}

		public override bool IsCollideable()
		{
			return true;
		}
	}
}

