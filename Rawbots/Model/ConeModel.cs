using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	class ConeModel : Model
	{
//		static uint vbo;

		static ConeModel()
		{
//			GL.GenBuffers(1, out vbo);

//			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

//			GL.VertexPointer(3, VertexPointerType.Double, 3 * sizeof(double), (IntPtr)(0));
		}

		private void renderWireCone(double dBase, double dHeight, int iSlices, int iStacks)
		{
			int i, j;

			double z = 0.0f;
			double r = dBase;

			double zStep = dHeight / ((iStacks > 0) ? iStacks : 1);
			double rStep = dBase / ((iStacks > 0) ? iStacks : 1);

			double cosn = (dHeight / Math.Sqrt(dHeight * dHeight + dBase * dBase));
			double sinn = (dBase / Math.Sqrt(dHeight * dHeight + dBase * dBase));

			if (sint == null || cost == null) //if they are blank slates, then create a new table
				CircleTable(ref sint, ref cost, -iSlices);

			if (sint != null || cost != null) //if they were already created...
				if (sint.Length != Math.Abs(iSlices) + 1) //but.. the size was different
					CircleTable(ref sint, ref cost, -iSlices);

			for (i = 0; i < iStacks; i++)
			{
				GL.Begin(BeginMode.LineLoop);

				for (j = 0; j < iSlices; j++)
				{
					GL.Normal3(cost[j] * sinn, sint[j] * sinn, cosn);
					GL.Vertex3(cost[j] * r, sint[j] * r, z);
				}

				GL.End();

				z += zStep;
				r -= rStep;
			}

			r = dBase;

			GL.Begin(BeginMode.Lines);

			for (j = 0; j < iSlices; j++)
			{
				GL.Normal3(cost[j] * sinn, sint[j] * sinn, cosn);
				GL.Vertex3(cost[j] * r, sint[j] * r, 0.0f);
				GL.Vertex3(0.0f, 0.0f, dHeight);
			}

			GL.End();
		}

        private void renderSolidCone(double dBase, double dHeight, int iSlices, int iStacks)
        {
            int i, j;

            double z0, z1;
            double r0, r1;

            double zStep = dHeight / ((iStacks > 0) ? iStacks : 1);
            double rStep = dBase / ((iStacks > 0) ? iStacks : 1);

            double cosn = (dHeight / Math.Sqrt(dHeight * dHeight + dBase * dBase));
            double sinn = (dBase / Math.Sqrt(dHeight * dHeight + dBase * dBase));

            if (sint == null || cost == null) //if they are blank slates, then create a new table
                CircleTable(ref sint, ref cost, -iSlices);

            if (sint != null || cost != null) //if they were already created...
                if (sint.Length != Math.Abs(iSlices) + 1) //but.. the size was different
                    CircleTable(ref sint, ref cost, -iSlices);

            z0 = 0.0f;
            z1 = zStep;

            r0 = dBase;
            r1 = r0 - rStep;

            GL.Begin(BeginMode.TriangleFan);

            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Vertex3(0.0f, 0.0f, z0);

            for (j = 0; j <= iSlices; j++)
                GL.Vertex3(cost[j] * r0, sint[j] * r0, z0);

            GL.End();
        
            /* Cover each stack with a quad strip, except the top stack */

            for( i=0; i<iStacks-1; i++ )
            {
                GL.Begin(BeginMode.QuadStrip);

                    for(j=0; j<=iSlices; j++)
                    {
                        GL.Normal3(cost[j]*cosn, sint[j]*cosn, sinn);
                        GL.Vertex3(cost[j]*r0,   sint[j]*r0,   z0  );
                        GL.Vertex3(cost[j]*r1,   sint[j]*r1,   z1  );
                    }

                    z0 = z1; z1 += zStep;
                    r0 = r1; r1 -= rStep;

                GL.End();
            }

            /* The top stack is covered with individual triangles */

            GL.Begin(BeginMode.Triangles);

            GL.Normal3(cost[0]*sinn, sint[0]*sinn, cosn);

            for (j=0; j<iSlices; j++)
            {
                GL.Vertex3(cost[j+0]*r0, sint[j+0]*r0, z0);
                GL.Vertex3(0.0f, 0.0f, dHeight);
                GL.Normal3(cost[j+1]*sinn, sint[j+1]*sinn, cosn);
                GL.Vertex3(cost[j+1]*r0, sint[j+1]*r0, z0);
            }

            GL.End();
        }

        public void render(double dBase, double dHeight, int iSlices, int iStacks)
        {
			GL.PushMatrix();

            switch (renderMode)
            {
                case RenderMode.SOLID:
                    GL.Color3(colorR, colorG, colorB);
                    renderSolidCone(dBase, dHeight, iSlices, iStacks);
                    break;
                case RenderMode.WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    renderWireCone(dBase, dHeight, iSlices, iStacks);
                    break;
                case RenderMode.SOLID_WIRE:
                    GL.Color3(colorR, colorG, colorB);
                    renderSolidCone(dBase, dHeight, iSlices, iStacks);
                    GL.Color3(wireColorR, wireColorG, wireColorB);
                    renderWireCone(dBase, dHeight, iSlices, iStacks);
                    break;
            }

			GL.PopMatrix();
        }
	}
}
