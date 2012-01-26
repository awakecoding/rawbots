using System;

namespace Rawbots
{
	public abstract class Weapon : Resource
	{
		public Weapon()
		{
		}
		
		public abstract int getCost();
		public abstract int getRange();
		public abstract int getLethality();
	}
}

