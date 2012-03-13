using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class Texture
	{
		Bitmap bmp;
		uint texId;
		//uint numTex = 0;

		public Texture(string filename)
		{
			addTexture(filename);
        }

		public void addTexture(string filename)
		{
			bmp = new Bitmap(filename);
			Bitmap b = bmp;
			b.RotateFlip(RotateFlipType.Rotate180FlipNone);
			BitmapData bd = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			//GL.ActiveTexture(TextureUnit.Texture0);
			GL.GenTextures(1, out texId);

			GL.BindTexture(TextureTarget.Texture2D, texId);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bd.Width, bd.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bd.Scan0);

			b.UnlockBits(bd);
		}

		public void apply()
		{
			GL.BindTexture(TextureTarget.Texture2D, texId);
		}

		public void unapply()
		{
			//GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public void Release()
		{
			GL.DeleteTexture(texId);
		}

		public TextureUnit numToActiveUnit(uint index)
		{
			switch (index)
			{ 
				case 0:
					return TextureUnit.Texture0;
				case 1:
					return TextureUnit.Texture1;
			}

			return TextureUnit.Texture0;
		}

	}
}
