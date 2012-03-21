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

		public void ShowTextures()
		{
			if (chassis != null)
				chassis.ShowTextures();

			if (weapons != null)
				for (int i = 0; i < weapons.Length; i++)
					weapons[i].ShowTextures();

			if (electronics != null)
				electronics.ShowTextures();
		}

		public void HideTextures()
		{
			if (chassis != null)
				chassis.HideTextures();

			if (weapons != null)
				for (int i = 0; i < weapons.Length; i++)
					weapons[i].HideTextures();

			if (electronics != null)
				electronics.HideTextures();
		}

		public void Render()
		{
			if (!show)
				return;

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
                                    0.0f, 0.0f, /*-2.0f*/-(totalHeight/2)/(float)Math.Tan((20.0f*Math.PI)/180.0f),
                                    0.0f, 1.0f, 0.0f);
                    //light.setPosition(PosX, totalHeight, PosY, 1.0f);
                    //light.setDirection(PosX, 0.0f, PosY);
                    light.apply();
                }
            }
		}
	}
}

