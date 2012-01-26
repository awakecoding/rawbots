using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public abstract class Resource
	{
		public Resource()
		{
		}
		
		public void Push()
		{
			GL.PushMatrix();
		}
		
		public void Pop()
		{
			GL.PopMatrix();
		}
		
		public void RenderAll()
		{
			Push();
			Render();
			Pop();
		}
		
		public abstract void Render();
	}
}

