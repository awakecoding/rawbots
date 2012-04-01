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
