using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ZTepsic.FFmpeg.Exceptions;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class MediaInfoFFmpegCmdTests {

		[Test]
		public void Can_Create_MediaInfoFFmpegCmd_And_Is_FFmpegCommand() {
			// Arrange
			const string resouceUriReference = "rtmp://184.173.181.2:554/tvsei/tvsei";

			// Act
			MediaInfoFFmpegCmd mediaInfoFFmpegCmd = new MediaInfoFFmpegCmd(resouceUriReference);

			// Assert
			Assert.IsNotNull(mediaInfoFFmpegCmd);
			Assert.IsInstanceOf<FFmpegCommand>(mediaInfoFFmpegCmd);
		}

		[Test]
		public void Can_Execute_Command() {
			// Arrange
			const string resouceUriReference = "rtmp://85.25.122.231/streamHD/video/stream live=1";
			MediaInfoFFmpegCmd resInfoFFmpegCmd = new MediaInfoFFmpegCmd(resouceUriReference);
			MediaInfo mediaInfo = null;
			resInfoFFmpegCmd.Notify += info => mediaInfo = info;

			// Act
			resInfoFFmpegCmd.Execute();

			// Assert
			Assert.IsNotNull(mediaInfo);
			Assert.IsNotNull(mediaInfo.Format.Bitrate);

		}

		[Test]
		[ExpectedException(typeof(OperationNotPermittedException))]
		public void If_Error_Exists_Command_Notifies_With_FFmpegException() {
			// Arrange
			const string resouceUriReference = "rtmp://error";
			MediaInfoFFmpegCmd resInfoFFmpegCmd = new MediaInfoFFmpegCmd(resouceUriReference);
			MediaInfo mediaInfo = null;
			resInfoFFmpegCmd.Notify += info => mediaInfo = info;

			// Act
			resInfoFFmpegCmd.Execute();

			// Assert
		}

	}
}
