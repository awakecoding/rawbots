using System;

namespace Rawbots
{
	class FirstPersonCamera : Camera
	{
        float delta = 1.0f;
		float deltaAngleYawLeft = 0.0f;
        float deltaAngleYawRight = 0.0f;
        float deltaAnglePitchUp = 0.0f;
        float deltaAnglePitchDown = 0.0f;
        const float MAX_ANGLEYAW = 45.0f;

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

            if((actions & Action.TILT_LEFT) != 0)
                TiltLeft();
            if ((actions & Action.TILT_RIGHT) != 0)
                TiltRight();
            if ((actions & Action.TILT_UP) != 0)
                TiltUp();
            if ((actions & Action.TILT_DOWN) != 0)
                TiltDown();
        }

        public void TiltLeft()
        {
            if(deltaAngleYawLeft <= MAX_ANGLEYAW)
            {
                deltaAngleYawLeft+= delta;
                RotateLeft();
            }
        }

        public void TiltRight()
        {
            if (deltaAngleYawRight <= MAX_ANGLEYAW)
            {
                deltaAngleYawRight += delta;
                RotateRight();
            }
        }

        public void TiltUp()
        {
            if (deltaAnglePitchUp <= MAX_ANGLEYAW)
            {
                deltaAnglePitchUp += delta;
                RotateUp();
            }
        }

        public void TiltDown()
        {
            if (deltaAnglePitchDown <= MAX_ANGLEYAW)
            {
                deltaAnglePitchDown += delta;
                RotateDown();
            }
        }

        public override void IdleAction()
        {
            if(deltaAngleYawLeft > 0.0f)
            {
                deltaAngleYawLeft -= delta;
                RotateRight();
            }

            if(deltaAngleYawRight > 0.0f)
            {
                deltaAngleYawRight -= delta;
                RotateLeft();
            }

            if (deltaAnglePitchUp > 0.0f)
            { 
                deltaAnglePitchUp -= delta;
                RotateDown();
            }

            if (deltaAnglePitchDown > 0.0f)
            {
                deltaAnglePitchDown -= delta;
                RotateUp();
            }
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
