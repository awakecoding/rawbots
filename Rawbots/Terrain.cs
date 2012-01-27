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

namespace Rawbots
{
	public class Terrain
	{
		private Tile[,] tiles;
		public Tile[,] Tiles { get; set; }
		
		public Terrain()
		{
			tiles = new Tile[50,50];
			
			for (int i = 0; i < tiles.GetLength(0); i++)
			{
				for (int j = 0; j < tiles.GetLength(1); j++)
				{
					tiles[i,j] = new Tile();
				}
			}
		}
	}
}

