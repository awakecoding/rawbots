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
	public class Electronics : RobotPart
	{
		CubeModel cube;
		HemisphereModel hemisphere;
		
		public Electronics()
		{
			/*
			 * This module increases weapon accuracy, giving a notional added
			 * range of 3 miles to each weapon type, Advance warning of attack
			 * contributes to the slightly increased resistance to damage from
			 * enemy fire when this unit is fitted.
			 */
			
			cube = new CubeModel();
			cube.setColor(0.5f, 0.3f, 0.5f);
			
			hemisphere = new HemisphereModel(1.0f);
			hemisphere.setColor(0.5f, 0.3f, 0.5f);
		}
		
        public override void setRenderMode(RenderMode renderMode)
        {
			cube.setRenderMode(renderMode);
			hemisphere.setRenderMode(renderMode);
        }
		
		public override void Render()
		{
			cube.render(1);
			hemisphere.render();
		}
	}
}

