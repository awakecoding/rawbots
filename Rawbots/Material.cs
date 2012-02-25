using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public class Material
    {
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
            Specular = new float[] { DefaultSpecular[0], DefaultSpecular[1], DefaultSpecular[2], DefaultSpecular[3] };
            Shine = new float[] { DefaultSpecular[0] };
            Emission = new float[] { DefaultEmission[0], DefaultEmission[1], DefaultEmission[2], DefaultEmission[3] };
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

        public static void unapply()
        {
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, DefaultAmbient);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, DefaultDiffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, DefaultSpecular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, DefaultShine);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, DefaultEmission);
        }
    }
}
