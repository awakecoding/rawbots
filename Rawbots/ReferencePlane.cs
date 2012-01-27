using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class ReferencePlane
    {
        public static int width;
        public static int height;

        public static void setDimensions(int width, int height)
        {
            ReferencePlane.width = width;
            ReferencePlane.height = height;
        }

        public static void render()
        {
            GL.PushMatrix();

 //           GL.LoadIdentity();

            GL.Color3(0.71f, 0.71f, 0.71f);

            GL.Translate(-width / 2 * 1.0f, 0.0f, height / 2 * 1.0f);

            for (int i = 0; i < width; i++)
			{
                for (int j = 0; j < height; j++)
                {
                    GL.Translate(i * 1.0f, 0.0f, j * -1.0f);
					
                    GL.Begin(BeginMode.LineLoop);
                        GL.Vertex3(0.0f, 0.0f, 0.0f);
                        GL.Vertex3(1.0f, 0.0f, 0.0f);
                        GL.Vertex3(1.0f, 0.0f, -1.0f);
                        GL.Vertex3(0.0f, 0.0f, -1.0f);
                    GL.End();
					
                    GL.Translate(-i * 1.0f, 0.0f, j * 1.0f);
                }
			}

            GL.PopMatrix();
            GL.PushMatrix();

            GL.Translate(0.0f, -width / 2 * 1.0f, height / 2 * 1.0f);
            GL.Rotate(90.0f, 0.0f, 0.0f, 1.0f);

            for (int i = 0; i < width; i++)
			{
                for (int j = 0; j < height; j++)
                {
                    GL.Translate(i * 1.0f, 0.0f, j * -1.0f);
					
                    GL.Begin(BeginMode.LineLoop);
	                    GL.Vertex3(0.0f, 0.0f, 0.0f);
	                    GL.Vertex3(1.0f, 0.0f, 0.0f);
	                    GL.Vertex3(1.0f, 0.0f, -1.0f);
	                    GL.Vertex3(0.0f, 0.0f, -1.0f);
                    GL.End();
                    
					GL.Translate(-i * 1.0f, 0.0f, j * 1.0f);
                }
			}

            GL.PopMatrix();
        }

    }

    
}
