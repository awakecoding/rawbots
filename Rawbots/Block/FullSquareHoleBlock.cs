﻿/**
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
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	class FullSquareHoleBlock : Block
	{
		public FullSquareHoleBlock()
		{
			PosX = PosY = 0;
			model = new OBJModel(Game.resourcePath + "/Obstacles/full_block.obj");
		}

		public FullSquareHoleBlock(int x, int y)
		{
			PosX = x;
			PosY = y;
			model = new OBJModel(Game.resourcePath + "/Obstacles/full_block.obj");
		}
	}
}
