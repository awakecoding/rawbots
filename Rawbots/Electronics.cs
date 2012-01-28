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
		CubeModel mc;
		
		public Electronics()
		{
			/*
			 * This module increases weapon accuracy, giving a notional added
			 * range of 3 miles to each weapon type, Advance warning of attack
			 * contributes to the slightly increased resistance to damage from
			 * enemy fire when this unit is fitted.
			 */
			
			mc = new CubeModel();
			mc.setColor(0.5f, 0.3f, 0.5f);
		}
		
		public override void Render()
		{
			mc.render(1);
		}
	}
}

