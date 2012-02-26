using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class FFmpegCommandTests {

		[Test]
		public void Can_Create_FFmpegCmd() {
			// Arrange
			var ffmpegCmdMock = new Mock<FFmpegCommand>();

			// Act
			FFmpegCommand ffmpegCmd = ffmpegCmdMock.Object;

			// Assert
			Assert.IsNotNull(ffmpegCmd);
			Assert.AreEqual(FFmpegCommand.FFMPEG_FILE, ffmpegCmd.FFmpegExePath);
		}


		[Test]
		public void Can_Create_FFmpegCmd_With_FFmpegExePath_Parameter() {
			// Arrange
			const string ffmpegExePath = "ffmpeg.exe";
			var ffmpegCmdMock = new Mock<FFmpegCommand>(ffmpegExePath);

			// Act
			FFmpegCommand ffmpegCmd = ffmpegCmdMock.Object;

			// Assert
			Assert.IsNotNull(ffmpegCmd);
			Assert.AreEqual(ffmpegExePath, ffmpegCmd.FFmpegExePath);

		}

		[Test]
		public void Can_Execute_FFmpegComd() {
			// Arrange
			var ffmpegCmdMock = new Mock<FFmpegCommand>();
			ffmpegCmdMock.Protected().Setup("manipulateWithProcess", ItExpr.IsAny<Process>());

			string output = null;
			string error = null;
			ffmpegCmdMock.Protected()
				.Setup("processResult", ItExpr.IsAny<string>(), ItExpr.IsAny<string>())
				.Callback((string o, string e) => {
				          	output = o;
							error = e;
				          });

			FFmpegCommand ffmpegCmd = ffmpegCmdMock.Object;

			// Act
			ffmpegCmd.Execute();

			// Assert
			ffmpegCmdMock.Protected().Verify("manipulateWithProcess", Times.Once(), ItExpr.IsAny<Process>());

			ffmpegCmdMock.Protected().Verify("processResult", Times.Once(), ItExpr.IsAny<string>(), ItExpr.IsAny<string>());
			Assert.IsNotNull(output);
			Assert.IsTrue(output.Contains("built on"));
			Console.WriteLine("Output: ");
			Console.Write(Enumerable.Repeat("-", 20).Aggregate((sum, element) => sum + element) + Environment.NewLine);
			Console.WriteLine(output);

			Assert.IsNotNull(error);
			Console.WriteLine("Error: ");
			Console.Write(Enumerable.Repeat("-", 20).Aggregate((sum, element) => sum + element) + Environment.NewLine);
			Console.WriteLine(error);
		}

		[Test]
		public void If_FFmpeg_Exe_File_Not_Exist_In_The_Given_Location_Try_In_Working_Dir() {
			// Arrange
			var ffmpegCmdMock = new Mock<FFmpegCommand>("fdsa");
			FFmpegCommand ffmpegCmd = ffmpegCmdMock.Object;

			// Act
			ffmpegCmd.Execute();

			// Assert
			Assert.AreEqual(FFmpegCommand.FFMPEG_FILE, ffmpegCmd.FFmpegExePath);
		}

		[Ignore]
		[Test]
		public void If_FFmpeg_Exe_File_Can_Not_Be_Found_In_Both_Locations_Throw_Exception() {
			// Arrange
			var ffmpegmdMock = new Mock<FFmpegCommand>("fdsa");
			FFmpegCommand ffmpegCmd = ffmpegmdMock.Object;

			// Act

			// Assert
			Assert.Throws<FileNotFoundException>(ffmpegCmd.Execute);
		}


	}
}
