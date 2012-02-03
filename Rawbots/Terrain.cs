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
		private static Tile[,] tiles;
		public static Tile[,] Tiles { get; set; }
		
		static Terrain()
        {
            tiles = new Tile[50,50];

            int n = 0;

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    switch(n%4)
                    {
                        case 0:
                            tiles[i, j] = new Tile();
                            break;
                        case 1:
                            tiles[i, j] = new LightRubbleTile();
                            break;
                        case 2:
                            tiles[i, j] = new MediumRubbleTile();
                            break;
                        case 3:
                            tiles[i, j] = new HeavyRubbleTile();
                            break;
                    }

                    n++;
                    //tiles[i,j] = new Tile();
                }
            }
		}

        public static void Render()
        {
            GL.PushMatrix();

            GL.Translate(-tiles.GetLength(0) / 2.0f, 0.0f, tiles.GetLength(1) / 2.0f);
            GL.LineWidth(2.5f);
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    GL.Translate(i * 1.0f, 0.0f, j * -1.0f);
                    tiles[i, j].RenderAll();
                    GL.Translate(-i * 1.0f, 0.0f, j * 1.0f);
                }
            }
            GL.LineWidth(1.0f);
            GL.PopMatrix();
        }
	}
}

