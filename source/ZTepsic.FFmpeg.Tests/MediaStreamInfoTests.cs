using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class MediaStreamInfoTests {

		private string xml;

		private int videoStreamIndex = 0;
		private MediaStreamInfo.MediaStreamType videoStreamType = MediaStreamInfo.MediaStreamType.Video;
		private string videoStreamCodec = "h264";
		private string videoStreamCodecLongName = "H.264 / AVC / MPEG-4 AVC / MPEG-4 part 10";
		private decimal videoStreamDuration = 0;
		private int videoWidth = 512;
		private int videoHeight = 288;
		private int videoFps = 0;


		private int audioStreamIndex = 1;
		private MediaStreamInfo.MediaStreamType audioStreamType = MediaStreamInfo.MediaStreamType.Audio;
		private string audioStreamCodec = "aac";
		private string audioStreamCodecLongName = "Advanced Audio Coding";
		private decimal audioStreamDuration = 0;
		private int audioChannels = 1;
		private decimal audioSampleRate = 48000;
		
		[SetUp]
		public void SetUp() {
			XDocument xDoc = new XDocument();
			var rootElem = new XElement("ffprobe");
			var streamsElem = new XElement(MediaStreamInfoFactory.STREAMS_NODE);
			var streamVideoElem = new XElement(MediaStreamInfoFactory.STREAM_NODE);
			streamVideoElem.Add(new XAttribute(MediaStreamInfoFactory.INDEX, videoStreamIndex));
			streamVideoElem.Add(new XAttribute(MediaStreamInfoFactory.CODEC_TYPE, videoStreamType.ToString().ToLower()));
			streamVideoElem.Add(new XAttribute(MediaStreamInfoFactory.CODEC_NAME, videoStreamCodec));
			streamVideoElem.Add(new XAttribute(MediaStreamInfoFactory.CODEC_LONG_NAME, videoStreamCodecLongName));
			streamVideoElem.Add(new XAttribute(MediaStreamInfoFactory.WIDTH, videoWidth));
			streamVideoElem.Add(new XAttribute(MediaStreamInfoFactory.HEIGHT, videoHeight));
			streamVideoElem.Add(new XAttribute(MediaStreamInfoFactory.AVG_FRAME_RATE, videoFps));
			streamVideoElem.Add(new XAttribute(MediaStreamInfoFactory.DURATION, videoStreamDuration.ToString(CultureInfo.InvariantCulture)));

			streamsElem.Add(streamVideoElem);

			var streamAudioElem = new XElement(MediaStreamInfoFactory.STREAM_NODE);
			streamAudioElem.Add(new XAttribute(MediaStreamInfoFactory.INDEX, audioStreamIndex));
			streamAudioElem.Add(new XAttribute(MediaStreamInfoFactory.CODEC_TYPE, audioStreamType.ToString().ToLower()));
			streamAudioElem.Add(new XAttribute(MediaStreamInfoFactory.CODEC_NAME, audioStreamCodec));
			streamAudioElem.Add(new XAttribute(MediaStreamInfoFactory.CODEC_LONG_NAME, audioStreamCodecLongName));
			streamAudioElem.Add(new XAttribute(MediaStreamInfoFactory.CHANNELS, audioChannels));
			streamAudioElem.Add(new XAttribute(MediaStreamInfoFactory.SAMPLE_RATE, audioSampleRate));
			streamAudioElem.Add(new XAttribute(MediaStreamInfoFactory.DURATION, videoStreamDuration.ToString(CultureInfo.InvariantCulture)));

			streamsElem.Add(streamAudioElem);

			rootElem.Add(streamsElem);;
			xDoc.Add(rootElem);

			xml = xDoc.ToString();

		}

		[Test]
		public void Can_Create_MediaStreamInfo() {
			// Arrange
			string videoStreamToString = String.Format("Index: {0}; Codec: {1}; Codec long name: {2}; Codec type: {3}; Width: {4}; Height: {5}; Duration: {6} sec;",
				videoStreamIndex,
				videoStreamCodec,
				videoStreamCodecLongName,
				videoStreamType.ToString().ToLower(),
				videoWidth,
				videoHeight,
				videoStreamDuration);

			string audioStreamToString = String.Format("Index: {0}; Codec: {1}; Codec long name: {2}; Codec type: {3}; Channels: {4}; Sample rate: {5}; Duration: {6} sec;",
				audioStreamIndex,
				audioStreamCodec,
				audioStreamCodecLongName,
				audioStreamType.ToString().ToLower(),
				audioChannels,
				audioSampleRate,
				audioStreamDuration);

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromXml(xml);

			// Assert
			Assert.IsNotNull(mediaStreamInfos);
			Assert.AreEqual(2, mediaStreamInfos.Count);
			Assert.IsNotNull(mediaStreamInfos[0]);
			Assert.IsNotNull(mediaStreamInfos[1]);
			Assert.AreEqual(videoStreamToString, mediaStreamInfos[0].ToString());
			Assert.AreEqual(audioStreamToString, mediaStreamInfos[1].ToString());
		}

		[Test]
		public void Can_Read_Values_From_MediaStreamInfo_Object() {
			// Arrange
			string videoStreamToString = String.Format("Index: {0}; Codec: {1}; Codec long name: {2}; Codec type: {3}; Width: {4}; Height: {5}; Duration: {6} sec;",
				videoStreamIndex,
				videoStreamCodec,
				videoStreamCodecLongName,
				videoStreamType.ToString().ToLower(),
				videoWidth,
				videoHeight,
				videoStreamDuration);

			string audioStreamToString = String.Format("Index: {0}; Codec: {1}; Codec long name: {2}; Codec type: {3}; Channels: {4}; Sample rate: {5}; Duration: {6} sec;",
				audioStreamIndex,
				audioStreamCodec,
				audioStreamCodecLongName,
				audioStreamType.ToString().ToLower(),
				audioChannels,
				audioSampleRate,
				audioStreamDuration);

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromXml(xml);

			// Assert
			Assert.IsNotNull(mediaStreamInfos);
			Assert.AreEqual(2, mediaStreamInfos.Count);

			Assert.IsNotNull(mediaStreamInfos[0]);
			Assert.AreEqual(videoStreamIndex, mediaStreamInfos[0].Index);
			Assert.AreEqual(videoStreamCodec, mediaStreamInfos[0].Codec);
			Assert.AreEqual(videoStreamCodecLongName, mediaStreamInfos[0].CodecLongName);
			Assert.AreEqual(videoStreamType, mediaStreamInfos[0].Type);
			Assert.AreEqual(videoWidth, mediaStreamInfos[0].VideoWidth);
			Assert.AreEqual(videoHeight, mediaStreamInfos[0].VideoHeight);
			Assert.AreEqual(videoStreamDuration, mediaStreamInfos[0].Duration);
			Assert.AreEqual(videoStreamToString, mediaStreamInfos[0].ToString());

			Assert.IsNotNull(mediaStreamInfos[1]);
			Assert.AreEqual(audioStreamIndex, mediaStreamInfos[1].Index);
			Assert.AreEqual(audioStreamCodec, mediaStreamInfos[1].Codec);
			Assert.AreEqual(audioStreamCodecLongName, mediaStreamInfos[1].CodecLongName);
			Assert.AreEqual(audioStreamType, mediaStreamInfos[1].Type);
			Assert.AreEqual(audioChannels, mediaStreamInfos[1].AudioChannels);
			Assert.AreEqual(audioSampleRate, mediaStreamInfos[1].AudioSampleRate);
			Assert.AreEqual(audioStreamDuration, mediaStreamInfos[1].Duration);
			Assert.AreEqual(audioStreamToString, mediaStreamInfos[1].ToString());
		}

		[Test]
		public void Can_Compare_Two_MediStreamInfo_Objects() {
			// Arrange
			IList<MediaStreamInfo> mediaStreamInfos01 = MediaStreamInfoFactory.CreateFromXml(xml);
			var videoStreamInfo01 = mediaStreamInfos01[0];
			var audioStreamInfo01 = mediaStreamInfos01[1];

			IList<MediaStreamInfo> mediaStreamInfos02 = MediaStreamInfoFactory.CreateFromXml(xml);
			var videoStreamInfo02 = mediaStreamInfos02[0];
			var audioStreamInfo02 = mediaStreamInfos02[1];

			// Act

			// Assert

			// reflexive: for any non-null reference value x, x.Equals(x) must return true
			Assert.IsTrue(videoStreamInfo01.Equals(videoStreamInfo01));
			Assert.IsTrue(audioStreamInfo01.Equals(audioStreamInfo01));

			Assert.IsTrue(videoStreamInfo02.Equals(videoStreamInfo02));
			Assert.IsTrue(audioStreamInfo02.Equals(audioStreamInfo02));

			// symetric: for any non-null reference values x and y, x.Equals(y) must return true if
			// and only if y.Equasls(x) returns true
			Assert.IsTrue(videoStreamInfo01.Equals(videoStreamInfo02));
			Assert.IsTrue(videoStreamInfo02.Equals(videoStreamInfo01));

			Assert.IsTrue(audioStreamInfo01.Equals(audioStreamInfo02));
			Assert.IsTrue(audioStreamInfo02.Equals(audioStreamInfo01));

			// transitive: for any non-null reference values x, y, z, if x.Equals(y) returns true and
			// y.Equals(z) returns true, then x.Equals(z) must return true

			// for any non-null reference value x, x.Equals(null) must return false
			Assert.IsFalse(videoStreamInfo01.Equals(null));
			Assert.IsFalse(audioStreamInfo01.Equals(null));

			Assert.IsFalse(videoStreamInfo02.Equals(null));
			Assert.IsFalse(audioStreamInfo02.Equals(null));

			// two equals objects must return the same hash code
			Assert.AreEqual(videoStreamInfo01.GetHashCode(), videoStreamInfo02.GetHashCode());
			Assert.AreEqual(audioStreamInfo01.GetHashCode(), audioStreamInfo02.GetHashCode());

		}

	}
}
