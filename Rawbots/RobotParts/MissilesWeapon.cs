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
	public class MissilesWeapon : Weapon
	{
        private float inCylinderRadius;
        private float inCylinderHeight;
        private float outCylinderRadius;
        private float outCylinderHeight;
        private float cubeSize;
        private CylinderModel inCylinder;
        private CylinderModel outCylinder;
        private CubeModel middleBox;
        private CubeModel frame;
        //CubeModel box; //For 1unit cubed box

		public MissilesWeapon()
		{    
            inCylinderRadius = 0.1f;
            inCylinderHeight = 1.0f;
            inCylinder = new CylinderModel(inCylinderRadius, inCylinderHeight);
            inCylinder.SetColor(0.4f, 0.5f, 0.6f);

            outCylinderRadius = 0.15f;
            outCylinderHeight = 0.3f;
            outCylinder = new CylinderModel(outCylinderRadius, outCylinderHeight);
            outCylinder.SetColor(0.0f, 0.7f, 0.6f);

            cubeSize = 1.0f; //Size of standard cube
            middleBox = new CubeModel();
            middleBox.SetColor(0.1f, 0.3f, 0.3f);

            frame = new CubeModel();
            frame.SetColor(0.1f, 1.0f, 0.0f);
		}
		
		public override int getCost()
		{
			return 4;
		}
		
		public override int getRange()
		{
			return 14;
		}
		
		public override int getLethality()
		{
			return 3;
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);
            inCylinder.SetRenderMode(renderMode);
            outCylinder.SetRenderMode(renderMode);
            middleBox.SetRenderMode(renderMode);
            frame.SetRenderMode(renderMode);
        }

		public override void Render()
		{
            //left missle chamber
            GL.PushMatrix();
            GL.Translate(-0.35f, 0.0f, 0.0f);
            //inner chamber
            GL.PushMatrix();
            GL.Translate(0.0f, 0.0f, -0.5f);
            inCylinder.render();
            GL.PopMatrix();

            //outer chambers
                //back chamber
                GL.PushMatrix();
                GL.Translate(0.0f, 0.0f, -0.4f);
                outCylinder.render();
                GL.PopMatrix();
                //front chamber
                GL.PushMatrix();
                GL.Translate(0.0f, 0.0f, 0.1f);
                outCylinder.render();
                GL.PopMatrix();
            GL.PopMatrix();

            //right missle chamber
            GL.PushMatrix();
            GL.Translate(0.35f, 0.0f, 0.0f);
            //inner chamber
            GL.PushMatrix();
            GL.Translate(0.0f, 0.0f, -0.5f);
            inCylinder.render();
            GL.PopMatrix();

            //outer chambers
                //back chamber
                GL.PushMatrix();
                GL.Translate(0.0f, 0.0f, -0.4f);
                outCylinder.render();
                GL.PopMatrix();
                //front chamber
                GL.PushMatrix();
                GL.Translate(0.0f, 0.0f, 0.1f);
                outCylinder.render();
                GL.PopMatrix();
            GL.PopMatrix();

            //Middle cube creation
            GL.PushMatrix();
            GL.Scale(0.5f, 0.4f, 0.5f);
            middleBox.render(cubeSize);
            GL.PopMatrix();

            //Top frame
            GL.PushMatrix();
            GL.Translate(0.0f, 0.2f, 0.0f);
            GL.Scale(0.7f, 0.1f, 0.7f);
            frame.render(cubeSize);
            GL.PopMatrix();

            //Bottom frame
            GL.PushMatrix();
            GL.Translate(0.0f, -0.2f, 0.0f);
            GL.Scale(0.7f, 0.1f, 0.7f);
            frame.render(cubeSize);
            GL.PopMatrix();

            //Team number
            GL.PushMatrix();
            GL.Translate(0.0f, 0.3f, -0.0f);
            GL.Rotate(90, 1.0f, 0.0f, 0.0f);
            GL.Scale(0.5f, 0.5f, 0.5f);
            TeamNumber.Render();
            GL.PopMatrix();

            //WIRE FRAME BOX - to make sure it fits in one unit cube.
            /*
            GL.PushMatrix();
            box = new CubeModel();

            box.setColor(1f, 1.0f, 1.0f);
            box.setRenderMode(Model.RenderMode.WIRE);
            box.render(1);
            GL.PopMatrix();
            */
		}
	}
}

