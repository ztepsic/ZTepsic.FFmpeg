using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// FFmpeg command which gets information about a video resource.
	/// Resource can be a file on file system or a stream.
	/// </summary>
	public class MediaInfoFFmpegCmd : FFmpegCommand {

		#region Members

		/// <summary>
		/// Wait for exit time constant in miliseconds
		/// </summary>
		public const int WAIT_FOR_EXIT_TIME = 120000;

		/// <summary>
		/// URI reference of the video resource for which we are asking information.
		/// A URI reference may take the form of a full URI, or just the scheme-specific portion of one, or even some trailing component.
		/// </summary>
		private readonly string resourceUriReference;

		/// <summary>
		/// Callback
		/// </summary>
		/// <param name="mediaInfo"></param>
		public delegate void Callback(MediaInfo mediaInfo);

		/// <summary>
		/// Callback notify event
		/// </summary>
		public event Callback Notify;

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="resourceUriReference">URI reference of the video resource for wich we are aksing information.
		/// A URI reference may take the form of a full URI, or just the scheme-specific portion of one, or even some trailing component.
		/// </param>
		public MediaInfoFFmpegCmd(string resourceUriReference) : this(resourceUriReference, WAIT_FOR_EXIT_TIME) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="resourceUriReference">URI reference of the video resource for wich we are aksing information.
		/// A URI reference may take the form of a full URI, or just the scheme-specific portion of one, or even some trailing component.
		/// </param>
		/// <param name="waitForExitTime">Wait for associated process to exit in miliseconds</param>
		public MediaInfoFFmpegCmd(string resourceUriReference, int waitForExitTime) : base(FFmpegApp.FFprobe) {
			this.resourceUriReference = resourceUriReference;
			WaitForExitTime = waitForExitTime;

			Parameters = String.Format("-i \"{0}\" -print_format xml -show_error -show_format -show_streams", resourceUriReference);
			//ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%
		}

		#endregion

		#region Methods

		/// <summary>
		/// Process result of the execution of the FFprobe application
		/// </summary>
		/// <param name="output">Output result of the execution of the FFmpeg application</param>
		/// <param name="error">Error result of the execution of the FFmpeg application</param>
		protected override void processResult(string output, string error) {
			MediaInfo mediaInfo = null;

			// For some reason, the ffmpeg application puts general information and errors in the error output (STDERR)
			// For explicit information, such as -show_error, -show_format and -show_streams, the ffmpeg application puts that in the standard output (STDOUT)

			if(!string.IsNullOrEmpty(output)) {
				mediaInfo = MediaInfoFactory.CreateFromXml(output);
			}

			Notify(mediaInfo);

		}

		#endregion

	}
}
