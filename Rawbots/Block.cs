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

namespace Rawbots
{
	public class Block : Model
	{
		bool half;
		CubeModel cube;
		
		public Block(bool half) : base()
		{
			this.half = half;
			cube = new CubeModel();
			cube.setColor(0.4f, 0.7f, 0.1f);
		}
		
        public void render()
        {
			cube.render(1.0f);
        }
	}
}

