using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// Creates MediaInfo object from given sources
	/// </summary>
	public static class MediaInfoFactory {

		/// <summary>
		/// Creates MediaInfo object from given file source
		/// </summary>
		/// <param name="filePath">full file path to file with data for MadiaInfo object creation</param>
		/// <returns>MediaInfo object</returns>
		public static MediaInfo Create(string filePath) {
			return null;
		}

		private static MediaInfo createBasedOnXml(string filePath) {
			MediaInfo mediaInfo = null; 
			XmlReader xmlReader = XmlReader.Create(filePath);
			while (xmlReader.Read()) {
				if(xmlReader.Name.Equals("format") && xmlReader.NodeType == XmlNodeType.Element) {
					// format_name
					// format_long_name
					// bit_rate
					// duration
					// size
				}
			}
			return null;
		}
	}
}
