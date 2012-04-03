using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class Projectile : Drawable
	{
		public float PosX, PosY, DirX, DirY;
		
		const int LIFE_TIME = 50;
		int lifeTime = LIFE_TIME; //The time of life the projectile is in function till it dies or gets hit

		SphereModel flare = new SphereModel(0.25f);

		public Robot self;

		public Projectile(Robot robot, float x, float y, float xdir, float yDir)
		{
			self = robot; //to make sure it doesn't get damaged by its own fire.
			PosX = x; PosY = y;
			DirX = xdir; DirY = yDir;
			flare.SetColor(0.75f, 0.0f, 0.0f);
			flare.SetRenderMode(RenderMode.SOLID);
		}

		public bool IsDead()
		{
			return !(lifeTime > 0);
		}

		public override void Render()
		{
			if (lifeTime > 0)
			{
				PosX += DirX;
				PosY += DirY;

				GL.Translate(PosX, 0.5f, PosY);
				flare.render();

				lifeTime--;
			}
		}
	}
}
