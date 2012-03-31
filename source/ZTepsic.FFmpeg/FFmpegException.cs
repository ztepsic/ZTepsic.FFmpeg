using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// FFmpeg error
	/// </summary>
	public class FFmpegException : Exception {

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

		/// <summary>
		/// FFmpeg error code
		/// </summary>
		public int Code { get; private set; }

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Initializes a new instance of the FFmpegException class
		/// </summary>
		public FFmpegException() { }

		/// <summary>
		/// Initializes a new instance of the FFmpegException class with a specified error messsage.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="code">FFmpeg error code</param>
		public FFmpegException(string message, int code) : base(message) {
			Code = code;
		}

		/// <summary>
		/// Initializes a new instance of the FFmpegException class with a specified error message and a reference to the inner exception
		/// that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="code">FFmpeg error code</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public FFmpegException(string message, int code, Exception innerException) : base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the FFmpegException class with serialized data.
		/// </summary>
		/// <param name="info">The SerializationInfo thet holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected FFmpegException(SerializationInfo info, StreamingContext context) : base(info, context) {}

		#endregion

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

					exception = new FFmpegException(message, code);

				}
			}

			return exception;
		}

		#endregion

	}
}
