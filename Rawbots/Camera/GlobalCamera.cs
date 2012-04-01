using System;

namespace Rawbots
{
	class GlobalCamera : Camera
	{
        public GlobalCamera(float x, float y, float z)
			: base(x, y, z)
		{
		}

		/*
		public override bool MouseDeltaMotion(int dx, int dy)
		{
			if (dx != 0)
			{
				RotateDeltaX(dx);
			}

			return true;
		}
		*/
	}
}
