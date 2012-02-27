/**
 * RawBots: an awesome robot game
 * 
 * Copyright 2012 Marc-Andre Moreau <marcandre.moreau@gmail.com>
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class Tile : Drawable
	{
        public Plane plane;
        private bool teamNumber = false;

        private Light light;

		public Tile()
		{
            plane = new Plane();
            plane.SetRenderMode(RenderMode.SOLID);
			
			material = new Material(Material.MaterialType.DIFFUSE_GRAY);
			plane.AssignMaterial(material);
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            plane.SetRenderMode(renderMode);
        }

        public void showTeamNumber()
        {
            teamNumber = true;
        }

        public void hideTeamNumber()
        {
            teamNumber = false;
        }

        public void AddLight(Light l)
        { 
            light = l;
        }

        public Light GetLight()
        {
            return light;
        }

        public override void Render()
        {
            GL.PushMatrix();
            plane.render(1.0f);
            
            if (teamNumber)
            {
                GL.Scale(0.5f, 0.5f, 0.5f);
                GL.Rotate(90.0f, 1.0f, 0.0f, 0.0f);
                TeamNumber.Render();
            }

            GL.PopMatrix();
        }
	}
}
