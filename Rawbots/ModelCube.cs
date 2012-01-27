using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using Tao.FreeGlut;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public class ModelCube
    {
        public const int OUTLINED_SOLID = 0;
        public const int SOLID = 1;
        public const int WIRE = 2;
        private int mode = OUTLINED_SOLID;
		
        private float colorR = 0.22f;
        private float colorG = 0.22f;
        private float colorB = 0.22f;

        private float colorWR = 1.0f;
        private float colorWG = 0.0f;
        private float colorWB = 0.0f;

        public void setRenderMode(int mode)
        {
            this.mode = mode;
        }
		
        public void setColor(float r, float g, float b)
        {
            colorR = r;
            colorG = g;
            colorB = b;
        }

        public void setWireColor(float r, float g, float b)
        {
            colorWR = r;
            colorWG = g;
            colorWB = b;
        }
		
		private void renderSolidCube(float size)
		{
			size /= 2;
			
			GL.PushMatrix();
			
			GL.Begin(BeginMode.Quads);
		
				GL.Normal3( 1.0, 0.0, 0.0);
				GL.Vertex3(+size,-size,+size);
				GL.Vertex3(+size,-size,-size);
				GL.Vertex3(+size,+size,-size);
				GL.Vertex3(+size,+size,+size);
		
				GL.Normal3( 0.0, 1.0, 0.0);
				GL.Vertex3(+size,+size,+size);
				GL.Vertex3(+size,+size,-size);
				GL.Vertex3(-size,+size,-size);
				GL.Vertex3(-size,+size,+size);
		
				GL.Normal3( 0.0, 0.0, 1.0);
				GL.Vertex3(+size,+size,+size);
				GL.Vertex3(-size,+size,+size);
				GL.Vertex3(-size,-size,+size);
				GL.Vertex3(+size,-size,+size);
		
				GL.Normal3(-1.0, 0.0, 0.0);
				GL.Vertex3(-size,-size,+size);
				GL.Vertex3(-size,+size,+size);
				GL.Vertex3(-size,+size,-size);
				GL.Vertex3(-size,-size,-size);
		
				GL.Normal3( 0.0,-1.0, 0.0);
				GL.Vertex3(-size,-size,+size);
				GL.Vertex3(-size,-size,-size);
				GL.Vertex3(+size,-size,-size);
				GL.Vertex3(+size,-size,+size);
		
				GL.Normal3( 0.0, 0.0,-1.0);
				GL.Vertex3(-size,-size,-size);
				GL.Vertex3(-size,+size,-size);
				GL.Vertex3(+size,+size,-size);
				GL.Vertex3(+size,-size,-size);
			
			GL.End();
			
			GL.PopMatrix();
		}
		
		private void renderWireCube(float size)
		{
			size /= 2;
			
			GL.PushMatrix();
			
			GL.Begin(BeginMode.LineLoop);
				GL.Normal3( 1.0, 0.0, 0.0);
				GL.Vertex3(+size,-size,+size);
				GL.Vertex3(+size,-size,-size);
				GL.Vertex3(+size,+size,-size);
				GL.Vertex3(+size,+size,+size);
			GL.End();
		
			GL.Begin(BeginMode.LineLoop);
				GL.Normal3( 0.0, 1.0, 0.0);
				GL.Vertex3(+size,+size,+size);
				GL.Vertex3(+size,+size,-size);
				GL.Vertex3(-size,+size,-size);
				GL.Vertex3(-size,+size,+size);
			GL.End();
		
			GL.Begin(BeginMode.LineLoop);
				GL.Normal3( 0.0, 0.0, 1.0);
				GL.Vertex3(+size,+size,+size);
				GL.Vertex3(-size,+size,+size);
				GL.Vertex3(-size,-size,+size);
				GL.Vertex3(+size,-size,+size);
			GL.End();
		
			GL.Begin(BeginMode.LineLoop);
				GL.Normal3(-1.0, 0.0, 0.0);
				GL.Vertex3(-size,-size,+size);
				GL.Vertex3(-size,+size,+size);
				GL.Vertex3(-size,+size,-size);
				GL.Vertex3(-size,-size,-size);
			GL.End();
		
			GL.Begin(BeginMode.LineLoop);
				GL.Normal3( 0.0,-1.0, 0.0);
				GL.Vertex3(-size,-size,+size);
				GL.Vertex3(-size,-size,-size);
				GL.Vertex3(+size,-size,-size);
				GL.Vertex3(+size,-size,+size);
			GL.End();
		
			GL.Begin(BeginMode.LineLoop);
				GL.Normal3( 0.0, 0.0,-1.0);
				GL.Vertex3(-size,-size,-size);
				GL.Vertex3(-size,+size,-size);
				GL.Vertex3(+size,+size,-size);
				GL.Vertex3(+size,-size,-size);
			GL.End();
			
			GL.PopMatrix();
		}
		
        public void render(float size)
        {
            switch (mode)
            {
                case OUTLINED_SOLID:
                    GL.Color3(colorR, colorG, colorB);
                    renderSolidCube(size);
                    GL.Color3(colorWR, colorWG, colorWB);
                	renderWireCube(size);
                break;
				
                case SOLID:
                    GL.Color3(colorR, colorG, colorB);
					renderSolidCube(size);
                break;
				
                case WIRE:
					GL.Color3(colorR, colorG, colorB);
					renderWireCube(size);
                break;
            }
        }

    }
}
