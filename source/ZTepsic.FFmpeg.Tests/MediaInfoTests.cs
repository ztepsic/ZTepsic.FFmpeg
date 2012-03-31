using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class MediaInfoTests {

		private string xml;

		private string fileName = "filename.flv";
		private string format = "flv";
		private string formatLongName = "FLV format";
		private decimal bitRate = 3000;
		private decimal duration = 564.23m;
		private decimal startTime = 0.12m;
		private decimal fileSize = 20;

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

			var formatElem = new XElement(MediaFormatInfoFactory.FORMAT_NODE);
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.FILE_NAME, fileName));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.FORMAT, format));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.FORMAT_LONG_NAME, formatLongName));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.BIT_RATE, bitRate.ToString(CultureInfo.InvariantCulture)));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.DURATION, duration.ToString(CultureInfo.InvariantCulture)));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.START_TIME, startTime.ToString(CultureInfo.InvariantCulture)));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.FILE_SIZE, fileSize.ToString(CultureInfo.InvariantCulture)));

			rootElem.Add(formatElem);

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

			rootElem.Add(streamsElem);
			xDoc.Add(rootElem);

			xml = xDoc.ToString();

		}


		[Test]
		public void Can_Create_MediaInfo() {
			// Arrange
			
			// Act
			MediaInfo mediaInfo = MediaInfoFactory.CreateFromXml(xml);
			MediaFormatInfo mediaFormatInfo = mediaInfo.Format;
			IList<MediaStreamInfo> mediaStreamInfos = mediaInfo.Streams;

			// Assert
			Assert.IsNotNull(mediaFormatInfo);
			Assert.AreEqual(fileName, mediaFormatInfo.FileName);
			Assert.AreEqual(format, mediaFormatInfo.Format);
			Assert.AreEqual(formatLongName, mediaFormatInfo.FormatLongName);
			Assert.AreEqual(bitRate, mediaFormatInfo.Bitrate);
			Assert.AreEqual(duration, mediaFormatInfo.Duration);
			Assert.AreEqual(startTime, mediaFormatInfo.StartTime);
			Assert.AreEqual(fileSize, mediaFormatInfo.FileSize);

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

			Assert.IsNotNull(mediaStreamInfos[1]);
			Assert.AreEqual(audioStreamIndex, mediaStreamInfos[1].Index);
			Assert.AreEqual(audioStreamCodec, mediaStreamInfos[1].Codec);
			Assert.AreEqual(audioStreamCodecLongName, mediaStreamInfos[1].CodecLongName);
			Assert.AreEqual(audioStreamType, mediaStreamInfos[1].Type);
			Assert.AreEqual(audioChannels, mediaStreamInfos[1].AudioChannels);
			Assert.AreEqual(audioSampleRate, mediaStreamInfos[1].AudioSampleRate);
			Assert.AreEqual(audioStreamDuration, mediaStreamInfos[1].Duration);
		}

	}
}
