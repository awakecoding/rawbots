
/**
 * RawBots: an awesome robot game
 * 
 * Copyright 2012 Marc-Andre Moreau <marcandre.moreau@gmail.com>
 * Copyright 2012 Mark Foo Bonasoro <foo_mark@q8ismobile.com>
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public class Base : Model
    {
        //private CubeModel cube;

        private double cylinderRadius; //antenna
        private double cylinderHeight;
        private CylinderModel cylinder;

        public int PosX { get; set; }
        public int PosY { get; set; }

        public Base()
        {
            /*
             * By far the best system, it simply flies over the
             * ground whatever its difficulties. This is the
             * only chassis that can span ravines!
             */

            //cube = new CubeModel();

            cylinderHeight = 0.5f;
            cylinderRadius = 0.03f;
            cylinder = new CylinderModel(cylinderRadius, cylinderHeight);
            cylinder.SetColor(0.2f, 0.2f, 0.2f);

            PosX = 0; PosY = 0;
			
			material = new Material(Material.MaterialType.SILK);
			cylinder.AssignMaterial(material);
			
			material = new Material(Material.MaterialType.CONCRETE);
			//cube.AssignMaterial(material);

			model = new OBJModel(Game.resourcePath + "/Base/dark_khaki.obj");
        }

        public Base(int x, int y)
        {
			model = new OBJModel(Game.resourcePath + "/Base/dark_khaki.obj");
			//cube = new CubeModel();

            cylinderHeight = 0.5f;
            cylinderRadius = 0.03f;
            cylinder = new CylinderModel(cylinderRadius, cylinderHeight);
            cylinder.SetColor(0.2f, 0.2f, 0.2f);

            PosX = x; PosY = y;
        }

        
        private void draw1x1Box(float Xtranslate, float Ytranslate, float Ztranslate)
        {
            GL.PushMatrix();
            GL.Translate(Xtranslate, Ytranslate, Ztranslate);

            //details:windows
            //cube.SetColor(0.2f, 0.2f, 0.2f);
            GL.PushMatrix();
            GL.Translate(0.0f, 0.35f, 0.0f);
            GL.Scale(0.9f, 0.15f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 0.1f, 0.0f);
            GL.Scale(0.9f, 0.15f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 0.35f, 0.0f);
            GL.Rotate(90.0f, 0.0f, 1.0f, 0.0f);
            GL.Scale(0.9f, 0.15f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 0.1f, 0.0f);
            GL.Rotate(90.0f, 0.0f, 1.0f, 0.0f);
            GL.Scale(0.9f, 0.15f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            //details:door
            GL.PushMatrix();
            GL.Translate(0.0f, -0.4f, 0.2f);
            GL.Scale(0.3f, 0.2f, 0.6f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            //cube
            //cube.SetColor(0.6f, 0.6f, 0.6f);
            GL.PushMatrix();
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PopMatrix();
        }

        private void drawHalfBoxTopDetails(float Xtranslate, float Ytranslate, float Ztranslate)
        {
            GL.PushMatrix();
            GL.Translate(Xtranslate, Ytranslate, Ztranslate);

            //details
            //cube.SetColor(0.2f, 0.2f, 0.2f);
            GL.PushMatrix();
            GL.Translate(0.0f, 0.05f, 0.0f);
            GL.Scale(0.9f, 0.15f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 0.05f, 0.0f);
            GL.Rotate(90.0f, 0.0f, 1.0f, 0.0f);
            GL.Scale(0.9f, 0.15f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 0.0f, -0.15f);
            GL.Scale(0.6f, 0.5f, 0.15f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 0.0f, 0.15f);
            GL.Scale(0.6f, 0.5f, 0.15f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();


            //base quad
            //cube.SetColor(0.7f, 0.7f, 0.7f);
            GL.PushMatrix();
            GL.Scale(1.0f, 0.5f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PopMatrix();
        }

        private void drawHalfBoxNoDetails(float Xtranslate, float Ytranslate, float Ztranslate)
        {
            GL.PushMatrix();
            GL.Translate(Xtranslate, Ytranslate, Ztranslate);

            //details
            //cube.SetColor(0.3f, 0.3f, 0.3f);
            GL.PushMatrix();
            GL.Translate(0.0f, 0.05f, 0.0f);
            GL.Scale(0.9f, 0.15f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.0f, 0.05f, 0.0f);
            GL.Rotate(90.0f, 0.0f, 1.0f, 0.0f);
            GL.Scale(0.9f, 0.15f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();


            //base quad
            //cube.SetColor(0.8f, 0.8f, 0.8f);
            GL.PushMatrix();
            GL.Scale(1.0f, 0.5f, 1.0f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PopMatrix();
        }

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            //cube.SetRenderMode(renderMode);
            cylinder.SetRenderMode(renderMode);
        }

        private void drawFlag(float Xtranslate, float Ytranslate, float Ztranslate)
        {
            GL.PushMatrix();
            GL.Translate(Xtranslate, Ytranslate, Ztranslate);

            //base
            //cube.SetColor(0.1f, 0.1f, 0.1f);
            GL.PushMatrix();
            GL.Scale(0.15f, 0.02f, 0.15f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            //pole
            GL.PushMatrix();
            //GL.Translate(0.0, 0.0, 0.0);
            GL.Rotate(-90.0, 1.0, 0.0, 0.0);
            cylinder.render();
            GL.PopMatrix();

            //flag
            //cube.SetColor(0.70f, 0.09f, 0.09f); //scarlet
            GL.PushMatrix();
            GL.Translate(0.18f, 0.35f, 0.0f);
            GL.Scale(0.3f, 0.2f, 0.05f);
			model.Render();
			//cube.render(1.0);
            GL.PopMatrix();

            GL.PopMatrix();
        }

        public void Render()
        {
            GL.PushMatrix();

            GL.Translate(PosX, 0.0f, -PosY);

            //1x1 boxes
            //::base:://

            draw1x1Box(0.0f, 0.5f, 0.0f);
            //cube.SetColor(0.9f, 0.9f, 0.9f);
            //Team Number
            GL.PushMatrix();
            GL.Translate(0.0, 1.0, 0.0);
            GL.Rotate(-90.0, 1.0, 0.0, 0.0);
            GL.Scale(0.5, 0.5, 0.5);
            TeamNumber.Render();
            GL.PopMatrix();

            //::the rest:://
            draw1x1Box(-2.0f, 0.5f, 0.0f);
            draw1x1Box(-2.0f, 0.5f, -1.0f);
            draw1x1Box(2.0f, 0.5f, 0.0f);
            draw1x1Box(2.0f, 0.5f, -1.0f);
            draw1x1Box(0.0f, 0.5f, -1.0f);
            draw1x1Box(-1.0f, 0.5f, -1.5f);
            draw1x1Box(1.0f, 0.5f, -1.5f);

            drawHalfBoxTopDetails(1.0f, 0.25f, -0.5f);
            drawHalfBoxTopDetails(1.0f, 0.25f, 0.5f);
            drawHalfBoxTopDetails(-1.0f, 0.25f, -0.5f);
            drawHalfBoxTopDetails(-1.0f, 0.25f, 0.5f);

            drawHalfBoxNoDetails(0.0f, 0.25f, 1.0f);
            drawHalfBoxNoDetails(-1.0f, 0.25f, 1.5f);
            drawHalfBoxNoDetails(1.0f, 0.25f, 1.5f);

            drawFlag(-2.0f, 1.01f, 0.0f);

            GL.PopMatrix();
        }
    }
}