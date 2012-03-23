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
		string szFilename;

		private static uint lastTexID = 0;
		private static Dictionary<string, Texture> textures = new Dictionary<string,Texture>();

		public static Texture AcquireTexture(string filename)
		{
			if (textures.ContainsKey(filename))
				return textures[filename];

			Texture texture = new Texture(filename);
			textures.Add(filename, texture);

			return texture;
		}

		private Texture(string filename)
		{
			LoadTexture(filename);
        }

		private void LoadTexture(string filename)
		{
			szFilename = filename;
			bmp = new Bitmap(filename);
			Bitmap b = bmp;
			b.RotateFlip(RotateFlipType.RotateNoneFlipY);
			BitmapData bd = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.GenTextures(1, out texId);

			GL.BindTexture(TextureTarget.Texture2D, texId);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Modulate);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bd.Width, bd.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bd.Scan0);

			b.UnlockBits(bd);
		}

		public void apply()
		{
			if (lastTexID == texId) /* if the id is the same, save the trouble of rebinding. */
				return;
			
			GL.BindTexture(TextureTarget.Texture2D, texId);

			lastTexID = texId;
		}

		public void unapply()
		{
			//GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public void Release()
		{
			GL.DeleteTexture(texId);
		}

		private TextureUnit numToActiveUnit(uint index)
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
