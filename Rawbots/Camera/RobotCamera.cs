using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace Rawbots
{
	class RobotCamera : FirstPersonCamera
	{
		private Robot robot;
		private bool attached;

		private bool tiltLeft;
		private bool tiltRight;
		private bool tiltUp;
		private bool tiltDown;

		public RobotCamera(float x, float y, float z)
			: base(x, y, z)
		{
			robot = null;
			tiltLeft = false;
			tiltRight = false;
			tiltUp = false;
			tiltDown = false;
		}

		public Robot Attach(Robot robot)
		{
			Robot previousRobot = robot;
			this.robot = robot;
			attached = true;
			return previousRobot;
		}

		public Robot Detach()
		{
			Robot previousRobot = robot;
			attached = false;
			robot = null;
			return previousRobot;
		}

		public override void PerformActions(Action actions)
		{
			float x, y;
			float angle;
			bool active;
			float prevX;
			float prevY;
			float[] position;
			float[] prevPosition;
			bool cancelMove = false;

			if (!attached)
				return;

			active = ((actions & Action.ACTIVE) != 0);

			if ((actions & Action.TOGGLE_LIGHT) != 0)
				robot.ToggleLight();

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

			if ((actions & Action.TILT_LEFT) != 0)
				tiltLeft = active;
			if ((actions & Action.TILT_RIGHT) != 0)
				tiltRight = active;
			if ((actions & Action.TILT_UP) != 0)
				tiltUp = active;
			if ((actions & Action.TILT_DOWN) != 0)
				tiltDown = active;

			prevPosition = GetPosition();
			prevX = prevPosition[0];
			prevY = prevPosition[2];

			position = GetPosition();
			x = position[0];
			y = -position[2];

			angle = GetXZViewAngle();

			if (x < 0.0f)
				x = 0.0f;

			if (x > map.GetWidth())
				x = map.GetWidth();

			if (y < 0.0f)
				y = 0.0f;

			if (y > map.GetHeight())
				y = map.GetHeight();

			if (map.IsColliding((int) x, (int) y))
				cancelMove = true;

			if (cancelMove)
			{
				if ((actions & Action.MOVE_UP) != 0)
					MoveDown();
				if ((actions & Action.MOVE_DOWN) != 0)
					MoveUp();
				if ((actions & Action.MOVE_LEFT) != 0)
					MoveRight();
				if ((actions & Action.MOVE_RIGHT) != 0)
					MoveUp();
			}
			else
			{
				//Console.WriteLine("Robot(" + x + "," + y + " @ " + angle + ")");

				robot.PosX = x;
				robot.PosY = -y;
			}

			robot.Angle = -angle;
		}

        public override void IdleAction()
        {
            base.IdleAction();
        }
	}
}
