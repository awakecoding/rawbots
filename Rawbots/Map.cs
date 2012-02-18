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
		
		public Terrain Terrain { get { return terrain; } }
		
		public Map(int width, int height)
		{
			this.width = width;
			this.height = height;
			
			terrain = new Terrain(this.width, this.height);
			
			robots = new List<Robot>();
			factories = new List<Factory>();
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
		
        public void SetRenderMode(RenderMode renderMode)
        {
			foreach (Robot robot in robots)
			{
				robot.SetRenderMode(renderMode);
			}
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
        }
	}
}

