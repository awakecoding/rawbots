using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class BlockSquareHole : Block
    {
        public BlockSquareHole(bool half) : base(half)
        {}

        public BlockSquareHole(bool half, int x, int y)
            : base(half, x, y)
        {}

        public override void Render()
        {
            base.Render();

            GL.PushMatrix();

            getCube().SetColor(0.0f, 0.0f, 0.0f);

            

            if (this.isHalf())
            {
                GL.Translate(0.0f, 0.25f, 0.0f);
                GL.Scale(0.75f, 0.5f, 0.75f);
                GL.Scale(1.0f, 0.5f, 1.0f);
                GL.Translate(0.0f, 0.5f, 0.0f);
            }
            else
            {
                GL.Translate(0.0f, 0.515f, 0.0f);
                GL.Scale(0.75f, 0.5f, 0.75f);
                GL.Translate(0.0f, 0.5f, 0.0f);
            }

            getCube().render(1.0f);

            GL.PopMatrix();
        }

    }
}
