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
using OpenTK.Input;
using Tao.FreeGlut;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Rawbots
{
    class Camera
    {
        public static float rayCameraSpeed = 1.0f;
        public static float rotCameraSpeed = 1.0f;

        public static float xPos = 0.0f;
        public static float yPos = 0.0f;
        public static float zPos = 0.0f;

        public static float Roll = 0.0f;
        public static float Yaw = 0.0f;
        public static float Pitch = 0.0f;

        static Camera()
        {
            resetCamera();
        }

        public static void resetCamera()
        {
            xPos = 0.0f;
            yPos = 0.0f;
            zPos = 0.0f;

            Roll = 0.0f;
            Yaw = 0.0f;
            Pitch = 0.0f;
        }

        public static void OnCameraFrame(GameWindow gw)
        {
            if (gw.Keyboard[Key.Up])
                zPos += rayCameraSpeed;
            else if (gw.Keyboard[Key.Down])
                zPos -= rayCameraSpeed;
            else if (gw.Keyboard[Key.Left])
                xPos -= rayCameraSpeed;
            else if (gw.Keyboard[Key.Right])
                xPos += rayCameraSpeed;
            else if (gw.Keyboard[Key.W])
                Pitch -= rotCameraSpeed;
            else if (gw.Keyboard[Key.S])
                Pitch += rotCameraSpeed;
            else if (gw.Keyboard[Key.A])
                Yaw -= rotCameraSpeed;
            else if (gw.Keyboard[Key.D])
                Yaw += rotCameraSpeed;
            else if (gw.Keyboard[Key.R])
                resetCamera();
        }

        public static void OnCameraUpdate()
        {
            GL.Rotate(Yaw, 0.0f, 1.0f, 0.0f);
            GL.Rotate(Pitch, 1.0f, 0.0f, 0.0f);

            GL.Translate(xPos, 0.0f, zPos);
        }
    }
}
