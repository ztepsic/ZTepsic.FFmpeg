using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class ResInfoTests {

		[Test]
		public void Can_Create_ResInfo_From_Output_Text() {
			// Arrange
			string output = null;
			using(var file = new StreamReader(Environment.CurrentDirectory + @"\Outputs\Output01.txt")) {
				output = file.ReadToEnd();
			}

			// Act
			var resInfo = ResInfo.Create(output);

			// Assert
			Assert.IsNotNull(resInfo);
			Assert.IsNullOrEmpty(resInfo.Author);
			Assert.IsNullOrEmpty(resInfo.Copyright);
			Assert.IsNullOrEmpty(resInfo.Keywords);
			Assert.IsNullOrEmpty(resInfo.Rating);
			Assert.IsNullOrEmpty(resInfo.Title);
			Assert.AreEqual("Custom", resInfo.PresetName);
			Assert.AreEqual("Fri Feb 24 16:26:31 2012", resInfo.CreationDate);
			Assert.AreEqual("TerraTec Grabby Analog Capture", resInfo.VideoDevice);
			Assert.AreEqual(25, resInfo.Framerate);
			Assert.AreEqual(480, resInfo.Width);
			Assert.AreEqual(360, resInfo.Height);
			Assert.AreEqual("avc1", resInfo.VideoCodecId);
			Assert.AreEqual(360, resInfo.Height);
			Assert.AreEqual(240, resInfo.VideoDataRate);
			Assert.AreEqual(31, resInfo.AvcLevel);
			Assert.AreEqual(66, resInfo.AvcProfile);
			Assert.AreEqual(44100, resInfo.AudioSampleRate);
			Assert.AreEqual(1, resInfo.AudioChannels);
			Assert.AreEqual(75, resInfo.AudioInputVolume);
			Assert.AreEqual(56, resInfo.AudioDataRate);
			Assert.AreEqual("Linea (TerraTec Grabby)", resInfo.AudioDevice);
			Assert.AreEqual(".mp3", resInfo.AudioCodecId);
			Assert.AreEqual(296, resInfo.Bitrate);
  
		}

		[Test]
		public void Can_Detect_Error_When_Stream_Not_Found_From_Output_Text() {
			// Arrange
			string output = null;
			using (var file = new StreamReader(Environment.CurrentDirectory + @"\Outputs\Output02.txt")) {
				output = file.ReadToEnd();
			}

			// Act
			var resInfo = ResInfo.Create(output);

			// Assert
			Assert.IsTrue(resInfo.Errors.Count > 0);
			Assert.AreEqual(resInfo.Errors[0], "Closing connection: NetStream.Play.StreamNotFound");
		}

		[Test]
		public void Can_Detect_Error_Dns_From_Output_Text() {
			// Arrange
			string output = null;
			using (var file = new StreamReader(Environment.CurrentDirectory + @"\Outputs\Output03.txt")) {
				output = file.ReadToEnd();
			}

			// Act
			var resInfo = ResInfo.Create(output);

			// Assert
			Assert.IsTrue(resInfo.Errors.Count > 0);
			Assert.AreEqual(resInfo.Errors[0], "Problem accessing the DNS.");
			
		}

		[Test]
		public void Can_Detect_Error_No_Such_File_Or_Directory_From_Output_Text() {
			// Arrange
			string output = null;
			using (var file = new StreamReader(Environment.CurrentDirectory + @"\Outputs\Output04.txt")) {
				output = file.ReadToEnd();
			}

			// Act
			var resInfo = ResInfo.Create(output);

			// Assert
			Assert.IsTrue(resInfo.Errors.Count > 0);
			Assert.AreEqual(resInfo.Errors[0], "No such file or directory.");
			
		}

		[Test]
		public void Can_Detect_EmptyOrNull_String_From_Output_Text() {
			// Arrange
			string output = null;

			// Act
			var resInfo = ResInfo.Create(output);

			// Assert
			Assert.IsTrue(resInfo.Errors.Count > 0);
			Assert.AreEqual(resInfo.Errors[0], "Not FFmpeg output text.");
		}


		[Test]
		public void Can_Detect_No_Related_Output_Text_To_FFmpeg() {
			// Arrange
			string output = "test testing no related output\n text";

			// Act
			var resInfo = ResInfo.Create(output);

			// Assert
			Assert.IsTrue(resInfo.Errors.Count > 0);
			Assert.AreEqual(resInfo.Errors[0], "Not FFmpeg output text.");
		}
		

	}
}
