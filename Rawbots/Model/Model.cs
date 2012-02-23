using System;

namespace Rawbots
{
	public enum RenderMode
	{
		SOLID,
		WIRE,
		SOLID_WIRE
	};
	
	public abstract class Model
	{
		protected bool show;

		protected float colorR;
		protected float colorG;
		protected float colorB;
		
		protected float wireColorR;
		protected float wireColorG;
		protected float wireColorB;
		
		protected RenderMode renderMode;

        protected float modelHeight;

		protected double[] sint;
		protected double[] cost;

		public Model()
		{
			show = true;

			colorR = 0.22f;
			colorG = 0.22f;
			colorB = 0.22f;
			
			wireColorR = 1.0f;
			wireColorG = 0.0f;
			wireColorB = 0.0f;

            modelHeight = 1.0f;

			renderMode = RenderMode.SOLID;
		}

		public void Show(bool show)
		{
			this.show = show;
		}

        public virtual void SetRenderMode(RenderMode renderMode)
        {
            this.renderMode = renderMode;
        }

        public void SetHeight(float h)
        {
            modelHeight = h;
        }

        public float GetHeight()
        {
            return modelHeight;
        }

        public void SetColor(float r, float g, float b)
        {
            colorR = r;
            colorG = g;
            colorB = b;
        }

        public void SetWireColor(float r, float g, float b)
        {
            wireColorR = r;
            wireColorG = g;
            wireColorB = b;
        }

		protected void CircleTable(ref double[] sint, ref double[] cost, int n)
		{
			int i;

			int size = Math.Abs(n);

			double angle = 2 * Math.PI / (double)((n == 0) ? 1 : n);

			sint = new double[size + 1];
			cost = new double[size + 1];

			sint[0] = 0.0f;
			cost[0] = 1.0f;

			for (i = 1; i < size; i++)
			{
				sint[i] = Math.Sin(angle * i);
				cost[i] = Math.Cos(angle * i);
			}

			sint[size] = sint[0];
			cost[size] = cost[0];
		}
	}
}
