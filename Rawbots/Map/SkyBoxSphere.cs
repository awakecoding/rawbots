using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	class SkyBoxSphere
	{
		private static float xOffset, yOffset, zOffset;
		private static OBJModel currEnv, box, sphere;

		static SkyBoxSphere()
		{
			xOffset = 0.0f;
			yOffset = 0.0f;
			zOffset = -20.0f;
			box = new OBJModel(Game.resourcePath + "/skybox.obj");
			sphere = new OBJModel(Game.resourcePath + "/sphere-colored_2.obj");
			currEnv = box;
		}

		public static void Render(Camera camera)
		{
			GL.PushMatrix();

			float[] camPos = camera.GetPosition();
			GL.Translate(camPos[0] + xOffset, camPos[1] + yOffset, camPos[2] + zOffset);
			currEnv.Render();

			GL.PopMatrix();
		}

		public static void ChangeEnvironment()
		{
			if (currEnv == box)
			{
				currEnv = sphere;
				zOffset = -10.0f;
			}
			else
			{
				currEnv = box;
				zOffset = -20.0f;
			}
		}
	}
}