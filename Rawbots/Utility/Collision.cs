using System;
using System.Collections.Generic;
using System.Text;

namespace Rawbots.Utility
{
	class Collision
	{
		private Collision() { }

		public static bool IntersectionTest2D(float p1x, float p1y, float p1w, float p1h, 
									   float p2x, float p2y, float p2w, float p2h)
		{
			if (p1x >= p2x && p1x <= p2x + p2w)
				if (p1y >= p2y && p1y <= p2y + p2h)
					return true;

			float p = (p1x + p1w);
			if (p >= p2x && p <= p2x + p2w)
				if (p1y >= p2y && p1y <= p2y + p2h)
					return true;

			p = (p1y + p1w);
			if (p1x >= p2x && p1x <= p2x + p2w)
				if (p >= p2y && p <= p2y + p2h)
					return true;

			float p2 = (p1x + p1w);
			if (p2 >= p2x && p2 <= p2x + p2w)
				if (p >= p2y && p <= p2y + p2h)
					return true;
			
			return false;
		}

	}
}
