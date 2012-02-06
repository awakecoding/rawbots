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
        RemoteControlUnit rmc;

		public Terrain Terrain { get { return terrain; } }
		
		public Map(int width, int height)
		{
			this.width = width;
			this.height = height;
			
			terrain = new Terrain(this.width, this.height);
			
			robots = new List<Robot>();
			factories = new List<Factory>();
            blocks = new List<Block>();
            rmc = new RemoteControlUnit();
            rmc.PosX = 43;
            rmc.PosY = -1;
		}
		
		public void AddRobot(Robot robot)
		{
			robots.Add(robot);
		}
		
		public void RemoveRobot(Robot robot)
		{
			robots.Remove(robot);
		}
		
		public void AddFactory(Factory factory)
		{
			factories.Add(factory);
		}
		
		public void RemoveFactory(Factory factory)
		{
			factories.Remove(factory);
		}

        public void AddBlock(Block block)
        {
            blocks.Add(block);
        }

        public void SetTile(Tile t, int x, int y)
        {
            terrain.setTile(t, x, y);
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

            TeamNumber.SetRenderMode(renderMode);
            rmc.SetRenderMode(renderMode);
        }

        public void Render()
        {
			/* Render terrain */
			
            GL.PushMatrix();
			
			terrain.BeginRender();
			
            GL.Translate(-terrain.getWidth() / 2.0f, 0.0f, terrain.getHeight() / 2.0f);

            for (int i = 0; i < terrain.getWidth(); i++)
            {
                for (int j = 0; j < terrain.getHeight(); j++)
                {
                    GL.Translate(i * 1.0f, 0.0f, j * -1.0f);
					terrain.RenderTile(i, j);					
                    GL.Translate(-i * 1.0f, 0.0f, j * 1.0f);
                }
            }
            
			terrain.EndRender();
			
            GL.PopMatrix();
			
			/* Render robots */
			
            GL.PushMatrix();
			
            GL.Translate(-width / 2.0f, 0.0f, height / 2.0f);
			
            foreach (Robot robot in robots)
            {
                GL.Translate(robot.PosX * 1.0f, 0.0f, robot.PosY * -1.0f);
                robot.RenderAll();
                GL.Translate(-robot.PosX * 1.0f, 0.0f, robot.PosY * 1.0f);
            }
			
            GL.PopMatrix();
			
			/* Render factories */
			
            GL.PushMatrix();
			
            GL.Translate(-width / 2.0f, 0.0f, height / 2.0f);
			
            foreach (Factory factory in factories)
            {
                GL.Translate(factory.PosX * 1.0f, 0.0f, factory.PosY * -1.0f);
                factory.Render();
                GL.Translate(-factory.PosX * 1.0f, 0.0f, factory.PosY * 1.0f);
            }
			
            GL.PopMatrix();

            /* Render buildings (or blocks)*/

            GL.PushMatrix();

            GL.Translate(-width / 2.0f, 0.0f, height / 2.0f);

            for (int i = 0; i < blocks.Count; i++)
            {
                Block b = blocks[i];
                GL.Translate(b.PosX * 1.0f, 0.0f, b.PosY * -1.0f);
                b.Render();
                GL.Translate(-b.PosX * 1.0f, 0.0f, b.PosY * 1.0f);
            }

            GL.PopMatrix();

            GL.PushMatrix();

            GL.Translate(-width / 2.0f, 0.0f, height / 2.0f);

            rmc.Render();

            GL.PopMatrix();
        }
	}
}

