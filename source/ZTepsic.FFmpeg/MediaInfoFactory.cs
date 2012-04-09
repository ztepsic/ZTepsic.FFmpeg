using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// Creates MediaInfo object from given sources
	/// </summary>
	public static class MediaInfoFactory {

		#region Members

		/// <summary>
		/// XML extension
		/// </summary>
		public const string XML_EXT = ".xml";

		#endregion

		#region Methods

		/// <summary>
		/// Creates MediaInfo from file.
		/// Method accepts file formats: xml
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>MediaInfo object</returns>
		public static MediaInfo CreateFromFile(string fileName) {
			MediaInfo mediaInfo = null;

			if (File.Exists(fileName)) {
				var extension = Path.GetExtension(fileName);
				switch (extension) {
					case XML_EXT:
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.Load(fileName);
						mediaInfo = createFromXml(xmlDoc);
						break;
				}
			}

			return mediaInfo;

		}

		/// <summary>
		/// Creates MediaInfo from xml string
		/// </summary>
		/// <param name="xml">xml string</param>
		/// <returns>MediaInfo object</returns>
		public static MediaInfo CreateFromXml(string xml) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);

			return createFromXml(xmlDoc);
		}

		/// <summary>
		/// Creates MediaInfo from xml stream
		/// </summary>
		/// <param name="stream">xml stream</param>
		/// <returns>MediaInfo object</returns>
		public static MediaInfo CreateFromXml(Stream stream) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(stream);

			return createFromXml(xmlDoc);
		}

		/// <summary>
		/// Creates MediaInfo from XmlDocument
		/// </summary>
		/// <param name="xmlDoc">XmlDocument containing data for creating MediaInfo object.</param>
		/// <returns>MediaInfo object</returns>
		private static MediaInfo createFromXml(XmlDocument xmlDoc) {
			FFmpegException fFmpegException = FFmpegException.CreateFromXml(xmlDoc);
			if(fFmpegException != null) {
				throw fFmpegException;
			}

			MediaInfo mediaInfo = null;

			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromXml(xmlDoc);
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromXml(xmlDoc);

			if(mediaFormatInfo != null) {
				mediaInfo = new MediaInfo(mediaFormatInfo, mediaStreamInfos);
			}


			return mediaInfo;
		}

		#endregion

	}
}
