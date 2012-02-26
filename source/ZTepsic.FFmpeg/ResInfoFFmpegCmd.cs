using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// FFmpeg command which gets information about a video resource.
	/// Resource can be a file on file system or a stream.
	/// </summary>
	public class ResInfoFFmpegCmd : FFmpegCommand {

		#region Members

		/// <summary>
		/// URI reference of the video resource for which we are asking information.
		/// A URI reference may take the form of a full URI, or just the scheme-specific portion of one, or even some trailing component.
		/// </summary>
		private readonly string resourceUriReference;

		/// <summary>
		/// Callback
		/// </summary>
		/// <param name="resInfo"></param>
		public delegate void Callback(ResInfo resInfo);

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
		public ResInfoFFmpegCmd(string resourceUriReference) {
			this.resourceUriReference = resourceUriReference;
			parameters = String.Format("-i {0}", resourceUriReference);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Process result of the execution of the FFmpeg application
		/// </summary>
		/// <param name="output">Output result of the execution of the FFmpeg application</param>
		/// <param name="error">Error result of the execution of the FFmpeg application</param>
		protected override void processResult(string output, string error) {
			ResInfo resInfo = null;
			if(string.IsNullOrEmpty(error)) {
				resInfo = ResInfo.Create(output);
			}

			Notify(resInfo);

		}

		#endregion

	}
}
