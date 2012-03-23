using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class BlockSquareHole : Block
    {
		static OBJModel fullModel, halfModel;

		static BlockSquareHole()
		{
			fullModel = new OBJModel(Game.resourcePath + "/Obstacles/full_block.obj");
			halfModel = new OBJModel(Game.resourcePath + "/Obstacles/stripped_half.obj");
		}

        public BlockSquareHole(bool half) : base(half)
        {
			//model = new OBJModel(Game.resourcePath + "/Obstacles/full_block.obj");
		}

        public BlockSquareHole(bool half, int x, int y)
            : base(half, x, y)
        {
			//model = new OBJModel(Game.resourcePath + "/Obstacles/full_block.obj");
		}

        public override void Render()
        {
            GL.PushMatrix();

			if (half)
				halfModel.Render();
			else
				fullModel.Render();

            GL.PopMatrix();
        }
    }
}
