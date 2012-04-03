using System;
using System.IO;
using System.Xml;
using OpenTK.Graphics.OpenGL;

namespace Rawbots
{
	public class MapFile
	{
		public static Map Load(string filename)
		{
			Map map = null;

			if (!File.Exists(filename))
				return null;

			XmlTextReader xml = new XmlTextReader(filename);

			while (xml.Read())
			{
				switch (xml.NodeType)
				{
					case XmlNodeType.Element:

						if (xml.Name.Equals("Map"))
							map = LoadMap(xml);
						
						break;
				}

				if (map != null)
					break;
			}

			return map;
		}

		private static Map LoadMap(XmlTextReader xml)
		{
			int width;
			int height;
			Map map = null;

			width = Int32.Parse(xml.GetAttribute("width"));
			height = Int32.Parse(xml.GetAttribute("height"));

			map = new Map(width, height);

			while (xml.Read())
			{
				switch (xml.NodeType)
				{
					case XmlNodeType.Element:

						if (xml.Name.Equals("Robot"))
						{
							map.AddRobot(LoadRobot(xml));
						}
						else if (xml.Name.Equals("Base"))
						{
							map.AddBase(LoadBase(xml));
						}
						else if (xml.Name.Equals("Factory"))
						{
							map.AddFactory(LoadFactory(xml));
						}
						else if (xml.Name.Equals("Terrain"))
						{
							map.SetTerrain(LoadTerrain(xml));
						}
						else if (xml.Name.Equals("Block"))
						{
							map.AddBlock(LoadBlock(xml));
						}
						else if (xml.Name.Equals("RemoteControlUnit"))
						{
							map.SetRemoteControlUnit(LoadRemoteControlUnit(xml));
						}
						
						break;

					case XmlNodeType.EndElement:

						if (xml.Name.Equals("Map"))
							return map;
				
						break;
				}
			}

			return null;
		}

		private static Robot LoadRobot(XmlTextReader xml)
		{
			int x, y;
			Robot robot = null;

			x = Int32.Parse(xml.GetAttribute("x"));
			y = Int32.Parse(xml.GetAttribute("y"));

			robot = new Robot(x, y);

			string friendly = xml.GetAttribute("f");
			if (friendly != null)
				robot.MarkFriendly();

			while (xml.Read())
			{
				switch (xml.NodeType)
				{
					case XmlNodeType.Element:

						if (xml.Name.Equals("AntiGravChassis"))
							robot.AddChassis(new AntiGravChassis());
						else if (xml.Name.Equals("BipodChassis"))
							robot.AddChassis(new BipodChassis());
						else if (xml.Name.Equals("TrackedChassis"))
							robot.AddChassis(new TrackedChassis());
						else if (xml.Name.Equals("CannonWeapon"))
							robot.AddWeapon(new CannonWeapon());
						else if (xml.Name.Equals("MissilesWeapon"))
							robot.AddWeapon(new MissilesWeapon());
						else if (xml.Name.Equals("NuclearWeapon"))
							robot.AddWeapon(new NuclearWeapon());
						else if (xml.Name.Equals("PhasersWeapon"))
							robot.AddWeapon(new PhasersWeapon());
						else if (xml.Name.Equals("Electronics"))
							robot.AddElectronics(new Electronics());

						break;

					case XmlNodeType.EndElement:

						if (xml.Name.Equals("Robot"))
							return robot;
						
						break;
				}
			}

			return robot;
		}

		private static Base LoadBase(XmlTextReader xml)
		{
			int x, y;
			Base robotBase;

			x = Int32.Parse(xml.GetAttribute("x"));
			y = Int32.Parse(xml.GetAttribute("y"));

			robotBase = new Base(x, y);

			return robotBase;
		}

		private static Factory LoadFactory(XmlTextReader xml)
		{
			int x, y;
			string type;
			Factory factory = null;

			x = Int32.Parse(xml.GetAttribute("x"));
			y = Int32.Parse(xml.GetAttribute("y"));
			type = xml.GetAttribute("type");

			if (type.Equals("AntiGravChassis"))
				factory = new AntiGravChassisFactory(x, y);
			else if (type.Equals("BipodChassis"))
				factory = new BipodChassisFactory(x, y);
			else if (type.Equals("TrackedChassis"))
				factory = new TrackedChassisFactory(x, y);
			else if (type.Equals("CannonWeapon"))
				factory = new CannonWeaponFactory(x, y);
			else if (type.Equals("MissilesWeapon"))
				factory = new MissilesWeaponFactory(x, y);
			else if (type.Equals("NuclearWeapon"))
				factory = new NuclearWeaponFactory(x, y);
			else if (type.Equals("PhasersWeapon"))
				factory = new PhasersWeaponFactory(x, y);
			else if (type.Equals("Electronics"))
				factory = new ElectronicsFactory(x, y);

			return factory;
		}

		private static Terrain LoadTerrain(XmlTextReader xml)
		{
			int width, height;
			Terrain terrain = null;

			width = Int32.Parse(xml.GetAttribute("width"));
			height = Int32.Parse(xml.GetAttribute("height"));

			terrain = new Terrain(width, height);

			while (xml.Read())
			{
				switch (xml.NodeType)
				{
					case XmlNodeType.Element:

						if (xml.Name.Equals("Tile"))
						{
							Tile tile = LoadTile(xml);
							terrain.SetTile(tile, tile.PosX, tile.PosY);
						}
						else if (xml.Name.Equals("Pit"))
						{
							Pit pit = LoadPit(xml);
							terrain.SetTile(pit, pit.PosX, pit.PosY);
						}
						else if (xml.Name.Equals("Boundary"))
						{
							Boundary boundary = LoadBoundary(xml);
							terrain.SetTile(boundary, boundary.PosX, boundary.PosY);
						}

						break;

					case XmlNodeType.EndElement:

						if (xml.Name.Equals("Terrain"))
							return terrain;
						
						break;
				}
			}

			return terrain;
		}

		private static Tile LoadTile(XmlTextReader xml)
		{
			int x, y;
			string type;
			Tile tile = null;

			x = Int32.Parse(xml.GetAttribute("x"));
			y = Int32.Parse(xml.GetAttribute("y"));
			type = xml.GetAttribute("type");

			if (type.Equals("LightRubble"))
				tile = new LightRubblePile(x, y);
			else if (type.Equals("MediumRubble"))
				tile = new MediumRubblePile(x, y);
			else if (type.Equals("HeavyRubble"))
				tile = new HeavyRubblePile(x, y);

			return tile;
		}

		private static Pit LoadPit(XmlTextReader xml)
		{
			int x, y;
			Pit pit = null;

			x = Int32.Parse(xml.GetAttribute("x"));
			y = Int32.Parse(xml.GetAttribute("y"));

			pit = new Pit(x, y);

			while (xml.Read())
			{
				switch (xml.NodeType)
				{
					case XmlNodeType.Element:

						if (xml.Name.Equals("North"))
							pit.setVisible(Pit.NORTH);
						else if (xml.Name.Equals("East"))
							pit.setVisible(Pit.EAST);
						else if (xml.Name.Equals("West"))
							pit.setVisible(Pit.WEST);
						else if (xml.Name.Equals("South"))
							pit.setVisible(Pit.SOUTH);

						break;

					case XmlNodeType.EndElement:

						if (xml.Name.Equals("Pit"))
							return pit;
						
						break;
				}
			}

			return pit;
		}

		private static Boundary LoadBoundary(XmlTextReader xml)
		{
			int x, y;
			Boundary boundary = null;

			x = Int32.Parse(xml.GetAttribute("x"));
			y = Int32.Parse(xml.GetAttribute("y"));
			string s = xml.GetAttribute("t");
			if (s != null)
				boundary = new Boundary(x, y, true);
			else
				boundary = new Boundary(x, y);

			return boundary;
		}

		private static Block LoadBlock(XmlTextReader xml)
		{
			int x, y;
			string type;
			Block block = null;

			x = Int32.Parse(xml.GetAttribute("x"));
			y = Int32.Parse(xml.GetAttribute("y"));
			type = xml.GetAttribute("type");

			if (type.Equals("FullPlain"))
				block = new FullPlainBlock(x, y);
			else if (type.Equals("HalfPlain"))
				block = new HalfPlainBlock(x, y);
			else if (type.Equals("FullSquareHole"))
				block = new FullSquareHoleBlock(x, y);
			else if (type.Equals("HalfSquareHole"))
				block = new HalfSquareHoleBlock(x, y);

			return block;
		}

		private static RemoteControlUnit LoadRemoteControlUnit(XmlTextReader xml)
		{
			int x, y;
			RemoteControlUnit remoteControlUnit = null;

			x = Int32.Parse(xml.GetAttribute("x"));
			y = Int32.Parse(xml.GetAttribute("y"));

			remoteControlUnit = new RemoteControlUnit(x, y);
			remoteControlUnit.AttachLight(new Light(LightName.Light4));

			return remoteControlUnit;
		}
	}
}
