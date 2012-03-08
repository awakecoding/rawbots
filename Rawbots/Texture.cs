using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	class Texture
	{
		Bitmap[] bmp = new Bitmap[2];
		uint[] texId = new uint[2];
		uint numTex = 0;

		public Texture(string filename)
		{
			addTexture(filename);
        }

		public void addTexture(string filename)
		{
			bmp[numTex] = new Bitmap(filename);
			Bitmap b = bmp[numTex];
			BitmapData bd = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.ActiveTexture(TextureUnit.Texture0);
			GL.GenTextures(1, out texId[numTex]);

			GL.BindTexture(TextureTarget.Texture2D, texId[numTex]);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bd.Width, bd.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bd.Scan0);

			b.UnlockBits(bd);
			numTex++;
		}

		public void apply()
		{
			//GL.ActiveTexture(numToActiveUnit());
			GL.BindTexture(TextureTarget.Texture2D, texId[numTex]);
		}

		public void unapply()
		{
			//GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public void Release()
		{
			for(uint i = 0; i < numTex; i++)
				GL.DeleteTexture(texId[i]);
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
