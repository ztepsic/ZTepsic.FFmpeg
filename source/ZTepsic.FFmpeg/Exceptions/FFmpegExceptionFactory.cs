using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ZTepsic.FFmpeg.Exceptions {
	internal static class FFmpegExceptionFactory {

		#region Members

		/// <summary>
		/// Error element
		/// </summary>
		public const string FORMAT_NODE = "error";

		/// <summary>
		/// File name attribute
		/// </summary>
		public const string CODE = "code";

		/// <summary>
		/// Number of streams attribute
		/// </summary>
		public const string MESSAGE = "string";

		/// <summary>
		/// XML extension
		/// </summary>
		public const string XML_EXT = ".xml";

		#endregion

		#region Methods

		#region Methods

		/// <summary>
		/// Creates FFmpegException from file.
		/// Method accepts file formats: xml
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>FFmpegException object</returns>
		public static FFmpegException CreateFromFile(string fileName) {
			FFmpegException fFmpegException = null;

			if (File.Exists(fileName)) {
				var extension = Path.GetExtension(fileName);
				switch (extension) {
					case XML_EXT:
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.Load(fileName);
						fFmpegException = CreateFromXml(xmlDoc);
						break;
				}
			}

			return fFmpegException;

		}

		/// <summary>
		/// Creates FFmpegException from xml string
		/// </summary>
		/// <param name="xml">xml string</param>
		/// <returns>FFmpegException object</returns>
		public static FFmpegException CreateFromXml(string xml) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);

			return CreateFromXml(xmlDoc);
		}

		/// <summary>
		/// Creates FFmpegException from xml stream
		/// </summary>
		/// <param name="stream">xml stream</param>
		/// <returns>FFmpegException object</returns>
		public static FFmpegException CreateFromXml(Stream stream) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(stream);

			return CreateFromXml(xmlDoc);
		}

		/// <summary>
		/// Creates FFmpegException from XmlDocument
		/// </summary>
		/// <param name="xmlDoc">XmlDocument containing data for creating FFmpegException object.</param>
		/// <returns>FFmpegException object</returns>
		public static FFmpegException CreateFromXml(XmlDocument xmlDoc) {
			FFmpegException exception = null;

			if (xmlDoc.DocumentElement != null) {
				XmlNode formatNode = xmlDoc.DocumentElement.SelectSingleNode(FORMAT_NODE);

				if (formatNode != null && formatNode.Attributes != null) {
					var code = formatNode.Attributes[CODE] != null
					           	? Int32.Parse(formatNode.Attributes[CODE].InnerText)
					           	: 0;

					var message = formatNode.Attributes[MESSAGE] != null ? formatNode.Attributes[MESSAGE].InnerText : null;

					switch (code) {
						case -1:
							exception = new OperationNotPermittedException();
							break;
						case -5:
							exception = new IOException();
							break;
						case -10060:
							exception = new UnknownException();
							break;
						case -1094995529:
							exception = new InvalidDataException();
							break;
						default:
							exception = new FFmpegException(message, code);
							break;
					}

				}
			}

			return exception;
		}

		#endregion

		#endregion
	}
}
