using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// FFmpegCommand is an adapter class to FFmpeg applications
	/// </summary>
	public abstract class FFmpegCommand : ICommand {

		#region Members

		/// <summary>
		/// FFmpeg applications
		/// </summary>
		public enum FFmpegApp {
			/// <summary>
			/// FFmpeg application
			/// </summary>
			FFmpeg,

			/// <summary>
			/// FFprobe application
			/// </summary>
			FFprobe
		}

		/// <summary>
		/// Default file names of the excutable ffmpeg application files.
		/// </summary>
		private static Dictionary<FFmpegApp, string> ffMpegApps = new Dictionary<FFmpegApp, string>() {
			{FFmpegApp.FFmpeg, "ffmpeg.exe"},
			{FFmpegApp.FFprobe, "ffprobe.exe"}
		};

		/// <summary>
		/// File path of the executable file
		/// </summary>
		private string exeFilePath;

		/// <summary>
		/// Gets the file path of the executable file
		/// </summary>
		public string ExeFilePath { get { return exeFilePath; } }

		/// <summary>
		/// Application Parameters
		/// </summary>
		protected string Parameters { get; set; }

		/// <summary>
		/// Wait for associated process to exit in miliseconds
		/// </summary>
		protected int WaitForExitTime { get; set; }

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="fFmpegApp"></param>
		protected FFmpegCommand(FFmpegApp fFmpegApp) : this(ffMpegApps[fFmpegApp]) {}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="exeFilePath">file path of the executable ffmpeg file</param>
		protected FFmpegCommand(string exeFilePath) {
			this.exeFilePath = exeFilePath;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Execute command
		/// </summary>
		public void Execute() {
			if(!File.Exists(exeFilePath)) {
				throw new FileNotFoundException(String.Format("Could not find the executable ffmpeg application in: {0}.", exeFilePath));
			}

			ProcessStartInfo processStartInfo = new ProcessStartInfo(exeFilePath, Parameters) {
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
			};

			string output = null;
			string error = null;
			Process proc = null;
			using(proc = Process.Start(processStartInfo)) {
				manipulateWithProcess(proc);

				output = proc.StandardOutput.ReadToEnd();
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
		/// Method provides hook to manipulate with running FFmpeg process.
		/// Default: Wait specified number of miliseconds for the associated process to end.
		/// </summary>
		/// <param name="proc">FFmpeg running process</param>
		protected virtual void manipulateWithProcess(Process proc) {
			if (WaitForExitTime > 0) {
				proc.WaitForExit(WaitForExitTime);
			} else {
				proc.WaitForExit();
			}
		}

		#endregion
	}
}
