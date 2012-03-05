using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	class Face
	{ 
		public uint[] VertIdx;
		public uint[] TexCoordIdx;
		public uint[] NormalIdx;

        public bool isQuad = false;

		public Face(uint[][] Data)
		{
            isQuad = (Data[0].Length == 4) ? true : false;
            VertIdx = isQuad ? new uint[4] : new uint[3];

			VertIdx[0] = Data[0][0] - 1;
			VertIdx[1] = Data[0][1] - 1;
			VertIdx[2] = Data[0][2] - 1;
            if (isQuad) //if we have a quad
                VertIdx[3] = Data[0][3] - 1;

			if (Data[1] != null)
			{
				TexCoordIdx = (Data[1].Length == 3) ? new uint[3] : new uint[4];
				TexCoordIdx[0] = Data[1][0] - 1;
				TexCoordIdx[1] = Data[1][1] - 1;
				TexCoordIdx[2] = Data[1][2] - 1;
                if (isQuad)
                    TexCoordIdx[3] = Data[1][3] - 1;
			}

			if (Data[2] != null)
			{
				NormalIdx = (Data[2].Length == 3) ? new uint[3] : new uint[4];
				NormalIdx[0] = Data[2][0] - 1;
				NormalIdx[1] = Data[2][1] - 1;
				NormalIdx[2] = Data[2][2] - 1;
                if (isQuad)
                    NormalIdx[3] = Data[2][3] - 1;
			}
		}
	}

	class OBJModel
	{
		private List<float[]> Vertices = new List<float[]>();
		private List<float[]> Normals = new List<float[]>();
		private List<float[]> TexCoords = new List<float[]>();
		private List<Face> Faces = new List<Face>();

		private Material material;
		private Bitmap imgImage;

		private bool HasTexCoords;
		private bool HasNormals;

		private string pathFileName;
		private string relativePath = "";

		public OBJModel(string filename)
		{
			pathFileName = filename;
			Load(filename);
		}

		public bool Load(string filename)
		{
			char[] cLine = new char[256];
			char[] seperators = new char[] { ' ', '/' };
			string sLine;
			string[] s;
			FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(fs);

			string absolutePath = fs.Name;
			char[] sep = new char[] { '\\' };
			string[] splitAbsolutePath = absolutePath.Split(sep);

			for (int i = 0; i < splitAbsolutePath.Length - 1; i++)
				relativePath += splitAbsolutePath[i] + "\\";

			int ic = sr.Read();
			uint lineNumber = 1;

			while (ic != -1)
			{
				char c = (char)ic;

				if (c == 'v')
				{
					int iNext = sr.Read();
					float[] fTemp;

					if (iNext == ' ' || iNext == '\t')
					{
                        fTemp = new float[3];
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);                      

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);
						float.TryParse(s[2], out fTemp[2]);
                            
						Vertices.Add(fTemp);
					}
					else if (iNext == 't')
					{
						fTemp = new float[2];
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);

						TexCoords.Add(fTemp);

						HasTexCoords = true;
					}
					else if (iNext == 'n')
					{
						fTemp = new float[3];
						sLine = sr.ReadLine();
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

						float.TryParse(s[0], out fTemp[0]);
						float.TryParse(s[1], out fTemp[1]);
						float.TryParse(s[2], out fTemp[2]);

						Normals.Add(fTemp);

						HasNormals = true;
					}
					else
					{
						sLine = sr.ReadLine(); 
					}
				}
				else if (c == 'f')
				{ 
					uint[][] iTemp = new uint[3][];
                    int faceSize;
                    bool isQuad = false;

					sLine = sr.ReadLine();

					if(HasTexCoords && HasNormals)
					{
						//f v/t/n v/t/n v/t/n (v/t/n)
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                        faceSize = s.Length/3;
                        isQuad = (faceSize == 4);

						iTemp[0] = isQuad ? new uint[4] : new uint[3];
						uint.TryParse(s[0], out iTemp[0][0]);
						uint.TryParse(s[3], out iTemp[0][1]);
						uint.TryParse(s[6], out iTemp[0][2]);
                        if(isQuad)
                            uint.TryParse(s[9], out iTemp[0][3]);

						iTemp[1] = isQuad ? new uint[4] : new uint[3];
						uint.TryParse(s[1], out iTemp[1][0]);
						uint.TryParse(s[4], out iTemp[1][1]);
						uint.TryParse(s[7], out iTemp[1][2]);
                        if (isQuad)
                            uint.TryParse(s[10], out iTemp[1][3]);

						iTemp[2] = isQuad ? new uint[4] : new uint[3];
						uint.TryParse(s[2], out iTemp[2][0]);
						uint.TryParse(s[5], out iTemp[2][1]);
						uint.TryParse(s[8], out iTemp[2][2]);
                        if (isQuad)
                            uint.TryParse(s[11], out iTemp[2][3]);

						Assertion(iTemp[0][0] - 1, (uint)Vertices.Count, lineNumber);
						Assertion(iTemp[0][1] - 1, (uint)Vertices.Count, lineNumber);
						Assertion(iTemp[0][2] - 1, (uint)Vertices.Count, lineNumber);
                        if(isQuad)
                            Assertion(iTemp[0][3] - 1, (uint)Vertices.Count, lineNumber);

						Assertion(iTemp[1][0] - 1, (uint)TexCoords.Count, lineNumber);
						Assertion(iTemp[1][1] - 1, (uint)TexCoords.Count, lineNumber);
						Assertion(iTemp[1][2] - 1, (uint)TexCoords.Count, lineNumber);
                        if (isQuad)
                            Assertion(iTemp[1][3] - 1, (uint)TexCoords.Count, lineNumber);

						Assertion(iTemp[2][0] - 1, (uint)Normals.Count, lineNumber);
						Assertion(iTemp[2][1] - 1, (uint)Normals.Count, lineNumber);
						Assertion(iTemp[2][2] - 1, (uint)Normals.Count, lineNumber);
                        if (isQuad)
                            Assertion(iTemp[2][3] - 1, (uint)Normals.Count, lineNumber);

						Faces.Add(new Face(iTemp));
					}
					else if (HasTexCoords && !HasNormals)
					{
						//f v/t v/t v/t (v/t)
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                        faceSize = s.Length / 3;
                        isQuad = (faceSize == 4);

						iTemp[0] = isQuad ? new uint[4] : new uint[3];
						uint.TryParse(s[0], out iTemp[0][0]);
						uint.TryParse(s[2], out iTemp[0][1]);
						uint.TryParse(s[4], out iTemp[0][2]);
                        if(isQuad)
                            uint.TryParse(s[6], out iTemp[0][3]);

						iTemp[1] = isQuad ? new uint[4] : new uint[3];
						uint.TryParse(s[1], out iTemp[1][0]);
						uint.TryParse(s[3], out iTemp[1][1]);
						uint.TryParse(s[5], out iTemp[1][2]);
                        if(isQuad)
                            uint.TryParse(s[7], out iTemp[1][3]);

						Assertion(iTemp[0][0] - 1, (uint)Vertices.Count, lineNumber);
						Assertion(iTemp[0][1] - 1, (uint)Vertices.Count, lineNumber);
						Assertion(iTemp[0][2] - 1, (uint)Vertices.Count, lineNumber);
                        if(isQuad)
                            Assertion(iTemp[0][3] - 1, (uint)Vertices.Count, lineNumber);

						Assertion(iTemp[1][0] - 1, (uint)TexCoords.Count, lineNumber);
						Assertion(iTemp[1][1] - 1, (uint)TexCoords.Count, lineNumber);
						Assertion(iTemp[1][2] - 1, (uint)TexCoords.Count, lineNumber);
                        if(isQuad)
                            Assertion(iTemp[1][3] - 1, (uint)Vertices.Count, lineNumber);

						Faces.Add(new Face(iTemp));
					}
					else if (!HasTexCoords && HasNormals)
					{
						//f v//n v//n v//n (v//n)
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                        faceSize = s.Length / 3;
                        isQuad = (faceSize == 4);

						iTemp[0] = isQuad ? new uint[4] : new uint[3];
						uint.TryParse(s[0], out iTemp[0][0]);
						uint.TryParse(s[2], out iTemp[0][1]);
						uint.TryParse(s[4], out iTemp[0][2]);
                        if(isQuad)
                            uint.TryParse(s[6], out iTemp[0][3]);

						iTemp[2] = isQuad ? new uint[4] : new uint[3];
						uint.TryParse(s[1], out iTemp[2][0]);
						uint.TryParse(s[3], out iTemp[2][1]);
						uint.TryParse(s[5], out iTemp[2][2]);
                        if(isQuad)
                            uint.TryParse(s[7], out iTemp[2][3]);

						Assertion(iTemp[0][0] - 1, (uint)Vertices.Count, lineNumber);
						Assertion(iTemp[0][1] - 1, (uint)Vertices.Count, lineNumber);
						Assertion(iTemp[0][2] - 1, (uint)Vertices.Count, lineNumber);
                        if(isQuad)
                            Assertion(iTemp[0][3] - 1, (uint)Vertices.Count, lineNumber);

						Assertion(iTemp[2][0] - 1, (uint)Normals.Count, lineNumber);
						Assertion(iTemp[2][1] - 1, (uint)Normals.Count, lineNumber);
						Assertion(iTemp[2][2] - 1, (uint)Normals.Count, lineNumber);
                        if(isQuad)
                            Assertion(iTemp[2][3] - 1, (uint)Vertices.Count, lineNumber);

						Faces.Add(new Face(iTemp));
					}
					else
					{
						s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                        faceSize = s.Length / 3;
                        isQuad = (faceSize == 4);

						iTemp[0] = isQuad ? new uint[4] : new uint[3];
						uint.TryParse(s[0], out iTemp[0][0]);
						uint.TryParse(s[1], out iTemp[0][1]);
						uint.TryParse(s[2], out iTemp[0][2]);
                        if(isQuad)
                            uint.TryParse(s[3], out iTemp[0][3]);

						Assertion(iTemp[0][0] - 1, (uint)Vertices.Count, lineNumber);
						Assertion(iTemp[0][1] - 1, (uint)Vertices.Count, lineNumber);
						Assertion(iTemp[0][2] - 1, (uint)Vertices.Count, lineNumber);
                        if(isQuad)
                            Assertion(iTemp[0][3] - 1, (uint)Vertices.Count, lineNumber);

						Faces.Add(new Face(iTemp));
					}
				}
				else if (c == 'm') //Material Library File
				{
					sLine = sr.ReadLine();

					s = sLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
									
					material = new Material(relativePath + s[1]);
				}
				else if (c != '\n')
				{
					sLine = sr.ReadLine();
				}

				lineNumber++;
				ic = sr.Read();
			}

			sr.Close();
			fs.Close();

			return true;
		}

		public void Assertion(uint i, uint j, uint lineNo)
		{
			if (i > j)
				throw new Exception("OBJ File ERROR: " + pathFileName + " IndexOutOfBounds " + i + " > " + j + " @ line " + lineNo);
				
		}

		public void Render()
		{
			Face f;
			uint[] tc2indices, n3indices, v3indices;
			float[] tc2, n3, v3;
			
			int iNumFaces = Faces.Count;

            if (HasTexCoords)
            {
                GL.Enable(EnableCap.Texture2D);
                if (material != null)
                    material.apply();
            }

			GL.Begin(Faces[0].isQuad ? BeginMode.Quads : BeginMode.Triangles);

			if (HasTexCoords && HasNormals)
			{
				for (int i = 0; i < iNumFaces; i++)
				{
					f = Faces[i];

					tc2indices = f.TexCoordIdx;
					n3indices = f.NormalIdx;
					v3indices = f.VertIdx;

					tc2 = TexCoords[(int)tc2indices[0]];
					n3 = Normals[(int)n3indices[0]];
					v3 = Vertices[(int)v3indices[0]];

					GL.TexCoord2(tc2);
					GL.Normal3(n3);
					GL.Vertex3(v3);

					tc2 = TexCoords[(int)tc2indices[1]];
					n3 = Normals[(int)n3indices[1]];
					v3 = Vertices[(int)v3indices[1]];

					GL.TexCoord2(tc2);
					GL.Normal3(n3);
					GL.Vertex3(v3);

					tc2 = TexCoords[(int)tc2indices[2]];
					n3 = Normals[(int)n3indices[2]];
					v3 = Vertices[(int)v3indices[2]];

					GL.TexCoord2(tc2);
					GL.Normal3(n3);
					GL.Vertex3(v3);

                    if (Faces[i].isQuad)
                    {
                        tc2 = TexCoords[(int)tc2indices[3]];
                        n3 = Normals[(int)n3indices[3]];
                        v3 = Vertices[(int)v3indices[3]];

                        GL.TexCoord2(tc2);
                        GL.Normal3(n3);
                        GL.Vertex3(v3);
                    }
                    
				}
			}
			else if (!HasTexCoords && HasNormals)
			{
				for (int i = 0; i < iNumFaces; i++)
				{
					f = Faces[i];

					n3indices = f.NormalIdx;
					v3indices = f.VertIdx;

					n3 = Normals[(int)n3indices[0]];
					v3 = Vertices[(int)v3indices[0]];

					GL.Normal3(n3);
					GL.Vertex3(v3);

					n3 = Normals[(int)n3indices[1]];
					v3 = Vertices[(int)v3indices[1]];

					GL.Normal3(n3);
					GL.Vertex3(v3);

					n3 = Normals[(int)n3indices[2]];
					v3 = Vertices[(int)v3indices[2]];

					GL.Normal3(n3);
					GL.Vertex3(v3);

                    if (Faces[i].isQuad)
                    {
                        n3 = Normals[(int)n3indices[3]];
                        v3 = Vertices[(int)v3indices[3]];

                        GL.Normal3(n3);
                        GL.Vertex3(v3);
                    }
				}
			}
			else if (HasTexCoords && !HasNormals)
			{
				for (int i = 0; i < iNumFaces; i++)
				{
					f = Faces[i];

					tc2indices = f.TexCoordIdx;
					v3indices = f.VertIdx;

					tc2 = TexCoords[(int)tc2indices[0]];
					v3 = Vertices[(int)v3indices[0]];

					GL.TexCoord2(tc2);
					GL.Vertex3(v3);

					tc2 = TexCoords[(int)tc2indices[1]];
					v3 = Vertices[(int)v3indices[1]];

					GL.TexCoord2(tc2);
					GL.Vertex3(v3);

					tc2 = TexCoords[(int)tc2indices[2]];
					v3 = Vertices[(int)v3indices[2]];

					GL.TexCoord2(tc2);
					GL.Vertex3(v3);

                    if (Faces[i].isQuad)
                    {
                        tc2 = TexCoords[(int)tc2indices[3]];
                        v3 = Vertices[(int)v3indices[3]];

                        GL.TexCoord2(tc2);
                        GL.Vertex3(v3);
                    }
				}
			}
			else
			{
				for (int i = 0; i < iNumFaces; i++)
				{
					f = Faces[i];

					v3indices = f.VertIdx;

					v3 = Vertices[(int)v3indices[0]];

					GL.Vertex3(v3);

					v3 = Vertices[(int)v3indices[1]];

					GL.Vertex3(v3);

					v3 = Vertices[(int)v3indices[2]];

					GL.Vertex3(v3);

                    if (Faces[i].isQuad)
                    {
                        v3 = Vertices[(int)v3indices[3]];

                        GL.Vertex3(v3);
                    }
				}
			}

			GL.End();

            if (HasTexCoords)
                GL.Disable(EnableCap.Texture2D);
		}
	}
}
