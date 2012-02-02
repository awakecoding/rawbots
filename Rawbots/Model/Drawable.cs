using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public abstract class Drawable : Model
    {
        public void Push()
        {
            GL.PushMatrix();
        }

        public void Pop()
        {
            GL.PopMatrix();
        }

        public void RenderAll()
        {
            Push();
            Render();
            Pop();
        }

        public abstract void Render();
    }
}
