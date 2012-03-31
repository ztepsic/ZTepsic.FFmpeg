using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// MediaFormatInfo factory
	/// </summary>
	public static class MediaFormatInfoFactory {

		#region Members

		/// <summary>
		/// Format element
		/// </summary>
		public const string FORMAT_NODE = "format";

		/// <summary>
		/// File name attribute
		/// </summary>
		public const string FILE_NAME = "filename";

		/// <summary>
		/// Number of streams attribute
		/// </summary>
		public const string NB_STREAMS = "nb_streams";

		/// <summary>
		/// Format name attribute
		/// </summary>
		public const string FORMAT = "format_name";

		/// <summary>
		/// Format long name attribute
		/// </summary>
		public const string FORMAT_LONG_NAME = "format_long_name";

		/// <summary>
		/// Start time attribute
		/// </summary>
		public const string START_TIME = "start_time";

		/// <summary>
		/// Duration attribute
		/// </summary>
		public const string DURATION = "duration";

		/// <summary>
		/// Bit rate attribute
		/// </summary>
		public const string BIT_RATE = "bit_rate";

		/// <summary>
		/// File size attribute
		/// </summary>
		public const string FILE_SIZE = "size";


		/// <summary>
		/// XML extension
		/// </summary>
		public const string XML_EXT = ".xml";

		#endregion

		#region Methods

		/// <summary>
		/// Creates MediaFromatInfo from file.
		/// Method accepts file formats: xml
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>MediaFormatInfo object</returns>
		public static MediaFormatInfo CreateFromFile(string fileName) {
			MediaFormatInfo mediaFormatInfo = null;

			if(File.Exists(fileName)) {
				var extension = Path.GetExtension(fileName);
				switch (extension) {
					case XML_EXT:
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.Load(fileName);
						mediaFormatInfo = CreateFromXml(xmlDoc);
						break;
				}
			}

			return mediaFormatInfo;

		}

		/// <summary>
		/// Creates MediaFormatInfo from xml string
		/// </summary>
		/// <param name="xml">xml string</param>
		/// <returns>MediaFormatInfo object</returns>
		public static MediaFormatInfo CreateFromXml(string xml) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);

			return CreateFromXml(xmlDoc);
		}

		/// <summary>
		/// Creates MediaFormatInfo from xml stream
		/// </summary>
		/// <param name="stream">xml stream</param>
		/// <returns>MediaFormatInfo object</returns>
		public static MediaFormatInfo CreateFromXml(Stream stream) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(stream);

			return CreateFromXml(xmlDoc);
		}

		/// <summary>
		/// Creates MediaFormatInfo from XmlDocument
		/// </summary>
		/// <param name="xmlDoc">XmlDocument containing data for creating MediaFormatInfo object.</param>
		/// <returns>MediaFormatInfo object</returns>
		public static MediaFormatInfo CreateFromXml(XmlDocument xmlDoc) {
			MediaFormatInfo mediaFormatInfo = null;

			if (xmlDoc.DocumentElement != null) {
				XmlNode formatNode = xmlDoc.DocumentElement.SelectSingleNode(FORMAT_NODE);

				if (formatNode != null && formatNode.Attributes != null) {
					var fileName = formatNode.Attributes[FILE_NAME] != null ? formatNode.Attributes[FILE_NAME].InnerText : null;
					var format = formatNode.Attributes[FORMAT] != null ? formatNode.Attributes[FORMAT].InnerText : null;

					if(fileName != null && format != null) {
						mediaFormatInfo = new MediaFormatInfo(fileName, format);

						if (formatNode.Attributes[FORMAT_LONG_NAME] != null) {
							mediaFormatInfo.FormatLongName = formatNode.Attributes[FORMAT_LONG_NAME].InnerText;
						}

						if(formatNode.Attributes[BIT_RATE] != null) {
							try {
								mediaFormatInfo.Bitrate = Decimal.Parse(formatNode.Attributes[BIT_RATE].InnerText, CultureInfo.InvariantCulture);	
							} catch(Exception) {
								mediaFormatInfo.Bitrate = 0;
							}
						}
						
						if(formatNode.Attributes[DURATION] != null) {
							try {
								mediaFormatInfo.Duration = Decimal.Parse(formatNode.Attributes[DURATION].InnerText, CultureInfo.InvariantCulture);		
							} catch(Exception) {
								mediaFormatInfo.Duration = 0;
							}
						}

						if (formatNode.Attributes[START_TIME] != null) {
							try {
								mediaFormatInfo.StartTime = Decimal.Parse(formatNode.Attributes[START_TIME].InnerText, CultureInfo.InvariantCulture);	
							} catch(Exception) {
								mediaFormatInfo.StartTime = 0;
							}
						}

						if (formatNode.Attributes[FILE_SIZE] != null) {
							try {
								mediaFormatInfo.FileSize = Decimal.Parse(formatNode.Attributes[FILE_SIZE].InnerText, CultureInfo.InvariantCulture);	
							} catch(Exception) {
								mediaFormatInfo.FileSize = 0;
							}
						}
						
					}

				}
			}

			return mediaFormatInfo;
		}

		#endregion

	}
}