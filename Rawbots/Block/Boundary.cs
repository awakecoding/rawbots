using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class Boundary : Tile
    {
		bool transparency = false; //transparency attribute

        public Boundary()
        {
			PosX = PosY = 0;
			model = new OBJModel(Game.resourcePath + "/Boundary/Boundary.obj");
        }

		public Boundary(int x, int y)
		{
			this.PosX = x;
			this.PosY = y;
			model = new OBJModel(Game.resourcePath + "/Boundary/Boundary.obj");
		}

		public Boundary(int x, int y, bool trans)
		{
			transparency = trans;
			this.PosX = x;
			this.PosY = y;
			model = new OBJModel(Game.resourcePath + "/Boundary/Boundary.obj");
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);
        }

		public override bool IsCollideable()
		{
			return true;
		}

		public override void Render()
		{
			if (!transparency)
				base.Render();
			else
			{
				GL.Disable(EnableCap.Lighting);
				GL.Disable(EnableCap.DepthTest);
				GL.Enable(EnableCap.Blend);

				GL.Color4(0.3f, 0.3f, 0.3f, 0.5f);
				model.Render();

				GL.Disable(EnableCap.Blend);
				GL.Enable(EnableCap.DepthTest);
				GL.Enable(EnableCap.Lighting);
			}
		}
    }
}
