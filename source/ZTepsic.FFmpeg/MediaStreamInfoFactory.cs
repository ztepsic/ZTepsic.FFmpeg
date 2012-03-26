using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// MediaStreamInfo factory
	/// </summary>
	public static class MediaStreamInfoFactory {

		#region Members

		/// <summary>
		/// Streams element
		/// </summary>
		public const string STREAMS_NODE = "streams";

		/// <summary>
		/// Stream element
		/// </summary>
		public const string STREAM_NODE = "stream";

		/// <summary>
		/// Index attribute
		/// </summary>
		public const string INDEX = "index";

		/// <summary>
		/// Codec type attribute
		/// </summary>
		public const string CODEC_TYPE = "codec_type";

		/// <summary>
		/// Codec name attribute
		/// </summary>
		public const string CODEC_NAME = "codec_name";

		/// <summary>
		/// Codec long name attribute
		/// </summary>
		public const string CODEC_LONG_NAME = "codec_long_name";

		/// <summary>
		/// Duration attribute
		/// </summary>
		public const string DURATION = "duration";

		/// <summary>
		/// Width attribute
		/// </summary>
		public const string WIDTH = "width";

		/// <summary>
		/// Height attribute
		/// </summary>
		public const string HEIGHT = "height";

		/// <summary>
		/// Average frame rate attribute
		/// </summary>
		public const string AVG_FRAME_RATE = "avg_frame_rate";

		/// <summary>
		/// Channels attribute
		/// </summary>
		public const string CHANNELS = "channels";

		/// <summary>
		/// Sample rate attribute
		/// </summary>
		public const string SAMPLE_RATE = "sample_rate";

		private const string CODEC_TYPE_VIDEO = "video";
		private const string CODEC_TYPE_AUDIO = "audio";

		/// <summary>
		/// XML extension
		/// </summary>
		public const string XML_EXT = ".xml";

		#endregion

		#region Methods

		/// <summary>
		/// Creates MediaStreamInfo from file.
		/// Method accepts file formats: xml
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>list of MediaStreamInfo objects</returns>
		public static IList<MediaStreamInfo> CreateFromFile(string fileName) {
			IList<MediaStreamInfo> mediaStreamInfos = null;

			if (File.Exists(fileName)) {
				var extension = Path.GetExtension(fileName);
				switch (extension) {
					case XML_EXT:
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.Load(fileName);
						mediaStreamInfos = createFromXml(xmlDoc);
						break;
				}
			}

			return mediaStreamInfos;

		}

		/// <summary>
		/// Creates MediaStreamInfo from xml string
		/// </summary>
		/// <param name="xml">xml string</param>
		/// <returns>list MediaStreamInfo objects</returns>
		public static IList<MediaStreamInfo> CreateFromXml(string xml) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);

			return createFromXml(xmlDoc);
		}

		/// <summary>
		/// Creates MediaStreamInfo from xml stream
		/// </summary>
		/// <param name="stream">xml stream</param>
		/// <returns>list MediaStreamInfo objects</returns>
		public static IList<MediaStreamInfo> CreateFromXml(Stream stream) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(stream);

			return createFromXml(xmlDoc);
		}

		/// <summary>
		/// Creates MediaStreamInfo from XmlDocument
		/// </summary>
		/// <param name="xmlDoc">XmlDocument containing data for creating MediaStreamInfo object.</param>
		/// <returns>list of MediaStreamInfo objects</returns>
		private static IList<MediaStreamInfo> createFromXml(XmlDocument xmlDoc) {
			IList<MediaStreamInfo> mediaStreamInfos = new List<MediaStreamInfo>();

			if (xmlDoc.DocumentElement != null) {
				XmlNodeList streamNodes = xmlDoc.DocumentElement.SelectNodes(STREAMS_NODE + "/" + STREAM_NODE);

				if (streamNodes != null) {
					foreach (XmlNode streamNode in streamNodes) {
						if (streamNode != null && streamNode.Attributes != null) {
							int index = 0;
							if (streamNode.Attributes[INDEX] != null) {
								try {
									index = Int32.Parse(streamNode.Attributes[INDEX].InnerText, CultureInfo.InvariantCulture);
								} catch (Exception) {
									index = 0;
								}
							}

							var codec = streamNode.Attributes[CODEC_NAME] != null ? streamNode.Attributes[CODEC_NAME].InnerText : null;

							MediaStreamInfo.MediaStreamType type = MediaStreamInfo.MediaStreamType.None;
							if (streamNode.Attributes[CODEC_TYPE] != null) {
								switch (streamNode.Attributes[CODEC_TYPE].InnerText) {
									case CODEC_TYPE_VIDEO:
										type = MediaStreamInfo.MediaStreamType.Video;
										break;
									case CODEC_TYPE_AUDIO:
										type = MediaStreamInfo.MediaStreamType.Audio;
										break;
								}
							}

							if (codec != null && type != MediaStreamInfo.MediaStreamType.None) {
								var mediaStreamInfo = new MediaStreamInfo(index, codec, type);

								if (streamNode.Attributes[CODEC_LONG_NAME] != null) {
									mediaStreamInfo.CodecLongName = streamNode.Attributes[CODEC_LONG_NAME].InnerText;
								}

								if (streamNode.Attributes[WIDTH] != null) {
									try {
										mediaStreamInfo.VideoWidth = Int32.Parse(streamNode.Attributes[WIDTH].InnerText, CultureInfo.InvariantCulture);
									} catch (Exception) {
										mediaStreamInfo.VideoWidth = 0;
									}
								}

								if (streamNode.Attributes[HEIGHT] != null) {
									try {
										mediaStreamInfo.VideoHeight = Int32.Parse(streamNode.Attributes[HEIGHT].InnerText,
										                                          CultureInfo.InvariantCulture);
									} catch (Exception) {
										mediaStreamInfo.VideoHeight = 0;
									}
								}

								if (streamNode.Attributes[DURATION] != null) {
									try {
										mediaStreamInfo.Duration = Decimal.Parse(streamNode.Attributes[DURATION].InnerText,
										                                         CultureInfo.InvariantCulture);
									} catch (Exception) {
										mediaStreamInfo.Duration = 0;
									}
								}


								if (streamNode.Attributes[AVG_FRAME_RATE] != null) {
									try {
										mediaStreamInfo.VideoFps = Int32.Parse(streamNode.Attributes[AVG_FRAME_RATE].InnerText);
									} catch (Exception) {
										mediaStreamInfo.VideoFps = 0;
									}
								}

								if (streamNode.Attributes[CHANNELS] != null) {
									try {
										mediaStreamInfo.AudioChannels = Int32.Parse(streamNode.Attributes[CHANNELS].InnerText);
									} catch (Exception) {
										mediaStreamInfo.AudioChannels = 0;
									}
								}

								if (streamNode.Attributes[SAMPLE_RATE] != null) {
									try {
										mediaStreamInfo.AudioSampleRate = Decimal.Parse(streamNode.Attributes[SAMPLE_RATE].InnerText,
										                                                CultureInfo.InvariantCulture);
									} catch (Exception) {
										mediaStreamInfo.AudioSampleRate = 0;
									}
								}

								mediaStreamInfos.Add(mediaStreamInfo);
							}
						}
					}

				}
			}

			return mediaStreamInfos;
		}

		#endregion


	}
}
