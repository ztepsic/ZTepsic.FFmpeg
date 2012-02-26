using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class ResInfoFFmpegCmdTests {

		[Test]
		public void Can_Create_InfoFileFFmpegCmd_And_Is_FFmpegCommand() {
			// Arrange
			const string resouceUriReference = "rtmp://184.173.181.2:554/tvsei/tvsei";

			// Act
			ResInfoFFmpegCmd resInfoFFmpegCmd = new ResInfoFFmpegCmd(resouceUriReference);

			// Assert
			Assert.IsNotNull(resInfoFFmpegCmd);
			Assert.IsInstanceOf<FFmpegCommand>(resInfoFFmpegCmd);
		}

		[Test]
		public void Can_Execute_Command() {
			// Arrange
			const string resouceUriReference = "rtmp://184.173.181.2:554/tvsei/tvsei";
			ResInfoFFmpegCmd resInfoFFmpegCmd = new ResInfoFFmpegCmd(resouceUriReference);
			ResInfo resInfo = null;
			resInfoFFmpegCmd.Notify += info => resInfo = info;

			// Act
			resInfoFFmpegCmd.Execute();

			// Assert
			Assert.IsNotNull(resInfo);
			Assert.IsNotNull(resInfo.Bitrate);

		}

		[Ignore]
		[Test]
		public void If_Error_Exists_Command_Notifies_With_ResInfo_As_Null() {
			// Arrange

			// Act

			// Assert
		}

	}
}
