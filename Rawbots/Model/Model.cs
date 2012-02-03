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
		protected float colorR;
		protected float colorG;
		protected float colorB;
		
		protected float wireColorR;
		protected float wireColorG;
		protected float wireColorB;
		
		protected RenderMode renderMode;
		
		public Model()
		{
			renderMode = RenderMode.SOLID;
			
			colorR = 0.22f;
			colorG = 0.22f;
			colorB = 0.22f;
			
			wireColorR = 1.0f;
			wireColorG = 0.0f;
			wireColorB = 0.0f;
		}
		
        public virtual void SetRenderMode(RenderMode renderMode)
        {
            this.renderMode = renderMode;
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
	}
}
