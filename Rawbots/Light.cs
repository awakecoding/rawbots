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

        //float[] matrix;

        bool debug = true;

        LightName lightName;

        float[] DefaultPos = { 0.0f, 0.0f, 0.0f, 1.0f };

        public Light(LightName name)
        {
            sphere_source.LatitudinalSlices = 8;
            sphere_source.LongitudinalSlices = 8;
            sphere_source.SetColor(1.0f, 1.0f, 1.0f);
            sphere_source.SetRenderMode(RenderMode.WIRE);
            cone_source.SetColor(1.0f, 1.0f, 1.0f);
            cone_source.SetRenderMode(RenderMode.WIRE);

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

        public void setDirection(float x, float y, float z)
        {
            Direction[0] = x; Direction[1] = y; Direction[2] = z;
        }

        public void lookAt(float eyex, float eyey, float eyez,
                           float centerx, float centery, float centerz,
                           float upx, float upy, float upz)
        {
            //Matrix4 matr = Matrix4.LookAt(eyex, eyey, eyez,
            //                              centerx, centery, centerz,
            //                              upx, upy, upz);
            //matrix = matr;

            //float newXeye = matr.Row0.X * eyex + matr.Row0.Y * eyey + matr.Row0.Z * eyez + matr.Row0.W * 1.0f;
            //float newYeye = matr.Row1.X * eyex + matr.Row1.Y * eyey + matr.Row1.Z * eyez + matr.Row1.W * 1.0f;
            //float newZeye = matr.Row2.X * eyex + matr.Row2.Y * eyey + matr.Row2.Z * eyez + matr.Row2.W * 1.0f;

            //setPosition(newXeye, newYeye, newZeye, 1.0f);

            //float newXdir = matr.Row0.Z * -1.0f + matr.Row0.W * 1.0f;
            //float newYdir = matr.Row1.Z * -1.0f + matr.Row1.W * 1.0f;
            //float newZdir = matr.Row2.Z * -1.0f + matr.Row2.W * 1.0f;

            //float mag = (float)Math.Sqrt(newXdir * newXdir + newYdir * newYdir + newZdir * newZdir);

            //newXdir /= mag;
            //newYdir /= mag;
            //newZdir /= mag;

            //setDirection(newXdir, newYdir, newZdir);

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
                    sphere_source.render();
                }
                else if (SpotCutOff >= 0.0f || SpotCutOff <= 90.0f)
                {
                    GL.Translate(Position[0], Position[1], Position[2]);
                    sphere_source.render();
                    GL.Translate(-Position[0], -Position[1], -Position[2]);

                    //GL.Color3(1.0f, 1.0f, 0.0f);
                    GL.Begin(BeginMode.Lines);
                    GL.Vertex3(Position[0], Position[1], Position[2]);
                    GL.Vertex3(Position[0] + getRayLength() * Direction[0],
                               Position[1] + getRayLength() * Direction[1],
                               Position[2] + getRayLength() * Direction[2]);
                    GL.End();

                    //GL.LoadIdentity();
                    //GL.MultMatrix(matrix);
                    
                    //GL.Color3(1.0f, 0.0f, 0.0f);
                    //GL.Begin(BeginMode.Lines);
                    //GL.Vertex3(0.0f, 0.0f, 1.0f);
                    //GL.Vertex3(0.0f, 0.0f, /*getRayLength()*/0.0f);
                    //GL.End();

                    //cone_source.render(/*3.0f*/getRayLength() * Math.Tan(SpotCutOff / 180.0f * Math.PI), getRayLength(), 20, 20);
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

            //GL.Enable(Light.lightNameCapLookUp(lightName));

			GL.PopMatrix();
        }

        public void unapply()
        {
            //GL.Disable(Light.lightNameCapLookUp(lightName));
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
