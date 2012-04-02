/**
 * RawBots: an awesome robot game
 * 
 * Copyright 2012 Marc-Andre Moreau <marcandre.moreau@gmail.com>
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using OpenTK.Graphics.OpenGL;
using System.Collections;
using System.Collections.Generic;

namespace Rawbots
{
	public class Map
	{
		int width;
		int height;
		Terrain terrain;
		List<Robot> robots;
		List<Factory> factories;
        List<Block> blocks;
        List<Base> bases;
        List<Light> lights;
        RemoteControlUnit remoteControlUnit;

		public Terrain Terrain { get { return terrain; } }
		
		public Map(int width, int height)
		{
			this.width = width;
			this.height = height;

			terrain = new Terrain(this.width, this.height);
			
			robots = new List<Robot>();
			factories = new List<Factory>();
            blocks = new List<Block>();
            bases = new List<Base>();
            lights = new List<Light>();

            remoteControlUnit = new RemoteControlUnit();
			remoteControlUnit.AttachLight(new Light(LightName.Light4));
            remoteControlUnit.PosX = 43;
			remoteControlUnit.PosY = 1;
		}

		public int GetWidth()
		{
			return width;
		}

		public int GetHeight()
		{
			return height;
		}

		public ByteMap GetCollisionMap()
		{
			return terrain.CollisionMap;
		}

		public bool IsColliding(int x, int y)
		{
			bool colliding = terrain.CollisionMap.IsPositionSet(x, y);

			if (colliding)
			{
				Console.WriteLine("collision at {0},{1}", x, y);
			}

			return colliding;
		}

		public void AddRobot(Robot robot)
		{
			robots.Add(robot);
		}
		
		public void RemoveRobot(Robot robot)
		{
			robots.Remove(robot);
		}

		public List<Robot> GetRobots()
		{
			return robots;
		}
		
		public void AddFactory(Factory factory)
		{
			factories.Add(factory);
		}
		
		public void RemoveFactory(Factory factory)
		{
			factories.Remove(factory);
		}

        public void AddLight(Light light)
        {
            lights.Add(light);
        }

        public void AddBlock(Block block)
        {
            blocks.Add(block);
        }

        public void AddBase(Base b)
        {
            bases.Add(b);
        }

		public void SetTerrain(Terrain terrain)
		{
			this.terrain = terrain;
		}

        public void SetTile(Tile tile, int x, int y)
        {
            terrain.SetTile(tile, x, y);
        }

        public void SetRenderMode(RenderMode renderMode)
        {
            for (int i = 0; i < factories.Count; i++)
            {
                Factory f = factories[i];
                f.SetRenderMode(renderMode);
            }
            
            foreach (Robot robot in robots)
			{
				robot.SetRenderMode(renderMode);
			}

            for (int i = 0; i < blocks.Count; i++)
            {
                Block b = blocks[i];
                b.SetRenderMode(renderMode);
            }

            for (int i = 0; i < bases.Count; i++)
            {
                Base b = bases[i];
                b.SetRenderMode(renderMode);
            }

            terrain.SetRenderMode(renderMode);

            TeamNumber.SetRenderMode(renderMode);
            remoteControlUnit.SetRenderMode(renderMode);
        }

		public void ShowTextures()
		{
			foreach (Robot robot in robots)
			{
				robot.ShowTextures();
			}

			terrain.ShowTextures();

			remoteControlUnit.ShowTextures();
		}

		public void HideTextures()
		{
			foreach (Robot robot in robots)
			{
				robot.HideTextures();
			}

			terrain.HideTextures();

			remoteControlUnit.HideTextures();
		}

		public void HoverRMC()
		{
			remoteControlUnit.Hover();
		}

		public void MoveRMCLeft()
		{
			remoteControlUnit.MoveLeft();
		}

		public void MoveRMCRight()
		{
			remoteControlUnit.MoveRight();
		}

		public void MoveRMCUp()
		{
			remoteControlUnit.MoveUp();
		}

		public void MoveRMCDown()
		{
			remoteControlUnit.MoveDown();
		}

		public float[] GetRMCPosition()
		{
			return new float[] { remoteControlUnit.PosX, remoteControlUnit.PosY };
		}

        public void Render()
        {
			GL.PushMatrix();

            /* Render Map */

            GL.PushMatrix();

			GL.Translate(0.0f, 0.0f, 0.0f);

			/* Render terrain */

            GL.PushMatrix();

			terrain.Render();

            GL.PopMatrix();

            /* Render robots */

            GL.PushMatrix();

            foreach (Robot robot in robots)
            {
				GL.Translate(robot.PosX * 1.0f, 0.0f, robot.PosY * 1.0f);
                robot.RenderAll();
				GL.Translate(-robot.PosX * 1.0f, 0.0f, robot.PosY * -1.0f);
            }

            GL.PopMatrix();

            /* Render factories */

            GL.PushMatrix();

            foreach (Factory factory in factories)
            {
                GL.Translate(factory.PosX * 1.0f, 0.0f, factory.PosY * -1.0f);
                factory.Render();
                GL.Translate(-factory.PosX * 1.0f, 0.0f, factory.PosY * 1.0f);
            }

            GL.PopMatrix();

            /* Render buildings (or blocks) */

            GL.PushMatrix();

			foreach (Block block in blocks)
			{
				GL.Translate(block.PosX * 1.0f, 0.0f, block.PosY * -1.0f);
				block.Render();
				GL.Translate(-block.PosX * 1.0f, 0.0f, block.PosY * 1.0f);
			}

            GL.PopMatrix();

            /* Render the bases */

            GL.PushMatrix();
            
			foreach (Base cBase in bases)
			{
				cBase.Render();
			}

            GL.PopMatrix();

            /* Render Remote Control Unit */

            GL.PushMatrix();

            remoteControlUnit.Render();

            GL.PopMatrix();

			GL.PopMatrix();

			//foreach (Light light in lights)
			//    light.unapply();

			GL.PopMatrix();
        }
	}
}

