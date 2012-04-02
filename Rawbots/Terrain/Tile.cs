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
	public abstract class Tile : Drawable
	{
		public int PosX;
		public int PosY;

        public Plane plane;
		private Light light;
        private bool teamNumber = false;

		public Tile()
		{
			PosX = PosY = 0;

			plane = new Plane();
			plane.SetRenderMode(RenderMode.SOLID);

			material = new Material(Material.MaterialType.DIFFUSE_GRAY);
			plane.AssignMaterial(material);

			model = new OBJModel(Game.resourcePath + "/Floor/Tile.obj");
		}

		public Tile(int x, int y)
		{
			PosX = x;
			PosY = y;

			plane = new Plane();
			plane.SetRenderMode(RenderMode.SOLID);

			material = new Material(Material.MaterialType.DIFFUSE_GRAY);
			plane.AssignMaterial(material);

			model = new OBJModel(Game.resourcePath + "/Floor/Tile.obj");
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);
            plane.SetRenderMode(renderMode);
        }

        public void ShowTeamNumber()
        {
            teamNumber = true;
        }

        public void HideTeamNumber()
        {
            teamNumber = false;
        }

        public void AddLight(Light light)
        { 
            this.light = light;
        }

        public Light GetLight()
        {
            return light;
        }

		public float GetWidth()
		{
			return 1.0f;
		}

        public override void Render()
        {
			if (teamNumber)
			{
				GL.Color3(0.3f, 0.3f, 0.3f);
				model.Render();
			}
			else
			{
				GL.Color3(0.3f, 0.3f, 0.3f);
				model.Render();
			}
        }
	}
}
