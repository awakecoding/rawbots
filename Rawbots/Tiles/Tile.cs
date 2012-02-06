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

		public Tile()
		{
            plane = new Plane();
            plane.SetRenderMode(RenderMode.SOLID_WIRE);
		}

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);

            plane.SetRenderMode(renderMode);
        }

        public override void Render()
        {
            GL.PushMatrix();
            plane.render(1.0f);
            GL.PopMatrix();
        }
	}
}
