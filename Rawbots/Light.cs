using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Rawbots
{
    public class Light
    {
        float[] Ambient = { 0.0f, 0.0f, 0.0f, 0.0f};
        float[] Diffuse = { 1.0f, 1.0f, 1.0f, 1.0f};
        float[] Specular = { 1.0f, 1.0f, 1.0f, 1.0f};
        
        float[] Position = { 0.0f, 0.0f, 1.0f, 0.0f };
        float[] Direction = { 0.0f, 0.0f, -1.0f};
        float SpotExp = 0.0f;
        float SpotCutOff = 180.0f;

        float Attenuation = 0.0f;
        SphereModel sphere_source = new SphereModel(0.05f);
        ConeModel cone_source = new ConeModel();

        bool debug = true;

        public Light()
        {
            sphere_source.LatitudinalSlices = 8;
            sphere_source.LongitudinalSlices = 8;
            sphere_source.SetColor(1.0f, 1.0f, 1.0f);
            sphere_source.SetRenderMode(RenderMode.WIRE);
            cone_source.SetColor(1.0f, 1.0f, 1.0f);
            cone_source.SetRenderMode(RenderMode.WIRE);
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

        public void setPosition(float x, float y, float z, float w)
        {
            Position[0] = x; Position[1] = y; Position[2] = z; Position[3] = w;
        }

        public void setDirection(float x, float y, float z)
        {
            Direction[0] = x; Direction[1] = y; Direction[2] = z;
        }

		public void lookAt(float eyex, float eyey, float eyez,
						   float centerx, float centery, float centerz,
						   float upx, float upy, float upz)
		{
			Matrix4 matr = Matrix4.LookAt(eyex, eyey, eyez,
										  centerx, centery, centery,
										  upx, upy, upz);
			setPosition(eyex, eyey, eyez, 1.0f);

			float newX = matr.Row0.Z * -1.0f + matr.Row0.W * 1.0f;
			float newY = matr.Row1.Z * -1.0f + matr.Row1.W * 1.0f;
			float newZ = matr.Row2.Z * -1.0f + matr.Row2.W * 1.0f;

			float mag = (float)Math.Sqrt(newX * newX + newY * newY + newZ * newZ);

			newX /= mag;
			newY /= mag;
			newZ /= mag;

			setDirection(newX, newY, newZ);
		}

        public void setAttenuation(float exp)
        {
            Attenuation = exp;
        }

        public void setCutOff(float angle)
        {
            SpotCutOff = angle;
        }

        public void apply()
        {
			bool reEnable = false;
			
			GL.Light(LightName.Light0, LightParameter.Ambient, Ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, Diffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, Specular);
            GL.Light(LightName.Light0, LightParameter.QuadraticAttenuation, Attenuation);

            if (debug)
            {
                if (GL.IsEnabled(EnableCap.Lighting))
                {
                    GL.Disable(EnableCap.Lighting);
                    reEnable = true;
                }

                GL.PushMatrix();
                if (SpotCutOff == 180.0f)
                {
                    GL.Translate(Position[0], Position[1], Position[2]);
                    sphere_source.render();
                }
                else if (SpotCutOff >= 0.0f || SpotCutOff <= 90.0f)
                {
                    cone_source.render(3.0f * Math.Tan(SpotCutOff / 180.0f * Math.PI), 3.0f, 20, 20);
                }

                GL.PopMatrix();

                if (reEnable)
                {
                    GL.Enable(EnableCap.Lighting);
                }
            }

            GL.Light(LightName.Light0, LightParameter.Position, Position);
            GL.Light(LightName.Light0, LightParameter.SpotCutoff, SpotCutOff);
            GL.Light(LightName.Light0, LightParameter.SpotDirection, Direction);
            GL.Light(LightName.Light0, LightParameter.SpotExponent, SpotExp);

            GL.Enable(EnableCap.Light0);
        }

        public void unapply()
        {
            GL.Disable(EnableCap.Light0);
        }
    }
}
