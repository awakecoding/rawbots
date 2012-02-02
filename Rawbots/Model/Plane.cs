using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public class Plane : Model
    {
        public Plane()
        { }

        private void renderPlane(double size, bool solid)
        {
            size /= 2;
            BeginMode beginMode;

            beginMode = (solid) ? BeginMode.Quads : BeginMode.LineLoop;

            GL.PushMatrix();

            GL.Begin(beginMode);
                GL.Vertex3(-size, 0.0f, -size);
                GL.Vertex3(size, 0.0f, -size);
                GL.Vertex3(size, 0.0f, size);
                GL.Vertex3(-size, 0.0f, size); 
            GL.End();

            GL.PopMatrix();
        }

        public void render(double size)
        {
            switch (renderMode)
            {
                case RenderMode.SOLID:
                    GL.Color3(colorR, colorG, colorB);
                    renderPlane(size, true);
                    break;

                case RenderMode.WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    renderPlane(size, false);
                    break;

                case RenderMode.SOLID_WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    renderPlane(size, true);
                    GL.Color3(wireColorR, wireColorG, wireColorB);
                    renderPlane(size, false);
                    break;
            }
        }
    }
}
