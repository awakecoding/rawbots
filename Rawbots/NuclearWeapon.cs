using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class NuclearWeapon : Weapon
	{
		public NuclearWeapon()
		{
		}
		
		public override int getCost()
		{
			return 20;
		}
		
		public override int getRange()
		{
			return 8;
		}
		
		public override int getLethality()
		{
			/*
			 * Nuclear weapons destroy all robots and factories within an 8 mile
			 * radius of the robot carrying the device - this includes the carrying robot.
			 * This is currently the only method we have to destroy factories and war bases!
			 */
			
			return 0;
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

