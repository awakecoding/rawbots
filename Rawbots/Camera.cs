/**
 * RawBots: an awesome robot game
 * 
 * Copyright 2012 Mark Foo Bonasoro <foo_mark@q8ismobile.com>
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 * 
 * Original camera code from Alexander Festini ported from C/C++ to C#. 
 * 
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
        private bool bViewVolume = true;

        private float[] Transform = new float[16];

        public Camera(float x, float y, float z)
        {
            Transform[0] = 1.0f;
            Transform[5] = 1.0f;
            Transform[10] = -1.0f;
            Transform[15] = 1.0f;
            Transform[12] = x; Transform[13] = y; Transform[14] = z;

            
        }

        public float[] getRight()
        { 
            return new float[] {Transform[0], Transform[1], Transform[2], Transform[3]};
        }

        public void setRight(float x, float y, float z, float w)
        {
            Transform[0] = x;
            Transform[1] = y;
            Transform[2] = z;
            Transform[3] = w;
        }

        public float[] getUp()
        {
            return new float[] { Transform[4], Transform[5], Transform[6], Transform[7]};
        }

        public void setUp(float x, float y, float z, float w)
        {
            Transform[4] = x;
            Transform[5] = y;
            Transform[6] = z;
            Transform[7] = w;
        }

        public float[] getForward()
        {
            return new float[] { Transform[8], Transform[9], Transform[10], Transform[11]};
        }

        public void setForward(float x, float y, float z, float w)
        {
            Transform[8] = x;
            Transform[9] = y;
            Transform[10] = z;
            Transform[11] = w;
        }

        public float[] getPosition()
        {
            return new float[] { Transform[12], Transform[13], Transform[14], Transform[15] };
        }

        public void setPosition(float x, float y, float z, float w)
        {
            Transform[12] = x;
            Transform[13] = y;
            Transform[14] = z;
            Transform[15] = w;
        }

        public void setView()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            float[] viewmatrix={//Remove the three - for non-inverted z-axis
						  Transform[0], Transform[4], -Transform[8], 0,
						  Transform[1], Transform[5], -Transform[9], 0,
						  Transform[2], Transform[6], -Transform[10], 0,

						  -(Transform[0]*Transform[12] +
						  Transform[1]*Transform[13] +
						  Transform[2]*Transform[14]),

						  -(Transform[4]*Transform[12] +
						  Transform[5]*Transform[13] +
						  Transform[6]*Transform[14]),

						  //add a - like above for non-inverted z-axis
						  (Transform[8]*Transform[12] +
						  Transform[9]*Transform[13] +
						  Transform[10]*Transform[14]), 1};


            GL.LoadMatrix(viewmatrix);

            //if (bViewVolume)
            //{
            //    GL.PushMatrix();

            //    float[] mat = new float[16];
            //    GL.GetFloat(GetPName.ProjectionMatrix, mat);
            //    GL.LoadMatrix(mat);
            //    GL.Color3(1.0f, 1.0f, 1.0f);
            //    Glut.glutWireCube(1.0f);

            //    GL.PopMatrix();

            //    GL.LoadMatrix(viewmatrix);
            //}
        }

	    public void moveLoc(float x, float y, float z, float distance)
        {
            float dx = x * Transform[0] + y * Transform[4] + z * Transform[8];
            float dy = x * Transform[1] + y * Transform[5] + z * Transform[9];
            float dz = x * Transform[2] + y * Transform[6] + z * Transform[10];
            Transform[12] += dx * distance;
            Transform[13] += dy * distance;
            Transform[14] += dz * distance;
        }

        public void moveGlob(float x, float y, float z, float distance)
        {
            Transform[12] += x * distance;
            Transform[13] += y * distance;
            Transform[14] += z * distance;
        }

        public void rotateLoc(float deg, float x, float y, float z)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadMatrix(Transform);
            GL.Rotate(deg, x, y, z);
            GL.GetFloat(GetPName.ModelviewMatrix, Transform);
            GL.PopMatrix();
        }

        public void rotateGlob(float deg, float x, float y, float z)
        {
            float dx = x * Transform[0] + y * Transform[1] + z * Transform[2];
            float dy = x * Transform[4] + y * Transform[5] + z * Transform[6];
            float dz = x * Transform[8] + y * Transform[9] + z * Transform[10];
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadMatrix(Transform);
            GL.Rotate(deg, dx, dy, dz);
            GL.GetFloat(GetPName.ModelviewMatrix, Transform);
            GL.PopMatrix();
        }
        
    }
}
