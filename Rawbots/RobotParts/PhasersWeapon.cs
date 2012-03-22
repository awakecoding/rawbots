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
	public class PhasersWeapon : Weapon
	{
		private double cylinderRadius;
		private double cylinderHeight;
		private CylinderModel cylinder;
		
		private double halfCylinderRadius;
		private double halfCylinderHeight;
		private HalfCylinderModel halfCylinder;
		
		private CubeModel cube;
		private double phasersWidth;
		private double phasersRadius;
		private double phasersHeight;
		private HalfCylinderModel hcPhasersL;
		private HalfCylinderModel hcPhasersR;
		
		public PhasersWeapon()
		{
			cylinderHeight = 0.1f;
			cylinderRadius = 0.5f;
			cylinder = new CylinderModel(cylinderRadius, cylinderHeight);
			cylinder.SetColor(0.7f, 0.6f, 0.75f);
			
			halfCylinderRadius = 0.5f;
			halfCylinderHeight = 0.5f;
			halfCylinder = new HalfCylinderModel(halfCylinderRadius, halfCylinderHeight);
			halfCylinder.SetColor(0.6f, 0.6f, 0.75f);
			
			phasersWidth = 0.1f;
			phasersRadius = 0.1f;
			phasersHeight = 0.1f;
			cube = new CubeModel();
			hcPhasersL = new HalfCylinderModel(phasersRadius, phasersHeight);
			hcPhasersR = new HalfCylinderModel(phasersRadius, phasersHeight);
			hcPhasersL.SetColor(0.45f, 0.75f, 0.6f);
			hcPhasersR.SetColor(0.45f, 0.75f, 0.6f);
			cube.SetColor(0.45f, 0.75f, 0.6f);
			
			material = new Material(Material.MaterialType.SHINY_STEEL);
			halfCylinder.AssignMaterial(material);
            cylinder.AssignMaterial(material);
			cube.AssignMaterial(material);
			hcPhasersL.AssignMaterial(material);
			hcPhasersR.AssignMaterial(material);

			model = new OBJModel(Game.resourcePath + "/Phasers/Phasers.obj");

		}
		
		public override int getCost()
		{
			return 4;
		}
		
		public override int getRange()
		{
			return 10;
		}
		
		public override int getLethality()
		{
			return 4;
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            cylinder.SetRenderMode(renderMode);
            halfCylinder.SetRenderMode(renderMode);
            cube.SetRenderMode(renderMode);
            hcPhasersL.SetRenderMode(renderMode);
            hcPhasersR.SetRenderMode(renderMode);
        }

		public override void Render()
		{
			//GL.PushMatrix();

			///* phasers base (cylinder) */
			
			//GL.PushMatrix();
			
			//GL.Rotate(-90, 1.0, 0.0, 0.0);
			//cylinder.render();
			
			//GL.PopMatrix();
			
			///* phasers support (half cylinder) */
			
			//GL.PushMatrix();
			
			//GL.Translate(0.0, cylinderHeight, 0.0);
			//GL.Rotate(-90, 1.0, 0.0, 0.0);
			//halfCylinder.render();
			
			//GL.PopMatrix();
			
			///* phasers */
			
			//GL.PushMatrix();
			//GL.Scale(0.2f, 0.2f, 0.6f);
			
			///* phasers sides (half cylinders) */
			
			//GL.PushMatrix();
			
			//GL.Translate(-phasersWidth, cylinderHeight + 0.2f, 0.0);
			//GL.Rotate(90, 0.0, 0.0, 1.0);
			//hcPhasersL.render();
			
			//GL.PopMatrix();
			
			//GL.PushMatrix();
			
			//GL.Translate(phasersWidth, cylinderHeight + 0.2f, 0.0);
			//GL.Rotate(-90, 0.0, 0.0, 1.0);
			//hcPhasersR.render();
			
			//GL.PopMatrix();
			
			///* phasers center (rectangular prism) */
			
			//GL.PushMatrix();
			
			//GL.Translate(0.0f, cylinderHeight + 0.2f, 0.0);
			//cube.render(phasersWidth * 2);
			
			//GL.PopMatrix();
			
			//GL.PopMatrix();
			
			///* team number */
			
			//GL.PushMatrix();
			
			//GL.Scale(0.3f, 0.3f, 0.3f);
			//GL.Translate(0.0f, 1.25f, 0.5f);
			//GL.Rotate(-90, 1.0f, 0.0f, 0.0f);
			//TeamNumber.Render();
			//GL.Translate(0.0f, -1.25f, -0.5f);
			
			//GL.PopMatrix();

			//GL.PopMatrix();

			GL.PushMatrix();
			GL.Scale(0.8f, 0.8f, 0.8f);
			model.Render();

			GL.PopMatrix();
		}
	}
}

