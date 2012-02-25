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
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
    public class CubeModel : Model
    {
        static uint vbo;
        static double[][] verts_side = new double[6][];
        static double[] side_size = new double[6];

        public static int sumTime = 0;

        void draw1(BeginMode beginMode, double size, float nx, float ny, float nz)
        {
            side_size[0] = size;
            if (verts_side[0] == null   //if the array is not initialized
                || side_size[0] != size)//if the new size is not identical to the old size
            {
                //Create a new array of vertices
                verts_side[0] = new double[]
				{
					+size, -size, +size,
					+size, -size, -size,
					+size, +size, -size,
					+size, +size, +size
				};
            }

            //Flush out the old data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[0].Length * sizeof(double)), IntPtr.Zero, BufferUsageHint.DynamicDraw);

            //Refilled with the new refreshed data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[0].Length * sizeof(double)), verts_side[0], BufferUsageHint.DynamicDraw);

            GL.Normal3(nx, ny, nz);
            //Draw them out
            GL.DrawArrays(beginMode, 0, 4);

        }

        void draw2(BeginMode beginMode, double size, float nx, float ny, float nz)
        {
            side_size[1] = size;
            if (verts_side[1] == null   //if the array is not initialized
                || side_size[1] != size)//if the new size is not identical to the old size
            {
                //Create a new array of vertices
                verts_side[1] = new double[] {
                +size, +size, +size,
                +size, +size, -size,
                -size, +size, -size,
                -size, +size, +size};
            }

            //Flush out the old data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[1].Length * sizeof(double)), IntPtr.Zero, BufferUsageHint.DynamicDraw);

            //Refilled with the new refreshed data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[1].Length * sizeof(double)), verts_side[1], BufferUsageHint.DynamicDraw);

            GL.Normal3(nx, ny, nz);
            //Draw them out
            GL.DrawArrays(beginMode, 0, 4);
        }

        void draw3(BeginMode beginMode, double size, float nx, float ny, float nz)
        {
            side_size[2] = size;
            if (verts_side[2] == null   //if the array is not initialized
                || side_size[2] != size)//if the new size is not identical to the old size
            {
                //Create a new array of vertices
                verts_side[2] = new double[] {
                +size, +size, +size,
                -size, +size, +size,
                -size, -size, +size,
                +size, -size, +size};
            }

            //Flush out the old data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[2].Length * sizeof(double)), IntPtr.Zero, BufferUsageHint.DynamicDraw);

            //Refilled with the new refreshed data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[2].Length * sizeof(double)), verts_side[2], BufferUsageHint.DynamicDraw);

            GL.Normal3(nx, ny, nz);
            //Draw them out
            GL.DrawArrays(beginMode, 0, 4);
        }

        void draw4(BeginMode beginMode, double size, float nx, float ny, float nz)
        {
            side_size[3] = size;
            if (verts_side[3] == null   //if the array is not initialized
                || side_size[3] != size)//if the new size is not identical to the old size
            {
                //Create a new array of vertices
                verts_side[3] = new double[] {
                -size, -size, +size,
                -size, +size, +size,
                -size, +size, -size,
                -size, -size, -size};
            }

            //Flush out the old data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[3].Length * sizeof(double)), IntPtr.Zero, BufferUsageHint.DynamicDraw);

            //Refilled with the new refreshed data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[3].Length * sizeof(double)), verts_side[3], BufferUsageHint.DynamicDraw);

            GL.Normal3(nx, ny, nz);
            //Draw them out
            GL.DrawArrays(beginMode, 0, 4);
        }

        void draw5(BeginMode beginMode, double size, float nx, float ny, float nz)
        {
            side_size[4] = size;
            if (verts_side[4] == null   //if the array is not initialized
                || side_size[4] != size)//if the new size is not identical to the old size
            {
                //Create a new array of vertices
                verts_side[4] = new double[] {
                -size, -size, +size,
                -size, -size, -size,
                +size, -size, -size,
                +size, -size, +size};
            }

            //Flush out the old data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[4].Length * sizeof(double)), IntPtr.Zero, BufferUsageHint.DynamicDraw);

            //Refilled with the new refreshed data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[4].Length * sizeof(double)), verts_side[4], BufferUsageHint.DynamicDraw);

            GL.Normal3(nx, ny, nz);
            //Draw them out
            GL.DrawArrays(beginMode, 0, 4);
        }

        void draw6(BeginMode beginMode, double size, float nx, float ny, float nz)
        {
            side_size[5] = size;
            if (verts_side[5] == null   //if the array is not initialized
                || side_size[5] != size)//if the new size is not identical to the old size
            {
                //Create a new array of vertices
                verts_side[5] = new double[] {
                -size, -size, -size,
                -size, +size, -size,
                +size, +size, -size,
                +size, -size, -size};
            }

            //Flush out the old data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[5].Length * sizeof(double)), IntPtr.Zero, BufferUsageHint.DynamicDraw);

            //Refilled with the new refreshed data
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts_side[5].Length * sizeof(double)), verts_side[5], BufferUsageHint.DynamicDraw);

            GL.Normal3(nx, ny, nz);
            //Draw them out
            GL.DrawArrays(beginMode, 0, 4);
        }

        static CubeModel()
        {
            GL.GenBuffers(1, out vbo);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

//            GL.NormalPointer(NormalPointerType.Float, 3*sizeof(float),IntPtr.Zero);
//            GL.VertexPointer(3, VertexPointerType.Float, 12*sizeof(float),(IntPtr)(3*sizeof(float)));

            GL.VertexPointer(3, VertexPointerType.Double, 3*sizeof(double), (IntPtr)(0));
        }

		public CubeModel() : base()
		{
		}
		
		private void renderCubeImmediate(double size, bool solid)
		{
			size /= 2;
			BeginMode beginMode;
			
			beginMode = (solid) ? BeginMode.Quads : BeginMode.LineLoop;
			
			GL.PushMatrix();
			
			GL.Begin(beginMode);
				GL.Normal3(1.0, 0.0, 0.0);
				GL.Vertex3(+size, -size, +size);
				GL.Vertex3(+size, -size, -size);
				GL.Vertex3(+size, +size, -size);
				GL.Vertex3(+size, +size, +size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(0.0, 1.0, 0.0);
				GL.Vertex3(+size, +size, +size);
				GL.Vertex3(+size, +size, -size);
				GL.Vertex3(-size, +size, -size);
				GL.Vertex3(-size, +size, +size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(0.0, 0.0, 1.0);
				GL.Vertex3(+size, +size, +size);
				GL.Vertex3(-size, +size, +size);
				GL.Vertex3(-size, -size, +size);
				GL.Vertex3(+size, -size, +size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(-1.0, 0.0, 0.0);
				GL.Vertex3(-size, -size, +size);
				GL.Vertex3(-size, +size, +size);
				GL.Vertex3(-size, +size, -size);
				GL.Vertex3(-size, -size, -size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(0.0,-1.0, 0.0);
				GL.Vertex3(-size, -size, +size);
				GL.Vertex3(-size, -size, -size);
				GL.Vertex3(+size, -size, -size);
				GL.Vertex3(+size, -size, +size);
			GL.End();
		
			GL.Begin(beginMode);
				GL.Normal3(0.0, 0.0, -1.0);
				GL.Vertex3(-size, -size, -size);
				GL.Vertex3(-size, +size, -size);
				GL.Vertex3(+size, +size, -size);
				GL.Vertex3(+size, -size, -size);
			GL.End();
			
			GL.PopMatrix();
		}

        private void renderCubeRetained(double size, bool solid)
        {


            size /= 2;
            BeginMode beginMode;

            GL.PushMatrix();

            beginMode = (solid) ? BeginMode.Quads : BeginMode.LineLoop;

            GL.EnableClientState(ArrayCap.VertexArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            draw1(beginMode, size, 1.0f, 0.0f, 0.0f);
            draw2(beginMode, size, 0.0f, 1.0f, 0.0f);
            draw3(beginMode, size, 0.0f, 0.0f, 1.0f);
            draw4(beginMode, size, -1.0f, 0.0f, 0.0f);
            draw5(beginMode, size, 0.0f, -1.0f, 0.0f);
            draw6(beginMode, size, 0.0f, 0.0f, -1.0f);

            GL.DisableClientState(ArrayCap.VertexArray);

            GL.PopMatrix();
        }

        public void render(double size)
        {
            if (material != null)
                material.apply();

            switch (renderMode)
            {				
                case RenderMode.SOLID:
                    GL.Color3(colorR, colorG, colorB);
					renderCubeRetained(size, true);
                	break;
				
                case RenderMode.WIRE:
                    GL.Color3(wireColorR, wireColorG, wireColorB);
                    renderCubeRetained(size, false);
					break;
				
				case RenderMode.SOLID_WIRE:
					GL.Color3(colorR, colorG, colorB);
                    renderCubeRetained(size, true);
					GL.Color3(wireColorR, wireColorG, wireColorB);
                    renderCubeRetained(size, false);
					break;
            }

            Material.unapply();
        }
    }
}
