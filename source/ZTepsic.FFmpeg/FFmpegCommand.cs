using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// FFmpeg is an adapter class to FFmpeg application
	/// </summary>
	public abstract class FFmpegCommand : ICommand {

		#region Members

		/// <summary>
		/// Default file name of the executable ffmpeg file.
		/// </summary>
		public const string FFMPEG_FILE = "ffmpeg.exe";

		/// <summary>
		/// File path of the executable ffmpeg file (ffmpeg.exe).
		/// </summary>
		private string ffmpegExePath;

		/// <summary>
		/// Gets the file path of the executable ffmpeg file (ffmpeg.exe)
		/// </summary>
		public string FFmpegExePath { get { return ffmpegExePath; } }

		/// <summary>
		/// FFmpeg parameters
		/// </summary>
		protected string parameters;

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Constructs FFmpeg with ffmpegExePath as ffmpeg.exe and workingDirPath in the current directory
		/// </summary>
		protected FFmpegCommand() : this(FFMPEG_FILE) { }


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ffmpegExePath">file path of the executable ffmpeg file</param>
		protected FFmpegCommand(string ffmpegExePath) {
			this.ffmpegExePath = ffmpegExePath;
		}

		#endregion

		#region Methods



		/// <summary>
		/// Execute command
		/// </summary>
		public void Execute() {
			if(!File.Exists(ffmpegExePath)) {
				if(File.Exists(FFMPEG_FILE)) {
					ffmpegExePath = FFMPEG_FILE;
				} else {
					throw new FileNotFoundException(String.Format("Could not find the executable ffmpeg application in: {0}.", ffmpegExePath));	
				}
			}


			ProcessStartInfo processStartInfo = new ProcessStartInfo(ffmpegExePath, parameters) {
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true
			};

			string output;
			string error;
			Process proc = null;
			using(proc = Process.Start(processStartInfo)) {
				manipulateWithProcess(proc);

				proc.WaitForExit();

				output = proc.StandardError.ReadToEnd();
				error = proc.StandardError.ReadToEnd();
			}

			processResult(output, error);
		}
		
		
		/// <summary>
		/// Process result of the execution of the FFmpeg application
		/// </summary>
		/// <param name="output">Output result of the execution of the FFmpeg application</param>
		/// <param name="error">Error result of the execution of the FFmpeg application</param>
		protected abstract void processResult(String output, String error);

		/// <summary>
		/// Method provides hook to manipulate with running FFmpeg process
		/// </summary>
		/// <param name="proc">FFmpeg running process</param>
		protected virtual void manipulateWithProcess(Process proc) { }

		#endregion
	}
}
