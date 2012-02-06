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
    public class CannonWeapon : Weapon
    {
        private double cannonRadius; //chase
        private double cannonHeight;

        private double cannonRingRadius1; //ringe1
        private double cannonRingHeight1;

        private double cannonRingRadius2;//ringe2
        private double cannonRingHeight2;

        private CylinderModel cylinder;

        private CubeModel cube;

        private float topWidth;//top of cannon
        private float topLength;
        private float topHeight;

        private float bottomWidth; // bottom of cannon
        private float bottomLength;
        private float bottomHeight;

        private float backDepth;

        static float K = 0.5f;

        static float[] shear = { 1, 0, 0, 0,
								 K, 1, 0, 0,
								 0, 0, 1, 0,
								 0, 0, 0, 1 };


        //parallelogram


        public CannonWeapon()
        {
            cannonRadius = 0.1f;
            cannonHeight = 1.0f;

            cannonRingRadius1 = 0.15f;
            cannonRingHeight1 = 0.2f;

            cannonRingRadius2 = 0.2f;
            cannonRingHeight2 = 0.1f;

            topWidth = 2.0f;//top of cannon
            topLength = 1.0f;
            topHeight = 0.1f;

            bottomWidth = 2.0f;
            bottomLength = 1.5f;
            bottomHeight = 0.1f;

            backDepth = 0.8f;

            cube = new CubeModel();
            cylinder = new CylinderModel(cannonRadius, cannonHeight);
        }

        public override int getCost()
        {
            return 8;
        }

        public override int getRange()
        {
            return 10;
        }

        public override int getLethality()
        {
            return 2;
        }

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            cylinder.SetRenderMode(renderMode);
            cube.SetRenderMode(renderMode);
        }

        public override void Render()
        {
            //Unit Size
            GL.Scale(0.5f, 0.5f, 0.5f);

            cube.SetColor(0.8f, 0.8f, 0.8f);
            //Bottom
            GL.PushMatrix();
            GL.Scale(bottomWidth, bottomHeight, bottomLength);
            cube.render(1.0f);
            GL.PopMatrix();

            //cube.SetColor(0.6f, 0.6f, 0.6f);
            //Top
            GL.PushMatrix();
            GL.Translate(0.0f, 1.0f, -(bottomLength - topLength) / 2);
            GL.Scale(topWidth, topHeight, topLength);
            cube.render(1.0f);
            GL.PopMatrix();

            //chases and ringes
            //::chases:://
            cylinder.SetColor(0.7f, 0.7f, 0.7f);
            cylinder.setHeight(cannonHeight);
            cylinder.setRadius(cannonRadius);
            GL.PushMatrix();
            GL.Translate(0.4f, 0.5f, 0.0f);
            cylinder.render();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-0.4f, 0.5f, 0.0f);
            cylinder.render();
            GL.PopMatrix();

            //::ringes1:://
            cylinder.SetColor(0.9f, 0.9f, 0.9f);
            cylinder.setHeight(cannonRingHeight1);
            cylinder.setRadius(cannonRingRadius1);
            GL.PushMatrix();
            GL.Translate(0.4f, 0.5f, 0.7f);
            cylinder.render();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-0.4f, 0.5f, 0.7f);
            cylinder.render();
            GL.PopMatrix();

            //::ringes2:://
            cylinder.setHeight(cannonRingHeight2);
            cylinder.setRadius(cannonRingRadius2);
            GL.PushMatrix();
            GL.Translate(0.4f, 0.5f, 0.05f);
            cylinder.render();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-0.4f, 0.5f, 0.05f);
            cylinder.render();
            GL.PopMatrix();

            cube.SetColor(0.8f, 0.8f, 0.8f);
            GL.PushMatrix();
            GL.Translate(-0.9f, 0.5f, 0.05f);
            GL.Rotate(90.0, 0.0f, 1.0f, 0.0f);
            GL.MultMatrix(shear);
            GL.Scale(0.8f, 0.9f, 0.2f);
            cube.render(1.0f);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-0.9f, 0.5f, 0.05f);
            GL.Rotate(90.0, 0.0f, 1.0f, 0.0f);
            GL.MultMatrix(shear);
            GL.Scale(0.8f, 0.9f, 0.2f);
            cube.render(1.0f);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.9f, 0.5f, -0.25f);
            GL.Scale(0.2f, 1.0f, 1.0f);
            cube.render(1.0f);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-0.9f, 0.5f, -0.25f);
            GL.Scale(0.2f, 1.0f, 1.0f);
            cube.render(1.0f);
            GL.PopMatrix();

            cube.SetColor(0.2f, 0.2f, 0.2f);
            //back
            GL.PushMatrix();
            GL.Translate(0.0f, 0.5f, -(bottomLength - backDepth) / 2);
            GL.Scale(topWidth, 1.0f, backDepth);
            cube.render(1.0f);
            GL.PopMatrix();

            cube.SetColor(0.2f, 0.2f, 0.2f);
            //Team Number
            GL.PushMatrix();
            GL.Translate(0.0f, 1.1f, -0.3f);
            GL.Rotate(-90.0f, 1.0f, 0.0f, 0.0f);
            GL.Scale(0.5f, 0.5f, 0.5f);
            TeamNumber.Render();
            GL.PopMatrix();
        }
    }
}

