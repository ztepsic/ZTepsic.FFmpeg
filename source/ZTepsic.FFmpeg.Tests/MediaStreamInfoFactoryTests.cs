using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class MediaStreamInfoFactoryTests {

		private string XML_DIR = Environment.CurrentDirectory + @"\Outputs\";

		[Test]
		public void Can_Create_MediaStreamInfo_From_Xml_File_Flv() {
			// Arrange

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromFile(XML_DIR + "flv_01.xml");

			// Assert
			Assert.NotNull(mediaStreamInfos);
			Assert.AreEqual(2, mediaStreamInfos.Count);

			var videoStreamInfo = mediaStreamInfos[0];
			Assert.AreEqual(0, videoStreamInfo.Index);
			Assert.AreEqual("h264", videoStreamInfo.Codec);
			Assert.AreEqual("H.264 / AVC / MPEG-4 AVC / MPEG-4 part 10", videoStreamInfo.CodecLongName);
			Assert.AreEqual("video", videoStreamInfo.Type.ToString().ToLower());
			// codec_time_base="1/50"
			// codec_tag_string="[0][0][0][0]"
			// codec_tag="0x0000"
			Assert.AreEqual(480, videoStreamInfo.VideoWidth);
			Assert.AreEqual(272, videoStreamInfo.VideoHeight);
			// has_b_frames="2"
			Assert.AreEqual("136:135", videoStreamInfo.VideoSampleAspectRatioStr);
			Assert.AreEqual(1.0074074074074074074074074074074m, videoStreamInfo.VideoSampleAspectRatio);
			Assert.AreEqual("16:9", videoStreamInfo.VideoDisplayAspectRatioStr);
			Assert.AreEqual(1.7777777777777777777777777777778m, videoStreamInfo.VideoDisplayAspectRatio);
			// pix_fmt="yuv420p"
			// level="21"
			// is_avc="1"
			// nal_length_size="4"
			Assert.AreEqual(25, videoStreamInfo.FrameRate);
			Assert.AreEqual(0, videoStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(200904.200000, videoStreamInfo.StartTime);

			var audioStreamInfo = mediaStreamInfos[1];
			Assert.AreEqual(1, audioStreamInfo.Index);
			Assert.AreEqual("aac", audioStreamInfo.Codec);
			Assert.AreEqual("Advanced Audio Coding", audioStreamInfo.CodecLongName);
			Assert.AreEqual("audio", audioStreamInfo.Type.ToString().ToLower());
			// codec_time_base="1/44100"
			// codec_tag_string="[0][0][0][0]"
			// codec_tag="0x0000"
			// sample_fmt="s16"
			Assert.AreEqual(44100, audioStreamInfo.AudioSampleRate);
			Assert.AreEqual(2, audioStreamInfo.AudioChannels);
			// bits_per_sample="0"
			Assert.AreEqual(0, audioStreamInfo.FrameRate);
			Assert.AreEqual(43.478260869565217391304347826087, audioStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(200904.120000, audioStreamInfo.StartTime);

		}

		[Test]
		public void Can_Create_MediaFormatInfo_From_Xml_File_Mmsh_01() {
			// Arrange

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromFile(XML_DIR + "mmsh_01.xml");

			// Assert
			Assert.NotNull(mediaFormatInfo);
			Assert.AreEqual("mmsh://asf.example.com/asf", mediaFormatInfo.FileName);
			Assert.AreEqual("asf", mediaFormatInfo.Format);
			Assert.AreEqual("ASF format", mediaFormatInfo.FormatLongName);
			Assert.AreEqual(441960, mediaFormatInfo.Bitrate);
			Assert.AreEqual(863916.568000, mediaFormatInfo.StartTime);
			Assert.AreEqual(0, mediaFormatInfo.Duration);
			Assert.AreEqual(0, mediaFormatInfo.FileSize);

		}

		[Test]
		public void Can_Create_MediaFormatInfo_From_Xml_File_Mmst_01() {
			// Arrange

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromFile(XML_DIR + "mmst_01.xml");

			// Assert
			Assert.NotNull(mediaFormatInfo);
			Assert.AreEqual("mmst://111.111.111.111/example", mediaFormatInfo.FileName);
			Assert.AreEqual("asf", mediaFormatInfo.Format);
			Assert.AreEqual("ASF format", mediaFormatInfo.FormatLongName);
			Assert.AreEqual(273000, mediaFormatInfo.Bitrate);
			Assert.AreEqual(144567.815000, mediaFormatInfo.StartTime);
			Assert.AreEqual(0, mediaFormatInfo.Duration);
			Assert.AreEqual(0, mediaFormatInfo.FileSize);

		}

		[Test]
		public void Can_Create_MediaFormatInfo_From_Xml_File_Rtmp_02() {
			// Arrange

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromFile(XML_DIR + "rtmp_02.xml");

			// Assert
			Assert.NotNull(mediaFormatInfo);
			Assert.AreEqual("rtmp://111.111.111.111/streamHD/video/stream live=1", mediaFormatInfo.FileName);
			Assert.AreEqual("flv", mediaFormatInfo.Format);
			Assert.AreEqual("FLV format", mediaFormatInfo.FormatLongName);
			Assert.AreEqual(0, mediaFormatInfo.Bitrate);
			Assert.AreEqual(0.035000, mediaFormatInfo.StartTime);
			Assert.AreEqual(0, mediaFormatInfo.Duration);
			Assert.AreEqual(0, mediaFormatInfo.FileSize);

		}

		[Test]
		public void Can_Create_MediaFormatInfo_From_Xml_File_Rtmp_03() {
			// Arrange

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromFile(XML_DIR + "rtmp_03.xml");

			// Assert
			Assert.NotNull(mediaFormatInfo);
			Assert.AreEqual("rtmp://stream.live.example.com/live/Flash_live_TV@27823 live=1", mediaFormatInfo.FileName);
			Assert.AreEqual("flv", mediaFormatInfo.Format);
			Assert.AreEqual("FLV format", mediaFormatInfo.FormatLongName);
			Assert.AreEqual(0, mediaFormatInfo.Bitrate);
			Assert.AreEqual(0, mediaFormatInfo.StartTime);
			Assert.AreEqual(0, mediaFormatInfo.Duration);
			Assert.AreEqual(0, mediaFormatInfo.FileSize);

		}

	}
}
