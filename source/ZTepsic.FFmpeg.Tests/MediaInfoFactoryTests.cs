using System;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class MediaInfoFactoryTests {

		private string XML_DIR = Environment.CurrentDirectory + @"\Outputs\";

		[Test]
		[ExpectedException(typeof(FFmpegException))]
		public void Throw_FFmpegException_If_Error_Element_In_Xml_File_Mmsh_02() {
			// Arrange
			MediaInfo mediaInfo = null;
			FFmpegException fFmpegException = null;

			// Act
			try {
				mediaInfo = MediaInfoFactory.CreateFromFile(XML_DIR + "mmsh_02_not.xml");	
			} catch(FFmpegException ex) {
				fFmpegException = ex;
			}
			

			// Assert
			Assert.AreEqual("Input/output error", fFmpegException.Message);
			Assert.AreEqual(-5, fFmpegException.Code);

			throw fFmpegException;
		}

		[Test]
		[ExpectedException(typeof(FFmpegException))]
		public void Throw_FFmpegException_If_Error_Element_In_Xml_File_Rtmp_01() {
			// Arrange
			MediaInfo mediaInfo = null;
			FFmpegException fFmpegException = null;

			// Act
			try {
				mediaInfo = MediaInfoFactory.CreateFromFile(XML_DIR + "rtmp_01_not.xml");
			} catch (FFmpegException ex) {
				fFmpegException = ex;
			}


			// Assert
			Assert.AreEqual("Operation not permitted", fFmpegException.Message);
			Assert.AreEqual(-1, fFmpegException.Code);

			throw fFmpegException;
		}

	}
}
