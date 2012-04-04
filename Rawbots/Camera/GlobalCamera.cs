using System;

namespace Rawbots
{
	class GlobalCamera : Camera
	{
		bool mouseEnabled = false;

        public GlobalCamera(float x, float y, float z)
			: base(x, y, z)
		{
		}

		public override void PerformActions(Action actions)
		{
			base.PerformActions(actions);

			if ((actions & Action.TOGGLE_MOUSE) != 0)
				mouseEnabled = (mouseEnabled) ? false : true;
		}

		public override bool MouseDeltaMotion(int dx, int dy)
		{
			if (!mouseEnabled)
				return false;

			if (Math.Abs(dx) > Math.Abs(dy))
			{
				RotateDeltaX(dx / 4);
			}
			else
			{
				RotateDeltaY(dy / 4);
			}

			return true;
		}
	}
}
