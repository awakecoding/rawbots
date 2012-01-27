using System;
using System.Collections.Generic;
using System.Text;
using Tao.FreeGlut;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class TeamNumber
    {
        private static ModelCube[] mcComponent;
        private const int TOTAL = 4;

        static TeamNumber()
        {
            mcComponent = new ModelCube[TOTAL];

            for (int i = 0; i < mcComponent.Length; i++)
            {
                ModelCube mc = new ModelCube();
                mc.setColor(1.0f, 1.0f, 1.0f);
                mcComponent[i] = mc;
            }
        }

        public static void render()
        {
            GL.PushMatrix();

            GL.Translate(0.375f, 0.75f, 0.0f);

            GL.PushMatrix();

            GL.Translate(0.0f, -0.5f, 0.0f);
            GL.Scale(0.25f, 2.0f, 0.33f);
            mcComponent[0].render(1.0f);

            GL.PopMatrix();
            GL.PushMatrix();

            GL.Translate(-0.75f, -0.0f, 0.0f);
            GL.Scale(0.25f, 1.0f, 0.33f);
            mcComponent[0].render(1.0f);

            GL.PopMatrix();
            GL.PushMatrix();

            GL.Translate(-0.37f, 0.375f, 0.0f);
            GL.Scale(1.0f, 0.25f, 0.33f);
            mcComponent[0].render(1.0f);

            GL.PopMatrix();
            GL.PushMatrix();

            GL.Translate(-0.37f, -0.375f, 0.0f);
            GL.Scale(1.0f, 0.25f, 0.33f);
            mcComponent[0].render(1.0f);

            GL.PopMatrix();
            GL.PopMatrix();
        }
    }
}
