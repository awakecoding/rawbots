using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rawbots
{
	class RobotCamera : FirstPersonCamera
	{
		private Robot robot;
		private float mapWidth;
		private float mapHeight;
		private bool attached;

		public RobotCamera(float x, float y, float z)
			: base(x, y, z)
		{
			robot = null;
			mapWidth = 50.0f;
			mapHeight = 50.0f;
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
			base.PerformActions(actions);

			if (attached)
			{
				float x, y;
				float[] position;
				position = getPosition();

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

				position[0] = x;
				position[2] = y;
				setPosition(position[0], position[1], position[2], position[3]);

				robot.PosX = x;
				robot.PosY = y;
			}
		}
	}
}
