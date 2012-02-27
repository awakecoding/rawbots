using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public class Material
    {
		public enum MaterialType {
			SHINY_STEEL, 
			ROCK_DIFFUSE, 
			DIFFUSE_GRAY
		};
		
		float[] shinySteel_ambient = {0.1f, 0.1f, 0.03f, 1.0f};
		float[] shinySteel_diffuse = {0.1f, 0.1f, 0.0f, 1.0f};
		float[] shinySteel_specular = {0.1f, 0.1f, 0.1f, 1.0f};
		float[] shinySteel_shininess = {40.0f};
		float[] shinySteel_emission = {0.0f, 0.0f, 0.0f, 0.1f};
		
		float[] rockDiffuse_ambient = {0.4f, 0.4f, 0.3f, 1.0f};
		float[] rockDiffuse_diffuse = {0.6f, 0.9f, 0.9f, 1.0f};
		float[] rockDiffuse_specular = {0.0f, 0.0f, 0.0f, 0.0f};
		float[] rockDiffuse_shininess = {20.0f};
		float[] rockDiffuse_emission = {0.0f, 0.0f, 0.0f, 0.1f};
		
		float[] diffuseGray_ambient = {0.1f, 0.1f, 0.1f, 1.0f};
		float[] diffuseGray_diffuse = {0.8f, 0.8f, 0.8f, 1.0f};
		float[] diffuseGray_specular = {0.99f, 0.91f, 0.81f, 1.0f};
		float[] diffuseGray_shininess = {10.0f};
		float[] diffuseGray_emission = {0.0f, 0.0f, 0.0f, 0.1f};
		
        static float[] DefaultAmbient = { 0.2f, 0.2f, 0.2f, 1.0f };
        static float[] DefaultDiffuse = { 0.8f, 0.8f, 0.8f, 1.0f };
        static float[] DefaultSpecular = { 0.0f, 0.0f, 0.0f, 0.0f };
        static float[] DefaultShine = { 0.0f };
        static float[] DefaultEmission = { 0.0f, 0.0f, 0.0f, 1.0f };
        
        float[] Ambient;
        float[] Diffuse;
        float[] Specular;
        float[] Shine;
        float[] Emission;

        public Material()
        {
            Ambient = new float[] { DefaultAmbient[0], DefaultAmbient[1], DefaultAmbient[2], DefaultAmbient[3] };
            Diffuse = new float[] { DefaultDiffuse[0], DefaultDiffuse[1], DefaultDiffuse[2], DefaultDiffuse[3] };
            Specular = new float[] { shinySteel_specular[0], shinySteel_specular[1], shinySteel_specular[2], shinySteel_specular[3] };
            Shine = new float[] { shinySteel_shininess[0] };
            Emission = new float[] { DefaultEmission[0], DefaultEmission[1], DefaultEmission[2], DefaultEmission[3] };
        }
		
		public Material(MaterialType my_material)
        {
            switch (my_material)
			{
				case MaterialType.SHINY_STEEL:
					Ambient = new float[] { shinySteel_ambient[0], shinySteel_ambient[1], shinySteel_ambient[2], shinySteel_ambient[3] };
           	 		Diffuse = new float[] { shinySteel_diffuse[0], shinySteel_diffuse[1], shinySteel_diffuse[2], shinySteel_diffuse[3] };
            		Specular = new float[] { shinySteel_specular[0], shinySteel_specular[1], shinySteel_specular[2], shinySteel_specular[3] };
            		Shine = new float[] { shinySteel_shininess[0] };
            		Emission = new float[] { shinySteel_emission[0], shinySteel_emission[1], shinySteel_emission[2], shinySteel_emission[3] };
				break;
  
				case MaterialType.ROCK_DIFFUSE:
					Ambient = new float[] { rockDiffuse_ambient[0], rockDiffuse_ambient[1], rockDiffuse_ambient[2], rockDiffuse_ambient[3] };
           	 		Diffuse = new float[] { rockDiffuse_diffuse[0], rockDiffuse_diffuse[1], rockDiffuse_diffuse[2], rockDiffuse_diffuse[3] };
            		Specular = new float[] { rockDiffuse_specular[0], rockDiffuse_specular[1], rockDiffuse_specular[2], rockDiffuse_specular[3] };
            		Shine = new float[] { rockDiffuse_shininess[0] };
            		Emission = new float[] { rockDiffuse_emission[0], rockDiffuse_emission[1], rockDiffuse_emission[2], rockDiffuse_emission[3] };
					break;
  
				case MaterialType.DIFFUSE_GRAY:
					Ambient = new float[] { diffuseGray_ambient[0], diffuseGray_ambient[1], diffuseGray_ambient[2], diffuseGray_ambient[3] };
           	 		Diffuse = new float[] { diffuseGray_diffuse[0], diffuseGray_diffuse[1], diffuseGray_diffuse[2], diffuseGray_diffuse[3] };
            		Specular = new float[] { diffuseGray_specular[0], diffuseGray_specular[1], diffuseGray_specular[2], diffuseGray_specular[3] };
            		Shine = new float[] { diffuseGray_shininess[0] };
            		Emission = new float[] { rockDiffuse_emission[0], rockDiffuse_emission[1], rockDiffuse_emission[2], rockDiffuse_emission[3] };
					break;
			
				default:
					GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, DefaultAmbient);
            		GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, DefaultDiffuse);
            		GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, DefaultSpecular);
            		GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, DefaultShine);
            		GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, DefaultEmission);
					break;
			}
        }

        public void setAmbient(float r, float g, float b, float a)
        {
            Ambient[0] = r; Ambient[1] = g; Ambient[2] = b; Ambient[3] = a;
        }

        public void setDiffuse(float r, float g, float b, float a)
        {
            Diffuse[0] = r; Diffuse[1] = g; Diffuse[2] = b; Diffuse[3] = a;
        }

        public void setSpecular(float r, float g, float b, float a)
        {
            Specular[0] = r; Specular[1] = g; Specular[2] = b; Specular[3] = a;
        }

        public void setShine(float exp)
        {
            Shine[0] = exp;
        }

        public void setEmission(float r, float g, float b, float a)
        {
            Emission[0] = r; Emission[1] = g; Emission[2] = b; Emission[3] = a;
        }

        public void apply()
        {
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, Ambient);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, Diffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, Specular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, Shine);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, Emission);
        }
		
		/*public void apply_material(MaterialType my_material)
        {
            switch (my_material)
			{
				case MaterialType.SHINY_STEEL:
					material(shinySteel_ambient, shinySteel_diffuse, shinySteel_specular, shinySteel_shininess);
					break;
  
				case MaterialType.ROCK_DIFFUSE:
					material(rockDiffuse_ambient, rockDiffuse_diffuse, rockDiffuse_specular, rockDiffuse_shininess);
					break;
  
				case MaterialType.DIFFUSE_GRAY:
					material(diffuseGray_ambient, diffuseGray_diffuse, diffuseGray_specular, diffuseGray_shininess);
					break;
			
				default:
					GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, DefaultAmbient);
            		GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, DefaultDiffuse);
            		GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, DefaultSpecular);
            		GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, DefaultShine);
            		GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, DefaultEmission);
					break;
			}
        }*/

        public static void unapply()
        {
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, DefaultAmbient);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, DefaultDiffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, DefaultSpecular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, DefaultShine);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, DefaultEmission);
        }
		
		void material(float[] ambient, float[] diffuse, float[] specular, float shininess)
		{
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, ambient);
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, diffuse);
            	GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, specular);
            	GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, shininess);
		}
		
    }
}
