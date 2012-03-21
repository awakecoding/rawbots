using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public class Material
    {
		public enum MaterialType {
			SHINY_STEEL, 
			ROCK_DIFFUSE, 
			DIFFUSE_GRAY,
			SILK,
			CONCRETE
		};
		
		float[] shinySteel_ambient = {0.1f, 0.1f, 0.03f, 1.0f};
		float[] shinySteel_diffuse = {0.1f, 0.1f, 0.0f, 1.0f};
		float[] shinySteel_specular = {0.1f, 0.1f, 0.1f, 1.0f};
		float[] shinySteel_shininess = {30.0f};
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
		
		float[] silk_ambient = {0.0f, 0.1f, 0.06f, 1.0f};
		float[] silk_diffuse = {0.01f, 0.01f, 0.01f, 1.0f};
		float[] silk_specular = {0.50f, 0.50f, 0.50f, 1.0f};
		float[] silk_shininess = {32.0f};
		float[] silk_emission = {0.0f, 0.0f, 0.0f, 0.1f};
		
		float[] concrete_ambient = {0.19225f, 0.19225f, 0.19225f, 1.0f};
		float[] concrete_diffuse = {0.50754f, 0.50754f, 0.50754f, 1.0f};
		float[] concrete_specular = {0.508273f, 0.508273f, 0.508273f, 1.0f};
		float[] concrete_shininess = {51.2f};
		float[] concrete_emission = {0.0f, 0.0f, 0.0f, 0.1f};
		
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

		public string matName;

		private string pathFileName;
		private string relativePath = "";

		private Texture texture;

        public Material()
        {
            Ambient = new float[] { DefaultAmbient[0], DefaultAmbient[1], DefaultAmbient[2], DefaultAmbient[3] };
            Diffuse = new float[] { DefaultDiffuse[0], DefaultDiffuse[1], DefaultDiffuse[2], DefaultDiffuse[3] };
            Specular = new float[] { shinySteel_specular[0], shinySteel_specular[1], shinySteel_specular[2], shinySteel_specular[3] };
            Shine = new float[] { shinySteel_shininess[0] };
            Emission = new float[] { DefaultEmission[0], DefaultEmission[1], DefaultEmission[2], DefaultEmission[3] };
        }

		private void useDefault()
		{
			Ambient = DefaultAmbient;
			Diffuse = DefaultDiffuse;
			Specular = DefaultSpecular;
			Shine = DefaultShine;
			Emission = DefaultEmission;
		}

		public static List<Material> ParseMaterials(string filename)
		{
			List<Material> MaterialList = new List<Material>();

			String fileName = filename;

			FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(fs);

			string sLine;
			char[] seperators = new char[] { ' ', '/' };
			string[] s;
			float[] fTemp;

			string absolutePath = fs.Name;
			char[] sep = new char[] { '\\' };
			string[] splitAbsolutePath = absolutePath.Split(sep);

			string relativepath = "";

			for (int i = 0; i < splitAbsolutePath.Length - 1; i++)
				relativepath += splitAbsolutePath[i] + "\\";

			int ic = sr.Read();
			uint lineNumber = 1;

			Material currMaterial = null;

			while (ic != -1)
			{
				char c = (char)ic;

				if (c == 'n') //New Material
				{
					sLine = sr.ReadLine();
					s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

					currMaterial = new Material();

					//matName = s[1];
					currMaterial.setName(s[1]);
					MaterialList.Add(currMaterial);
				}
				else if (c == 'N')
				{
					int iNext = sr.Read();

					if (iNext == 's')
					{
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
						fTemp = new float[s.Length];

						float.TryParse(s[0], out fTemp[0]);

						currMaterial.setShine((fTemp[0]/100.0f)*128.0f);
					}
					else
					{
						sLine = sr.ReadLine();
					}
				}
				else if (c == 'i')
				{
					sLine = sr.ReadLine();

					Console.WriteLine("WARNING: Material File " + filename + " Illumination Parameter Not Supported on Line " + lineNumber);
				}
				else if (c == 'T')
				{
					sLine = sr.ReadLine();

					Console.WriteLine("WARNING: Material File " + filename + " Transmission Filter Parameter Not Supported on Line " + lineNumber);
				}
				else if (c == 'K')
				{
					int iNext = sr.Read();

					if (iNext == 'd')
					{
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
						fTemp = new float[s.Length];

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);
						float.TryParse(s[2], out fTemp[2]);
						if (s.Length == 4)
							float.TryParse(s[3], out fTemp[3]);

						//setDiffuse(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
						currMaterial.setDiffuse(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
					}
					else if (iNext == 'a')
					{
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
						fTemp = new float[s.Length];

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);
						float.TryParse(s[2], out fTemp[2]);
						if (s.Length == 4)
							float.TryParse(s[3], out fTemp[3]);

						//setAmbient(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
						currMaterial.setAmbient(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
					}
					else if (iNext == 's')
					{
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
						fTemp = new float[s.Length];

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);
						float.TryParse(s[2], out fTemp[2]);
						if (s.Length == 4)
							float.TryParse(s[3], out fTemp[3]);

						//setSpecular(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
						currMaterial.setSpecular(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
					}
				}
				else if (c == 'm') //Texture file
				{
					sr.Read(); //'a'
					sr.Read(); //'p'
					sr.Read(); //'_'
					sr.Read(); //'K'

					int iNext = sr.Read();

					if (iNext == 'd' || iNext == 'a' || iNext == 's')
					{
						Console.WriteLine("WARNING: Material File " + filename + " Texture and Material Parameter Not Supported on Line " + lineNumber);
						sr.Read();

						string texFileName = sr.ReadLine();

						//texture = new Texture(relativePath + texFileName);
						currMaterial.setTexture(Texture.AcquireTexture(relativepath + texFileName));
					}
				}
				else if (c != '\n')
				{
					sLine = sr.ReadLine();
				}

				lineNumber++;
				ic = sr.Read();
			}

			sr.Close();
			fs.Close();

			return MaterialList;
		}

		/**
		 *	Load OBJ Material file
		 **/
		[Obsolete("Not used anymore", true)]
		public Material(string filename)
		{
			useDefault();

			pathFileName = filename;
			
			FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(fs);

			string sLine;
			char[] seperators = new char[] { ' ', '/' };
			string[] s;
			float[] fTemp;

			string absolutePath = fs.Name;
			char[] sep = new char[] { '\\' };
			string[] splitAbsolutePath = absolutePath.Split(sep);

			for (int i = 0; i < splitAbsolutePath.Length - 1; i++)
				relativePath += splitAbsolutePath[i] + "\\";


			int ic = sr.Read();
			uint lineNumber = 1;

			while (ic != -1)
			{
				char c = (char)ic;

				if(c == 'n') //New Material
				{
					sLine = sr.ReadLine();
					s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

					matName = s[1];
				}
				else if (c == 'N')
				{
					int iNext = sr.Read();

					if (iNext == 's')
					{
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
						fTemp = new float[s.Length];

						float.TryParse(s[0], out fTemp[0]);

						setShine(fTemp[0]);
					}
					else
					{
						sLine = sr.ReadLine();
					}
				}
				else if (c == 'i')
				{
					sLine = sr.ReadLine();

					Console.WriteLine("WARNING: Material File " + filename + " Illumination Parameter Not Supported on Line " + lineNumber);
				}
				else if (c == 'T')
				{
					sLine = sr.ReadLine();

					Console.WriteLine("WARNING: Material File " + filename + " Transmission Filter Parameter Not Supported on Line " + lineNumber);
				}
				else if (c == 'K')
				{
					int iNext = sr.Read();

					if (iNext == 'd')
					{
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
						fTemp = new float[s.Length];

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);
						float.TryParse(s[2], out fTemp[2]);
						if (s.Length == 4)
							float.TryParse(s[3], out fTemp[3]);

						setDiffuse(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
					}
					else if (iNext == 'a')
					{
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
						fTemp = new float[s.Length];

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);
						float.TryParse(s[2], out fTemp[2]);
						if (s.Length == 4)
							float.TryParse(s[3], out fTemp[3]);

						setAmbient(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
					}
					else if (iNext == 's')
					{
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
						fTemp = new float[s.Length];

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);
						float.TryParse(s[2], out fTemp[2]);
						if (s.Length == 4)
							float.TryParse(s[3], out fTemp[3]);

						setSpecular(fTemp[0], fTemp[1], fTemp[2], fTemp.Length == 4 ? fTemp[3] : 1.0f);
					}
				}
				else if (c == 'm') //Texture file
				{
					sr.Read(); //'a'
					sr.Read(); //'p'
					sr.Read(); //'_'
					sr.Read(); //'K'

					int iNext = sr.Read();

					if (iNext == 'd' || iNext == 'a' || iNext == 's')
					{
						Console.WriteLine("WARNING: Material File " + filename + " Texture and Material Parameter Not Supported on Line " + lineNumber);
						sr.Read();

						string texFileName = sr.ReadLine();

						texture = Texture.AcquireTexture(relativePath + texFileName);
					}
				}
				else if (c != '\n')
				{
					sLine = sr.ReadLine();
				}

				lineNumber++;
				ic = sr.Read();
			}


			sr.Close();
			fs.Close();
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
				
				case MaterialType.SILK:
					Ambient = new float[] { silk_ambient[0], silk_ambient[1], silk_ambient[2], silk_ambient[3] };
           	 		Diffuse = new float[] { silk_diffuse[0], silk_diffuse[1], silk_diffuse[2], silk_diffuse[3] };
            		Specular = new float[] { silk_specular[0], silk_specular[1], silk_specular[2], silk_specular[3] };
            		Shine = new float[] { silk_shininess[0] };
            		Emission = new float[] { silk_emission[0], silk_emission[1], silk_emission[2], silk_emission[3] };
					break;
				
				case MaterialType.CONCRETE:
					Ambient = new float[] { concrete_ambient[0], concrete_ambient[1], concrete_ambient[2], concrete_ambient[3] };
           	 		Diffuse = new float[] { concrete_diffuse[0], concrete_diffuse[1], concrete_diffuse[2], concrete_diffuse[3] };
            		Specular = new float[] { concrete_specular[0], concrete_specular[1], concrete_specular[2], concrete_specular[3] };
            		Shine = new float[] { concrete_shininess[0] };
            		Emission = new float[] { concrete_emission[0], concrete_emission[1], concrete_emission[2], concrete_emission[3] };
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

		public void setName(string name)
		{
			matName = name;
		}

		public void setTexture(Texture tex)
		{
			texture = tex;
		}

        public void apply()
        {
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, Ambient);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, Diffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, Specular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, Shine);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, Emission);

			if (texture != null)
				texture.apply();
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
