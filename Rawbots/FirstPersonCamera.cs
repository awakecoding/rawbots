using System;

namespace Rawbots
{
	class FirstPersonCamera : Camera
	{
		public FirstPersonCamera(float x, float y, float z)
			: base(x, y, z)
		{
		}

		public override void PerformActions(Action actions)
		{
			if ((actions & Action.MOVE_UP) != 0)
				MoveUp();
			if ((actions & Action.MOVE_DOWN) != 0)
				MoveDown();
			if ((actions & Action.MOVE_LEFT) != 0)
				MoveLeft();
			if ((actions & Action.MOVE_RIGHT) != 0)
				MoveRight();
			if ((actions & Action.ROTATE_RIGHT) != 0)
				RotateRight();
			if ((actions & Action.ROTATE_LEFT) != 0)
				RotateLeft();
		}
	}
}
