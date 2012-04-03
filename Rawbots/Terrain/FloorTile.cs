using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	class FloorTile : Tile
	{
		bool occupied = false;

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

		public void MarkOccupied()
		{
			occupied = true;
		}

		public void MarkUnOccupied()
		{
			occupied = false;
		}

		public override bool IsCollideable()
		{
			return occupied;		 	 
		}

		public override void Render()
		{
			if (!occupied)
				base.Render();
			else
			{
				GL.Color3(0.0f, 1.0f, 1.0f);
				model.Render();
			}
		}
	}
}
