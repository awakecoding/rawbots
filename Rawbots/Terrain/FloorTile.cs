using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rawbots
{
	class FloorTile : Tile
	{
		public FloorTile()
		{
			PosX = PosY = 0;
			model = new OBJModel(Game.resourcePath + "/Floor/Tile.obj");
		}

		public FloorTile(int x, int y)
		{
			PosX = x;
			PosY = y;
			model = new OBJModel(Game.resourcePath + "/Floor/Tile.obj");
		}
	}
}
