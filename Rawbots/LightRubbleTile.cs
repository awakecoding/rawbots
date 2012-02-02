using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class LightRubbleTile : Tile
    {
        public CubeModel cube;

        public LightRubbleTile()
        {
            cube = new CubeModel();
        }

        public override void Render()
        {
            base.Render();

            GL.PushMatrix();

            cube.render(1.0f);

            GL.PopMatrix();
        }
    }
}
