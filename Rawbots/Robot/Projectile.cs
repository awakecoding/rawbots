using System;
using System.Collections.Generic;
using System.Text;

namespace Rawbots
{
	class Projectile : Drawable
	{
		const int LIFE_TIME = 1000;
		int lifeTime = LIFE_TIME; //The time of life the projectile is in function till it dies or gets hit

		SphereModel flare = new SphereModel(1.0f);

		public Projectile()
		{
			flare.SetColor(0.75f, 0.0f, 0.0f);
			flare.SetRenderMode(RenderMode.SOLID);
		}

		public override void Render()
		{
			flare.render();
		}
	}
}
