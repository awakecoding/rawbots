using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Rawbots
{
    public class Light
    {
        float[] Ambient = { 0.0f, 0.0f, 0.0f, 0.0f };
        float[] Diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
        float[] Specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        
        float[] Position = { 0.0f, 0.0f, 1.0f, 0.0f };
        float[] Direction = { 0.0f, 0.0f, -1.0f};
        float SpotExp = 0.0f;
        float SpotCutOff = 180.0f;

        float Attenuation = 0.0f;
        SphereModel sphereSource = new SphereModel(0.05f);
        ConeModel coneSource = new ConeModel();

        bool debug = true;

        LightName lightName;

        float[] DefaultPos = { 0.0f, 0.0f, 0.0f, 1.0f };

        public Light(LightName name)
        {
            sphereSource.LatitudinalSlices = 8;
            sphereSource.LongitudinalSlices = 8;
            sphereSource.SetColor(1.0f, 1.0f, 1.0f);
            sphereSource.SetRenderMode(RenderMode.WIRE);
            coneSource.SetColor(1.0f, 1.0f, 1.0f);
            coneSource.SetRenderMode(RenderMode.WIRE);

            lightName = name;
            GL.Enable(lightNameCapLookUp(name));
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

		public float[] getPosition()
		{
			return Position;
		}

        public void setDirection(float x, float y, float z)
        {
            Direction[0] = x; Direction[1] = y; Direction[2] = z;
        }

        public void lookAt(float eyex, float eyey, float eyez,
                           float centerx, float centery, float centerz,
                           float upx, float upy, float upz)
        {
            setPosition(eyex, eyey, eyez, 1.0f);

            float dirX = centerx - eyex; 
            float dirY = centery - eyey; 
            float dirZ = centerz - eyez;

            float mag = (float)Math.Sqrt(dirX * dirX + dirY * dirY + dirZ * dirZ);

            setDirection(dirX / mag, dirY / mag, dirZ / mag);
        }

        public void setAttenuation(float exp)
        {
            Attenuation = exp;
        }

        public void setCutOff(float angle)
        {
            SpotCutOff = angle;
        }

        public double getRayLength()
        {
            return Math.Sqrt(Position[0] * Position[0] + Position[1] * Position[1] + Position[2] * Position[2]);
        }

        public void apply()
        {
			bool reEnable = false;

			GL.PushMatrix();

			GL.Light(lightName, LightParameter.Ambient, Ambient);
            GL.Light(lightName, LightParameter.Diffuse, Diffuse);
            GL.Light(lightName, LightParameter.Specular, Specular);
            GL.Light(lightName, LightParameter.QuadraticAttenuation, Attenuation);

            if (debug && GL.IsEnabled(lightNameCapLookUp(lightName)))
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
                    sphereSource.render();
                }
                else if (SpotCutOff >= 0.0f || SpotCutOff <= 90.0f)
                {
                    GL.Translate(Position[0], Position[1], Position[2]);
                    sphereSource.render();
                    GL.Translate(-Position[0], -Position[1], -Position[2]);

                    //GL.Color3(1.0f, 1.0f, 0.0f);
                    GL.Begin(BeginMode.Lines);
                    GL.Vertex3(Position[0], Position[1], Position[2]);
                    GL.Vertex3(Position[0] + getRayLength() * Direction[0],
                               Position[1] + getRayLength() * Direction[1],
                               Position[2] + getRayLength() * Direction[2]);
                    GL.End();
                }

                GL.PopMatrix();

                if (reEnable)
                {
                    GL.Enable(EnableCap.Lighting);
                }
            }

            GL.Light(lightName, LightParameter.Position, Position);
            GL.Light(lightName, LightParameter.SpotCutoff, SpotCutOff);
            GL.Light(lightName, LightParameter.SpotDirection, Direction);
            GL.Light(lightName, LightParameter.SpotExponent, SpotExp);

			GL.PopMatrix();
        }

        public void unapply()
        {

        }

		public float[] getShadowMatrix(float[] normal)
		{
			return shadowMatrix(normal, Position);
		}

		public static float[] shadowMatrix(float[] plane, float[] light_pos)
		{
			float[] matOut = new float[16];

			float dot = plane[0] * light_pos[0] + plane[1] * light_pos[1] + plane[2] * light_pos[2] + plane[3] * light_pos[3];

			matOut[0] = dot - light_pos[0] * plane[0];
			matOut[4] = -light_pos[0] * plane[1];
			matOut[8] = -light_pos[0] * plane[2];
			matOut[12] = -light_pos[0] * plane[3];

			matOut[1] = -light_pos[1] * plane[0];
			matOut[5] = dot - light_pos[1] * plane[1];
			matOut[9] = -light_pos[1] * plane[2];
			matOut[13] = -light_pos[1] * plane[3];

			matOut[2] = -light_pos[2] * plane[0];
			matOut[6] = -light_pos[2] * plane[1];
			matOut[10] = dot - light_pos[2] * plane[2];
			matOut[14] = -light_pos[2] * plane[3];

			matOut[3] = -light_pos[3] * plane[0];
			matOut[7] = -light_pos[3] * plane[1];
			matOut[11] = -light_pos[3] * plane[2];
			matOut[15] = dot - light_pos[3] * plane[3];

			return matOut;
		}

		public static float[] calcNormal(float[] p0, float[] p1, float[] p2)
		{
			float[] v0 = new float[3];
			float[] v1 = new float[3];
			float[] pOut = new float[4];
			float len;
			
			v0[0] = p1[0] - p0[0];
			v0[1] = p1[1] - p0[1];
			v0[2] = p1[2] - p0[2];
			len = (float)Math.Sqrt(v0[0]*v0[0] + v0[1]*v0[1] + v0[2]*v0[2]);
			v0[0] /= len;
			v0[1] /= len;
			v0[2] /= len;

			v1[0] = p2[0] - p0[0];
			v1[1] = p2[1] - p0[1];
			v1[2] = p2[2] - p0[2];
			len = (float)Math.Sqrt(v1[0]*v1[0] + v1[1]*v1[1] + v1[2]*v1[2]);
			v1[0] /= len;
			v1[1] /= len;
			v1[2] /= len;

			pOut[0] = v0[1]*v1[2] - v0[2]*v1[1];
			pOut[1] = -(v0[0]*v1[2] - v0[2]*v1[0]);
			pOut[2] = v0[0]*v1[1] - v0[1]*v1[0];
			pOut[3] = -(pOut[0]*p0[0] + pOut[1]*p0[1] + pOut[2]*p0[2]);
		
			return pOut;
		}

       public static EnableCap lightNameCapLookUp(LightName name)
        {
            switch (name)
            { 
                case LightName.Light0:
                    return EnableCap.Light0;
                case LightName.Light1:
                    return EnableCap.Light1;
                case LightName.Light2:
                    return EnableCap.Light2;
                case LightName.Light3:
                    return EnableCap.Light3;
                case LightName.Light4:
                    return EnableCap.Light4;
                case LightName.Light5:
                    return EnableCap.Light5;
                case LightName.Light6:
                    return EnableCap.Light6;
                case LightName.Light7:
                    return EnableCap.Light7;
            }

            return EnableCap.Light0;
        }
		
    }
}
