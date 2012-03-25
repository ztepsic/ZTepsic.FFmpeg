using System;
using System.Text;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// Information about the container format of the input multimedia stream.
	/// </summary>
	public class MediaFormatInfo {

		#region Members

		private const int ODD_PRIME = 31;
		private const int INITIAL_HASH_CODE_VALUE = 17;
		private int hashCode;

		/// <summary>
		/// File name
		/// </summary>
		private readonly string fileName;

		/// <summary>
		/// Gets file name
		/// </summary>
		public string FileName {
			get { return fileName; }
		}

		/// <summary>
		/// Short format name (eg. "flv")
		/// </summary>
		private readonly string format;

		/// <summary>
		/// Gets short format name (eg. "flv")
		/// </summary>
		public string Format { get { return format; } }

		/// <summary>
		/// Long format name (eg. "FLV format")
		/// </summary>
		public string FormatLongName { get; internal set; }

		/// <summary>
		/// Total bitrate (bps)
		/// </summary>
		public decimal Bitrate { get; internal set; }

		/// <summary>
		/// Media duration in seconds
		/// </summary>
		public decimal Duration { get; internal set; }

		/// <summary>
		/// Start time in seconds
		/// </summary>
		public decimal StartTime { get; internal set; }

		/// <summary>
		/// File size
		/// </summary>
		public decimal FileSize { get; internal set; }

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Constructor
		/// </summary>
		internal MediaFormatInfo(string fileName, string format) {
			this.fileName = fileName;
			this.format = format;
		}

		#endregion

		#region Methods

		public override string ToString() {
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Filename: ")
				.Append(fileName)
				.Append("; ")
				.Append("Format: ")
				.Append(format)
				.Append("; ")
				.Append("Format long name: ")
				.Append(FormatLongName)
				.Append("; ")
				.Append("Bitrate: ")
				.Append(Bitrate)
				.Append("; ")
				.Append("Duration: ")
				.Append(Duration)
				.Append(" sec; ")
				.Append("Start Time: ")
				.Append(StartTime)
				.Append(" sec; ")
				.Append("File size: ")
				.Append(FileSize)
				;

			return stringBuilder.ToString();
		}

		public override bool Equals(object obj) {
			if (this == obj) {
				return true;
			}

			if(!(obj is MediaFormatInfo)) {
				return false;
			}

			MediaFormatInfo mediaFormatInfo = obj as MediaFormatInfo;
			return fileName.Equals(mediaFormatInfo.FileName) &&
				format.Equals(mediaFormatInfo.Format) &&
				FormatLongName.Equals(mediaFormatInfo.FormatLongName) &&
				Bitrate == mediaFormatInfo.Bitrate &&
				Duration == mediaFormatInfo.Duration &&
				StartTime == mediaFormatInfo.StartTime &&
				FileSize == mediaFormatInfo.FileSize;
		}

		public override int GetHashCode() {
			int result = hashCode;
			if(result == 0) {
				result = INITIAL_HASH_CODE_VALUE;
				result = ODD_PRIME * result + fileName.GetHashCode();
				result = ODD_PRIME * result + format.GetHashCode();
				result = ODD_PRIME * result + FormatLongName.GetHashCode();
				result = ODD_PRIME * result + Bitrate.GetHashCode();
				result = ODD_PRIME * result + Duration.GetHashCode();
				result = ODD_PRIME * result + StartTime.GetHashCode();
				result = ODD_PRIME * result + FileSize.GetHashCode();
				hashCode = result;
			}

			return result;
		}

		#endregion

	}
}
