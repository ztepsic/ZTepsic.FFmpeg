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
		/// Codec time base attribute
		/// </summary>
		public const string CODEC_TIME_BASE = "codec_time_base";

		/// <summary>
		/// Codec tag string attribute
		/// </summary>
		public const string CODEC_TAG_STRING = "codec_tag_string";

		/// <summary>
		/// Codec tag attribute
		/// </summary>
		public const string CODEC_TAG = "codec_tag";

		/// <summary>
		/// Duration attribute
		/// </summary>
		public const string DURATION = "duration";

		/// <summary>
		/// Average frame rate attribute
		/// </summary>
		public const string AVG_FRAME_RATE = "avg_frame_rate";

		/// <summary>
		/// R frame rate attribute
		/// </summary>
		public const string R_FRAME_RATE = "r_frame_rate";

		/// <summary>
		/// Time base attribute
		/// </summary>
		public const string TIME_BASE = "time_base";

		/// <summary>
		/// Start time attribute
		/// </summary>
		public const string START_TIME = "start_time";

		/// <summary>
		/// Bit rate attribute
		/// </summary>
		public const string BIT_RATE = "bit_rate";

		#region Video specific

		/// <summary>
		/// Width attribute
		/// </summary>
		public const string WIDTH = "width";

		/// <summary>
		/// Height attribute
		/// </summary>
		public const string HEIGHT = "height";

		/// <summary>
		/// 
		/// </summary>
		public const string HAS_B_FRAMES = "has_b_frames";

		/// <summary>
		/// Sample aspect ratio attribute
		/// </summary>
		public const string SAMPLE_ASPECT_RATIO = "sample_aspect_ratio";

		/// <summary>
		/// Sample aspect ratio attribute
		/// </summary>
		public const string DISPLAY_ASPECT_RATIO = "display_aspect_ratio";

		/// <summary>
		/// Pix fmt attribute
		/// </summary>
		public const string PIX_FMT = "pix_fmt";

		/// <summary>
		/// Level attribute
		/// </summary>
		public const string LEVEL = "level";

		/// <summary>
		/// Is avc attribute
		/// </summary>
		public const string IS_AVC = "is_avc";

		/// <summary>
		/// Nal length size attribute
		/// </summary>
		public const string NAL_LENGTH_SIZE = "nal_length_size";

		#endregion

		#region Audio specific

		/// <summary>
		/// Channels attribute
		/// </summary>
		public const string CHANNELS = "channels";

		/// <summary>
		/// Sample rate attribute
		/// </summary>
		public const string SAMPLE_RATE = "sample_rate";

		/// <summary>
		/// Sample fmt attribute
		/// </summary>
		public const string SAMPLE_FMT = "sample_rate";

		/// <summary>
		/// Bits per sample attribute
		/// </summary>
		public const string BITS_PER_SAMPLE = "bits_per_sample";

		#endregion

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

								if (streamNode.Attributes[START_TIME] != null) {
									try {
										mediaStreamInfo.StartTime = Decimal.Parse(streamNode.Attributes[START_TIME].InnerText,
																				 CultureInfo.InvariantCulture);
									} catch (Exception) {
										mediaStreamInfo.StartTime = 0;
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

								//r_frame_rate="25/1" ||"50/2"
								if (streamNode.Attributes[R_FRAME_RATE] != null) {
									mediaStreamInfo.FrameRateStr = streamNode.Attributes[R_FRAME_RATE].InnerText;
								}


								// avg_frame_rate="1000/23" || "500/21"
								if (streamNode.Attributes[AVG_FRAME_RATE] != null) {
									mediaStreamInfo.AvgFrameRateStr = streamNode.Attributes[AVG_FRAME_RATE].InnerText;
								}

								#region Video specific

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

								// sample_aspect_ratio="136:135" || "1:1"
								if (streamNode.Attributes[SAMPLE_ASPECT_RATIO] != null) {
									mediaStreamInfo.VideoSampleAspectRatioStr = streamNode.Attributes[SAMPLE_ASPECT_RATIO].InnerText;
								}

								//display_aspect_ratio="16:9"
								if (streamNode.Attributes[DISPLAY_ASPECT_RATIO] != null) {
									mediaStreamInfo.VideoDisplayAspectRatioStr = streamNode.Attributes[DISPLAY_ASPECT_RATIO].InnerText;
								}

								#endregion

								#region Audio specific

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

								#endregion

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
