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

namespace Rawbots
{
	public class Terrain
	{		
		public Tile[,] tiles;
		public Tile[,] Tiles { get { return tiles; } }
		
		public Terrain(int width, int height)
		{
			tiles = new Tile[width, height];
			
			for (int i = 0; i < tiles.GetLength(0); i++)
			{
				for (int j = 0; j < tiles.GetLength(1); j++)
				{
					tiles[i,j] = new Tile();
				}
			}
		}
		
		public void BeginRender()
		{
            GL.PushMatrix();
            GL.LineWidth(2.5f);
		}
		
		public void EndRender()
		{
			GL.LineWidth(1.0f);
            GL.PopMatrix();
		}
		
		public void RenderTile(int x, int y)
		{
			tiles[x, y].RenderAll();
		}
	}
}

