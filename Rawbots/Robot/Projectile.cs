using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	class Projectile : Drawable
	{
		float PosX, PosY, DirX, DirY;
		
		const int LIFE_TIME = 1000;
		int lifeTime = LIFE_TIME; //The time of life the projectile is in function till it dies or gets hit

		SphereModel flare = new SphereModel(1.0f);

		public Projectile(float x, float y, float xdir, float yDir)
		{
			PosX = x; PosY = y;
			DirX = xdir; DirY = yDir;
			flare.SetColor(0.75f, 0.0f, 0.0f);
			flare.SetRenderMode(RenderMode.SOLID);
		}

		public override void Render()
		{
			
			flare.render();
		}
	}
}
