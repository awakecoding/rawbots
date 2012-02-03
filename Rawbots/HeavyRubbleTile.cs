using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class HeavyRubbleTile : Tile
    {
        public CubeModel cube;
        public HemisphereModel hemiM;

        public HeavyRubbleTile()
        {
            cube = new CubeModel();
            cube.setRenderMode(RenderMode.SOLID_WIRE);
            cube.setColor(0.64f, 0.64f, 0.67f);

            hemiM = new HemisphereModel(1.0f);
            hemiM.LatitudinalSlices = 10;
            hemiM.LongitudinalSlices = 10;
            hemiM.setRenderMode(RenderMode.SOLID_WIRE);
            cube.setColor(0.64f, 0.64f, 0.67f);
        }

        public override void Render()
        {
            base.Render();

            GL.Translate(0.0f, -0.05f, 0.0f);

            GL.PushMatrix();

            //(0, 0, 0)

            smallPileOne(); //Draw First One

            GL.Translate(-0.25f, 0.0f, 0.25f); //(-.25, 0, .25)

            smallPileOne(); //Draw 2nd One

            GL.Translate(0.0f, 0.0f, -0.5f); //(-.25, 0, -.25)

            smallPileOne(); //Draw 3rd One

            GL.Translate(0.5f, 0.0f, 0.0f); //(.25, 0, -.25) 

            smallPileOne(); //Draw 4th One

            GL.Translate(0.0f, 0.0f, 0.5f); //(.25, 0, .25)

            smallPileOne(); //Draw 5th One

            GL.PopMatrix();
            GL.PushMatrix();

            GL.Translate(-0.3f, 0.0f, 0.0f);

            smallPileRoundOne();

            GL.Translate(0.6f, 0.0f, 0.0f);

            smallPileRoundOne();

            GL.Translate(-0.3f, 0.0f, -0.3f);

            smallPileRoundOne();

            GL.Translate(0.0f, 0.0f, 0.6f);

            smallPileRoundOne();

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

        public void smallPileRoundOne()
        {
            GL.PushMatrix();

            GL.Scale(0.15f, 0.15f, 0.15f);
            hemiM.render();

            GL.PopMatrix();
        }

    }
}
