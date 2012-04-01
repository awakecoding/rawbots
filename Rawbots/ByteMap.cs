using System;

namespace Rawbots
{
	public class ByteMap
	{
		private int width;
		private int height;
		private byte[,] map;

		public ByteMap(int width, int height)
		{
			this.width = width;
			this.height = height;
			map = new byte[this.width, this.height];
		}

		public int Width
		{
			get { return this.width; }
		}

		public int Height
		{
			get { return this.height; }
		}

		public byte[,] Bytes
		{
			get { return map; }
		}
	}
}
