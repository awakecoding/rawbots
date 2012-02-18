using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class LightRubbleTile : Tile
    {
        public CubeModel cube;
        public HemisphereModel hemiM;

        public LightRubbleTile()
        {
            cube = new CubeModel();
            cube.SetRenderMode(RenderMode.SOLID_WIRE);
            cube.SetColor(0.64f, 0.64f, 0.67f);

            hemiM = new HemisphereModel(1.0f);
            hemiM.LatitudinalSlices = 10;
            hemiM.LongitudinalSlices = 10;
            hemiM.SetRenderMode(RenderMode.SOLID_WIRE);
            cube.SetColor(0.64f, 0.64f, 0.67f);
        }

        public override void Render()
        {
            base.Render();

            GL.Translate(0.0f, -0.05f, 0.0f);

            GL.PushMatrix();

            //(0, 0, 0)

            smallPileOne(); //Draw First One

            GL.PopMatrix();
        }

        public void smallPileOne()
        {
            GL.PushMatrix();

            GL.Translate(0.0f, -0.05f, 0.0f);
            GL.Rotate(45.0f, 1.0f, 1.0f, 1.0f);
            GL.Scale(0.25f, 0.25f, 0.25f);

            cube.render(1.0f);

            GL.PopMatrix();
        }
    }
}
