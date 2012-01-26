using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class Electronics : Resource
	{
		public Electronics()
		{
			/*
			 * This module increases weapon accuracy, giving a notional added
			 * range of 3 miles to each weapon type, Advance warning of attack
			 * contributes to the slightly increased resistance to damage from
			 * enemy fire when this unit is fitted.
			 */
		}
		
		public override void Render()
		{
			GL.Begin(BeginMode.Triangles);

			GL.Color3(1.0f, 1.0f, 0.0f);
			GL.Vertex3(-1.0f, -1.0f, 4.0f);
			GL.Color3(1.0f, 0.0f, 0.0f);
			GL.Vertex3(1.0f, -1.0f, 4.0f);
			GL.Color3(0.2f, 0.9f, 1.0f);
			GL.Vertex3(0.0f, 1.0f, 4.0f);
			
			GL.End();
		}
	}
}

