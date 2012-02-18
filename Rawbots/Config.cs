/**
 * RawBots: an awesome robot game
 * 
 * Copyright 2012 Marc-Andre Moreau <marcandre.moreau@gmail.com>
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this file,
 * You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Rawbots
{
	public class Config
	{
		[XmlAttribute("ScreenWidth")]
		public int ScreenWidth { get; set; }

		[XmlAttribute("ScreenHeight")]
		public int ScreenHeight { get; set; }

		[XmlAttribute("Fullscreen")]
		public bool Fullscreen { get; set; }
		
		public Config()
		{
			ScreenWidth = 800;
			ScreenHeight = 600;
			Fullscreen = false;
		}
		
		public static void Save(Config config)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Config));
			TextWriter textWriter = new StreamWriter(@"config.xml");
			serializer.Serialize(textWriter, config);
			textWriter.Close();
		}
		
		public static Config Load()
		{
			Config config;
			
			if (File.Exists(@"config.xml"))
			{
				XmlSerializer deserializer = new XmlSerializer(typeof(Config));
				TextReader textReader = new StreamReader(@"config.xml");
				config = (Config) deserializer.Deserialize(textReader);
				textReader.Close();
			}
			else
			{
				config = new Config();
				Save(config);
			}
			
			return config;
		}
	}
}
