using System;
using System.Collections.Generic;
using System.Text;

namespace Rawbots
{
	class RobotCamera : FirstPersonCamera
	{
		private Robot robot;
		private float mapWidth;
		private float mapHeight;
		private bool attached;

		private bool tiltLeft;
		private bool tiltRight;
		private bool tiltUp;
		private bool tiltDown;

		public RobotCamera(float x, float y, float z)
			: base(x, y, z)
		{
			robot = null;
			mapWidth = 50.0f;
			mapHeight = 50.0f;
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
			bool active;
			base.PerformActions(actions);

			active = ((actions & Action.ACTIVE) != 0);

			if ((actions & Action.TILT_LEFT) != 0)
				tiltLeft = active;
			if ((actions & Action.TILT_RIGHT) != 0)
				tiltRight = active;
			if ((actions & Action.TILT_UP) != 0)
				tiltUp = active;
			if ((actions & Action.TILT_DOWN) != 0)
				tiltDown = active;

			if (attached)
			{
				float x, y;
				float angle;
				float[] position;

				position = GetPosition();
				angle = GetXZViewAngle();

				x = position[0];
				y = position[2];

				if (x < 0.0f)
					x = 0.0f;

				if (x > mapWidth)
					x = mapWidth;

				if (y < -mapHeight)
					y = -mapHeight;

				if (y > 0.0f)
					y = 0.0f;

                Console.WriteLine("Robot(" + x + "," + y + " @ " + angle + ")");

				robot.PosX = x;
				robot.PosY = y;

				robot.Angle = -angle;

				if ((actions & Action.TOGGLE_LIGHT) != 0)
					robot.ToggleLight();
			}
		}

        public override void IdleAction()
        {
            base.IdleAction();
        }
	}
}
