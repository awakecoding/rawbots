using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class MediumRubbleTile : Tile
    {
        public CubeModel cube;

        public MediumRubbleTile()
        {
            cube = new CubeModel();
            cube.SetColor(0.64f, 0.64f, 0.67f);
			
			material = new Material(Material.MaterialType.ROCK_DIFFUSE);
			cube.AssignMaterial(material);
        }

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            cube.SetRenderMode(renderMode);
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
