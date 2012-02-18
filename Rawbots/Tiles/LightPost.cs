using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class LightPost : Tile
    {
        public CubeModel cube;
		public CylinderModel cylinder;
		public SphereModel sphere;
		
		int position;
		
        public LightPost(int x)
        {
            cube = new CubeModel();
            //cube.SetColor(0.2f, 0.2f, 0.2f);
			
			cylinder = new CylinderModel(0.08f, 6.0f);
			cylinder.SetColor (0.3f, 0.3f, 0.3f);
			
			sphere = new SphereModel(1.0f);
			sphere.SetColor(0.0f, 0.0f, 0.0f);
			
			position = x;
			
        }

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            cube.SetRenderMode(renderMode);
        }

			
		private void drawLightPost()
		{
			sphere.SetColor(0.1f, 0.1f, 0.1f);
			GL.PushMatrix();
			GL.Translate(0.0f, 5.60f, 0.0f);
			GL.Scale(0.2f, 0.1f, 0.2f);
			sphere.render();
			GL.PopMatrix();
			
			
			//bulb
			sphere.SetColor(1.0f, 1.0f, 1.0f);
			GL.PushMatrix();
			GL.Translate(0.0f, 5.85f, 0.0f);
			GL.Scale(0.3f, 0.3f, 0.3f);
			sphere.render();
			GL.PopMatrix();
			
			sphere.SetColor(0.1f, 0.1f, 0.1f);
			GL.PushMatrix();
			GL.Translate(0.0f, 1.2f, 0.0f);
			GL.Scale(0.2f, 0.05f, 0.2f);
			sphere.render();
			GL.PopMatrix();
			
			cube.SetColor(0.0f, 0.0f, 0.0f);
            GL.PushMatrix();
            GL.Translate(0.0f, 0.7f, 0.0f);
            GL.Scale(0.25f, 1.0f, 0.25f);
            cube.render(1.0f);
            GL.PopMatrix();
            
			//pole
			cylinder.SetColor(0.1f, 0.1f, 0.1f);
			GL.PushMatrix();
            GL.Rotate(-90.0f, 1.0f, 0.0f, 0.0f);
            cylinder.render();
            GL.PopMatrix();
            
			//base
			cube.SetColor(0.1f, 0.1f, 0.1f);
			GL.PushMatrix();
            GL.Translate(0.0f, 0.1f, 0.0f);
            GL.Scale(0.3f, 0.2f, 0.3f);
            cube.render(1.0f);
            GL.PopMatrix();
		}
		
        public override void Render()
        {
			base.Render();
			
			switch(position)
			{
				//top-left corner of tile
				case 1:
					GL.PushMatrix();
					GL.Translate (-0.35f, 0.0f, -0.35f);
					drawLightPost();
					GL.PopMatrix ();
					break;
				
				//top-right corner of tile
				case 2:
					GL.PushMatrix();
					GL.Translate (0.35f, 0.0f, -0.35f);
					drawLightPost();
					GL.PopMatrix ();
					break;
				
				//bottom-right corner of tile
				case 3:
					GL.PushMatrix();
					GL.Translate (0.35f, 0.0f, 0.35f);
					drawLightPost();
					GL.PopMatrix ();
					break;
				
				//top-right corner of tile
				case 4:
					GL.PushMatrix();
					GL.Translate (-0.35f, 0.0f, 0.35f);
					drawLightPost();
					GL.PopMatrix ();
					break;
				
				default:            
            		Console.WriteLine("Invalid selection. Please select 1 to draw a lightpost in the top-left corner of the tile" +
            							'\n' + 	"2 to draw a lightpost in the top-right of the tile" +
				                  		'\n' + "3 to draw a lightpost in the bottom-right of the tile" +
				                  		'\n' + "and 4 to draw a lightpost in the bottom-left of the tile");           
            		break; 
			}
			
            
			

        }
    }
}

