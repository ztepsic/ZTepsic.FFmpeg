using System;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class MediaFormatInfoFactoryTests {

		private string XML_DIR = Environment.CurrentDirectory +  @"\Outputs\";

		[Test]
		public void Can_Create_MediaFormatInfo_From_Xml_File_Flv() {
			// Arrange

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromFile(XML_DIR + "flv_01.xml");

			// Assert
			Assert.NotNull(mediaFormatInfo);
			Assert.AreEqual("http://flash.example.com/ex/all/ex-h264.flv", mediaFormatInfo.FileName);
			Assert.AreEqual("flv", mediaFormatInfo.Format);
			Assert.AreEqual("FLV format", mediaFormatInfo.FormatLongName);
			Assert.AreEqual(0, mediaFormatInfo.Bitrate);
			Assert.AreEqual(200904.120000, mediaFormatInfo.StartTime);
			Assert.AreEqual(0, mediaFormatInfo.Duration);
			Assert.AreEqual(0, mediaFormatInfo.FileSize);
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
		public void MediaFormatInfo_Is_Null_If_Format_Element_Not_Exist_In_Xml_File_Mmsh_02() {
			// Arrange

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromFile(XML_DIR + "mmsh_02_not.xml");

			// Assert
			Assert.IsNull(mediaFormatInfo);

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
		public void MediaFormatInfo_Is_Null_If_Format_Element_Not_Exist_In_Xml_File_Rtmp_01() {
			// Arrange

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromFile(XML_DIR + "rtmp_01_not.xml");

			// Assert
			Assert.IsNull(mediaFormatInfo);

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
