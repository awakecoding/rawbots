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
using System.Runtime.InteropServices;

using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    class ReferencePlane
    {
        public static int width;
        public static int height;

        public static int axis = 3;
        public const int XYZ = 0;
        public const int XZ = 1;
        public const int XY = 2;
        public const int NONE = 3;

        public static void setVisibleAxis(int axis)
        {
            ReferencePlane.axis = axis;
        }

        public static void setDimensions(int width, int height)
        {
            ReferencePlane.width = width;
            ReferencePlane.height = height;
        }

        public static void render()
        {
			int i, j;
			float di, dj;

            if (axis == XYZ || axis == XZ)
            {
                GL.PushMatrix();

                GL.Color3(0.71f, 0.71f, 0.71f);

                GL.Translate(-width / 2 * 1.0f, 0.0f, height / 2 * 1.0f);

                /* Still Innefficient! */

                GL.Begin(BeginMode.LineLoop);

                for (i = 0; i < width; i++)
                {
                    for (j = 0; j < height; j++)
                    {
                        di = i * 1.0f;
                        dj = j * -1.0f;

                        GL.Vertex3(0.0f + di, 0.0f, 0.0f + dj);
                        GL.Vertex3(1.0f + di, 0.0f, 0.0f + dj);
                        GL.Vertex3(1.0f + di, 0.0f, 0.0f + dj);
                        GL.Vertex3(1.0f + di, 0.0f, -1.0f + dj);
                    }
                }

                GL.End();

                GL.PopMatrix();
            }

            if (axis == XYZ || axis == XY)
            {
                GL.PushMatrix();

                GL.Translate(0.0f, -width / 2 * 1.0f, height / 2 * 1.0f);
                GL.Rotate(90.0f, 0.0f, 0.0f, 1.0f);

                GL.Begin(BeginMode.LineLoop);

                for (i = 0; i < width; i++)
                {
                    for (j = 0; j < height; j++)
                    {
                        di = i * 1.0f;
                        dj = j * -1.0f;

                        GL.Vertex3(0.0f + di, 0.0f, 0.0f + dj);
                        GL.Vertex3(1.0f + di, 0.0f, 0.0f + dj);
                        GL.Vertex3(1.0f + di, 0.0f, -1.0f + dj);
                        GL.Vertex3(0.0f + di, 0.0f, -1.0f + dj);
                    }
                }

                GL.End();

                GL.PopMatrix();
            }
        }

        ~ReferencePlane()
        {

        }
    }
}
