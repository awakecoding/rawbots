/**
 * RawBots: an awesome robot game
 * 
 * Copyright 2012 Mark Foo Bonasoro <foo_mark@q8ismobile.com>
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Text;
using Tao.FreeGlut;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class TeamNumber
    {
        private static CubeModel[] mcComponent;
        private const int TOTAL = 4;

        static TeamNumber()
        {
            mcComponent = new CubeModel[TOTAL];

            for (int i = 0; i < mcComponent.Length; i++)
            {
                CubeModel mc = new CubeModel();
                mc.SetColor(1.0f, 1.0f, 1.0f);
                mcComponent[i] = mc;
            }
        }

        public static void render()
        {
            GL.PushMatrix();

            GL.Translate(0.375f, 0.75f, 0.0f);

            GL.PushMatrix();

            GL.Translate(0.0f, -0.5f, 0.0f);
            GL.Scale(0.25f, 2.0f, 0.33f);
            mcComponent[0].render(1.0f);

            GL.PopMatrix();
            GL.PushMatrix();

            GL.Translate(-0.75f, -0.0f, 0.0f);
            GL.Scale(0.25f, 1.0f, 0.33f);
            mcComponent[0].render(1.0f);

            GL.PopMatrix();
            GL.PushMatrix();

            GL.Translate(-0.37f, 0.375f, 0.0f);
            GL.Scale(1.0f, 0.25f, 0.33f);
            mcComponent[0].render(1.0f);

            GL.PopMatrix();
            GL.PushMatrix();

            GL.Translate(-0.37f, -0.375f, 0.0f);
            GL.Scale(1.0f, 0.25f, 0.33f);
            mcComponent[0].render(1.0f);

            GL.PopMatrix();
            GL.PopMatrix();
        }
    }
}
