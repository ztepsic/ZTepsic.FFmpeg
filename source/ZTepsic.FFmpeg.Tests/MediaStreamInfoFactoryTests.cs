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
		public void Can_Create_MediaStreamInfos_From_Xml_File_Flv() {
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
			Assert.AreEqual("25/1", videoStreamInfo.FrameRateStr);
			Assert.AreEqual(25, videoStreamInfo.FrameRate);
			Assert.AreEqual("0/0", videoStreamInfo.AvgFrameRateStr);
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
			Assert.AreEqual("0/0", audioStreamInfo.FrameRateStr);
			Assert.AreEqual(0, audioStreamInfo.FrameRate);
			Assert.AreEqual("1000/23", audioStreamInfo.AvgFrameRateStr);
			Assert.AreEqual(43.478260869565217391304347826087, audioStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(200904.120000, audioStreamInfo.StartTime);

		}

		[Test]
		public void Can_Create_MediaStreamInfos_From_Xml_File_Mmsh_01() {
			// Arrange

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromFile(XML_DIR + "mmsh_01.xml");

			// Assert
			Assert.NotNull(mediaStreamInfos);
			Assert.AreEqual(2, mediaStreamInfos.Count);

			var videoStreamInfo = mediaStreamInfos[1];
			Assert.AreEqual(1, videoStreamInfo.Index);
			Assert.AreEqual("wmv3", videoStreamInfo.Codec);
			Assert.AreEqual("Windows Media Video 9", videoStreamInfo.CodecLongName);
			Assert.AreEqual("video", videoStreamInfo.Type.ToString().ToLower());
			// codec_time_base="1/1000"
			// codec_tag_string="WMV3"
			// codec_tag="0x33564d57
			Assert.AreEqual(720, videoStreamInfo.VideoWidth);
			Assert.AreEqual(576, videoStreamInfo.VideoHeight);
			// has_b_frames="0"
			Assert.AreEqual(null, videoStreamInfo.VideoSampleAspectRatioStr);
			Assert.AreEqual(0, videoStreamInfo.VideoSampleAspectRatio);
			Assert.AreEqual(null, videoStreamInfo.VideoDisplayAspectRatioStr);
			Assert.AreEqual(0, videoStreamInfo.VideoDisplayAspectRatio);
			// pix_fmt="yuv420p"
			// level="-99"
			Assert.AreEqual(25, videoStreamInfo.FrameRate);
			Assert.AreEqual(0, videoStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(863916.568000, videoStreamInfo.StartTime);
			Assert.AreEqual(410000, videoStreamInfo.BitRate);

			var audioStreamInfo = mediaStreamInfos[0];
			Assert.AreEqual(0, audioStreamInfo.Index);
			Assert.AreEqual("wmav2", audioStreamInfo.Codec);
			Assert.AreEqual("Windows Media Audio 2", audioStreamInfo.CodecLongName);
			Assert.AreEqual("audio", audioStreamInfo.Type.ToString().ToLower());
			// codec_time_base="1/44100"
			// codec_tag_string="a[1][0][0]"
			// codec_tag="0x0161"
			// sample_fmt="s16"
			Assert.AreEqual(44100, audioStreamInfo.AudioSampleRate);
			Assert.AreEqual(2, audioStreamInfo.AudioChannels);
			// bits_per_sample="0"
			Assert.AreEqual("0/0", audioStreamInfo.FrameRateStr);
			Assert.AreEqual(0, audioStreamInfo.FrameRate);
			Assert.AreEqual("200/37", audioStreamInfo.AvgFrameRateStr);
			Assert.AreEqual(5.4054054054054054054054054054054m, audioStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(863916.616000, audioStreamInfo.StartTime);
			Assert.AreEqual(31960, audioStreamInfo.BitRate);

		}

		[Test]
		public void MediaStreamInfo_List_Is_Empty_If_Streams_Element_Not_Exist_In_Xml_File_Mmsh_02() {
			// Arrange

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromFile(XML_DIR + "mmsh_02_not.xml");

			// Assert
			Assert.NotNull(mediaStreamInfos);
			Assert.IsEmpty(mediaStreamInfos);

		}

		[Test]
		public void Can_Create_MediaFormatInfo_From_Xml_File_Mmst_01() {
			// Arrange

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromFile(XML_DIR + "mmst_01.xml");

			// Assert
			Assert.NotNull(mediaStreamInfos);
			Assert.AreEqual(2, mediaStreamInfos.Count);

			var videoStreamInfo = mediaStreamInfos[1];
			Assert.AreEqual(1, videoStreamInfo.Index);
			Assert.AreEqual("wmv3", videoStreamInfo.Codec);
			Assert.AreEqual("Windows Media Video 9", videoStreamInfo.CodecLongName);
			Assert.AreEqual("video", videoStreamInfo.Type.ToString().ToLower());
			// codec_time_base="1/1000"
			// codec_tag_string="WMV3"
			// codec_tag="0x33564d57"
			Assert.AreEqual(320, videoStreamInfo.VideoWidth);
			Assert.AreEqual(240, videoStreamInfo.VideoHeight);
			// has_b_frames="0"
			Assert.AreEqual(null, videoStreamInfo.VideoSampleAspectRatioStr);
			Assert.AreEqual(0m, videoStreamInfo.VideoSampleAspectRatio);
			Assert.AreEqual(null, videoStreamInfo.VideoDisplayAspectRatioStr);
			Assert.AreEqual(0m, videoStreamInfo.VideoDisplayAspectRatio);
			// pix_fmt="yuv420p"
			// level="-99"
			// is_avc="1"
			// nal_length_size="4"
			Assert.AreEqual("25/1", videoStreamInfo.FrameRateStr);
			Assert.AreEqual(25, videoStreamInfo.FrameRate);
			Assert.AreEqual("0/0", videoStreamInfo.AvgFrameRateStr);
			Assert.AreEqual(0, videoStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(144568.163000, videoStreamInfo.StartTime);
			Assert.AreEqual(241000, videoStreamInfo.BitRate);

			var audioStreamInfo = mediaStreamInfos[0];
			Assert.AreEqual(0, audioStreamInfo.Index);
			Assert.AreEqual("wmav2", audioStreamInfo.Codec);
			Assert.AreEqual("Windows Media Audio 2", audioStreamInfo.CodecLongName);
			Assert.AreEqual("audio", audioStreamInfo.Type.ToString().ToLower());
			// ccodec_time_base="1/32000"
			// codec_tag_string="a[1][0][0]"
			// codec_tag="0x0161"
			// sample_fmt="s16"
			Assert.AreEqual(32000, audioStreamInfo.AudioSampleRate);
			Assert.AreEqual(2, audioStreamInfo.AudioChannels);
			// bits_per_sample="0"
			Assert.AreEqual("0/0", audioStreamInfo.FrameRateStr);
			Assert.AreEqual(0, audioStreamInfo.FrameRate);
			Assert.AreEqual("125/24", audioStreamInfo.AvgFrameRateStr);
			Assert.AreEqual(5.2083333333333333333333333333333m, audioStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(144567.815000, audioStreamInfo.StartTime);
			Assert.AreEqual(32000, audioStreamInfo.BitRate);

		}

		[Test]
		public void MediaStreamInfo_List_Is_Empty_If_Streams_Element_Not_Exist_In_Xml_File_Rtmp_01() {
			// Arrange

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromFile(XML_DIR + "rtmp_01_not.xml");

			// Assert
			Assert.NotNull(mediaStreamInfos);
			Assert.IsEmpty(mediaStreamInfos);

		}

		[Test]
		public void Can_Create_MediaFormatInfo_From_Xml_File_Rtmp_02() {
			// Arrange

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromFile(XML_DIR + "rtmp_02.xml");

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
			Assert.AreEqual(512, videoStreamInfo.VideoWidth);
			Assert.AreEqual(288, videoStreamInfo.VideoHeight);
			// has_b_frames="1"
			Assert.AreEqual("1:1", videoStreamInfo.VideoSampleAspectRatioStr);
			Assert.AreEqual(1m, videoStreamInfo.VideoSampleAspectRatio);
			Assert.AreEqual("16:9", videoStreamInfo.VideoDisplayAspectRatioStr);
			Assert.AreEqual(1.7777777777777777777777777777778m, videoStreamInfo.VideoDisplayAspectRatio);
			// pix_fmt="yuv420p"
			// level="51"
			// is_avc="1"
			// nal_length_size="4"
			Assert.AreEqual("50/2", videoStreamInfo.FrameRateStr);
			Assert.AreEqual(25, videoStreamInfo.FrameRate);
			Assert.AreEqual("0/0", videoStreamInfo.AvgFrameRateStr);
			Assert.AreEqual(0, videoStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(0.040000, videoStreamInfo.StartTime);

			var audioStreamInfo = mediaStreamInfos[1];
			Assert.AreEqual(1, audioStreamInfo.Index);
			Assert.AreEqual("aac", audioStreamInfo.Codec);
			Assert.AreEqual("Advanced Audio Coding", audioStreamInfo.CodecLongName);
			Assert.AreEqual("audio", audioStreamInfo.Type.ToString().ToLower());
			// codec_time_base="1/24000"
			// codec_tag_string="[0][0][0][0]"
			// codec_tag="0x0000"
			// sample_fmt="s16"
			Assert.AreEqual(48000, audioStreamInfo.AudioSampleRate);
			Assert.AreEqual(1, audioStreamInfo.AudioChannels);
			// bits_per_sample="0"
			Assert.AreEqual("0/0", audioStreamInfo.FrameRateStr);
			Assert.AreEqual(0, audioStreamInfo.FrameRate);
			Assert.AreEqual("500/21", audioStreamInfo.AvgFrameRateStr);
			Assert.AreEqual(23.809523809523809523809523809524m, audioStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(0.035000, audioStreamInfo.StartTime);
		}

		[Test]
		public void Can_Create_MediaFormatInfo_From_Xml_File_Rtmp_03() {
			// Arrange

			// Act
			IList<MediaStreamInfo> mediaStreamInfos = MediaStreamInfoFactory.CreateFromFile(XML_DIR + "rtmp_03.xml");

			// Assert
			Assert.NotNull(mediaStreamInfos);
			Assert.AreEqual(2, mediaStreamInfos.Count);

			var videoStreamInfo = mediaStreamInfos[0];
			Assert.AreEqual(0, videoStreamInfo.Index);
			Assert.AreEqual("vp6f", videoStreamInfo.Codec);
			Assert.AreEqual("On2 VP6 (Flash version)", videoStreamInfo.CodecLongName);
			Assert.AreEqual("video", videoStreamInfo.Type.ToString().ToLower());
			// codec_time_base="1/1000"
			// codec_tag_string="[0][0][0][0]"
			// codec_tag="0x0000"
			Assert.AreEqual(800, videoStreamInfo.VideoWidth);
			Assert.AreEqual(464, videoStreamInfo.VideoHeight);
			// has_b_frames="0"
			Assert.AreEqual(null, videoStreamInfo.VideoSampleAspectRatioStr);
			Assert.AreEqual(0, videoStreamInfo.VideoSampleAspectRatio);
			Assert.AreEqual(null, videoStreamInfo.VideoDisplayAspectRatioStr);
			Assert.AreEqual(0, videoStreamInfo.VideoDisplayAspectRatio);
			// pix_fmt="yuv420p"
			// level="-99"
			Assert.AreEqual("25/1", videoStreamInfo.FrameRateStr);
			Assert.AreEqual(25, videoStreamInfo.FrameRate);
			Assert.AreEqual("0/0", videoStreamInfo.AvgFrameRateStr);
			Assert.AreEqual(0, videoStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(0, videoStreamInfo.StartTime);

			var audioStreamInfo = mediaStreamInfos[1];
			Assert.AreEqual(1, audioStreamInfo.Index);
			Assert.AreEqual("mp3", audioStreamInfo.Codec);
			Assert.AreEqual("MP3 (MPEG audio layer 3)", audioStreamInfo.CodecLongName);
			Assert.AreEqual("audio", audioStreamInfo.Type.ToString().ToLower());
			// codec_time_base="1/22050"
			// codec_tag_string="[0][0][0][0]"
			// codec_tag="0x0000"
			// sample_fmt="s16"
			Assert.AreEqual(22050, audioStreamInfo.AudioSampleRate);
			Assert.AreEqual(2, audioStreamInfo.AudioChannels);
			// bits_per_sample="0"
			Assert.AreEqual("0/0", audioStreamInfo.FrameRateStr);
			Assert.AreEqual(0, audioStreamInfo.FrameRate);
			Assert.AreEqual("500/13", audioStreamInfo.AvgFrameRateStr);
			Assert.AreEqual(38.461538461538461538461538461538m, audioStreamInfo.AvgFrameRate);
			// time_base="1/1000"
			Assert.AreEqual(0, audioStreamInfo.StartTime);
			Assert.AreEqual(64000, audioStreamInfo.BitRate);

		}

	}
}
