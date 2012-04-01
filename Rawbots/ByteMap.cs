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

		public static byte TRUE = 0xFF;
		public static byte FALSE = 0x00;

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

		public bool IsPositionSet(int x, int y)
		{
			if ((x >= 0 && x < width) && (y >= 0 && y < height))
				return (map[x, y] == ByteMap.FALSE) ? false : true;

			return false;
		}

		public ByteMap CombineWith(ByteMap otherByteMap)
		{
			Byte[,] newBytes;
			Byte[,] otherBytes;
			ByteMap newByteMap;

			if ((this.Width != otherByteMap.Width) || (this.Height != otherByteMap.Height))
				return null;

			newByteMap = new ByteMap(width, height);

			newBytes = newByteMap.Bytes;
			otherBytes = otherByteMap.Bytes;

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					newBytes[i, j] = (byte) (otherBytes[i, j] | map[i, j]);
				}
			}

			return newByteMap;
		}
	}
}
