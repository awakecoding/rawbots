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
    public class CannonWeapon : Weapon
    {
        public CannonWeapon()
        {			
			material = new Material(Material.MaterialType.SHINY_STEEL);
			model = new OBJModel(Game.resourcePath + "/Cannon/Phasers.obj");
        }

        public override int getCost()
        {
            return 8;
        }

        public override int getRange()
        {
            return 10;
        }

        public override int getLethality()
        {
            return 2;
        }

        public override void SetRenderMode(RenderMode renderMode)
        {
            base.SetRenderMode(renderMode);
        }

        public override void Render()
        {
			GL.PushMatrix();

			model.Render();

			GL.PopMatrix();
        }
    }
}

