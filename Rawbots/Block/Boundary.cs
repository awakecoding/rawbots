using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class Boundary : Tile
    {
        public Boundary()
        {
			PosX = PosY = 0;
			model = new OBJModel(Game.resourcePath + "/Boundary/Boundary.obj");
        }

		public Boundary(int x, int y)
		{
			this.PosX = y;
			this.PosY = y;
			model = new OBJModel(Game.resourcePath + "/Boundary/Boundary.obj");
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);
        }

        public override void Render()
        {
			model.Render();
        }

		public override bool IsCollideable()
		{
			return true;
		}
    }
}
