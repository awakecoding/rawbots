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

namespace Rawbots
{
	public class AntiGravChassisFactory : Factory
	{
		public AntiGravChassisFactory() : base()
		{
			robotPart = new AntiGravChassis();
		}
		
		public AntiGravChassisFactory(int x, int y) : base(x, y)
		{
			robotPart = new AntiGravChassis();
		}
	}
}
