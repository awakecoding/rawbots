using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class LightPost : Tile
    {
		int position;

        public LightPost(int x)
        {
			position = x;
			model = new OBJModel(Game.resourcePath + "/Lightpost/Lightpost2.obj");
        }

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);
        }

		private void DrawLightPost()
		{
			GL.PushMatrix();
			model.Render();
			GL.PopMatrix();
		}
		
        public override void Render()
        {
			GL.PushMatrix();

            Light light = GetLight();
            
			if (light != null)
                light.apply();

			base.Render();
			
			switch(position)
			{
				/* top-left corner of tile */
				case 1:
					GL.PushMatrix();
					GL.Translate (-0.35f, 0.0f, -0.35f);
                    DrawLightPost();
                    GL.PopMatrix ();
					break;
				
				/* top-right corner of tile */
				case 2:
					GL.PushMatrix();
					GL.Translate (0.35f, 0.0f, -0.35f);
                    DrawLightPost();
                    GL.PopMatrix ();
					break;
				
				/* bottom-right corner of tile */
				case 3:
					GL.PushMatrix();
					GL.Translate (0.35f, 0.0f, 0.35f);
                    DrawLightPost();
                    GL.PopMatrix ();
					break;
				
				/* top-right corner of tile */
				case 4:
					GL.PushMatrix();
					GL.Translate (-0.35f, 0.0f, 0.35f);
                    DrawLightPost();
                    GL.PopMatrix ();
					break;
				
				default:            
            		Console.WriteLine("Invalid selection. Please select 1 to draw a lightpost in the top-left corner of the tile" +
            							'\n' + 	"2 to draw a lightpost in the top-right of the tile" +
				                  		'\n' + "3 to draw a lightpost in the bottom-right of the tile" +
				                  		'\n' + "and 4 to draw a lightpost in the bottom-left of the tile");           
            		break; 
			}

            if (light != null)
                light.unapply();

			GL.PopMatrix();
        }
    }
}

