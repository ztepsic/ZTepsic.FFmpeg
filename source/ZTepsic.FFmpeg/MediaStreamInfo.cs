using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// Information about each media stream contained in the input multimedia stream.
	/// </summary>
	public class MediaStreamInfo {

		#region Members

		private const int ODD_PRIME = 31;
		private const int INITIAL_HASH_CODE_VALUE = 17;
		private int hashCode;

		/// <summary>
		/// Media stream type
		/// </summary>
		public enum MediaStreamType {
			/// <summary>
			/// None
			/// </summary>
			None,

			/// <summary>
			/// Video stream type
			/// </summary>
			Video,

			/// <summary>
			/// Audio stream type
			/// </summary>
			Audio
		}

		/// <summary>
		/// Stream index inside the container (0-based)
		/// </summary>
		private readonly int index;

		/// <summary>
		/// Gets stream index inside the container (0-based)
		/// </summary>
		public int Index { get { return index; } }

		/// <summary>
		/// Stream type, either "audio" or "video"
		/// </summary>
		private readonly MediaStreamType type;

		/// <summary>
		/// Gets stream type
		/// </summary>
		public MediaStreamType Type {get { return type; }}

		/// <summary>
		/// Codec short name (eg. "vorbis", "theora")
		/// </summary>
		private readonly string codec;

		/// <summary>
		/// Gets codec short name
		/// </summary>
		public string Codec { get { return codec; } }

		/// <summary>
		/// Codec long/descriptive name
		/// </summary>
		public string CodecLongName { get; internal set; }

		/// <summary>
		/// Frame rate
		/// </summary>
		private string frameRateStr;

		/// <summary>
		/// Frame rate
		/// </summary>
		public string FrameRateStr {
			get { return frameRateStr; }
			set {
				frameRateStr = value;
				FrameRate = toDecimalRatio(frameRateStr);
			}
		}

		/// <summary>
		/// Frame rate
		/// </summary>
		public decimal FrameRate { get; private set; }

		/// <summary>
		/// Avg frame rate
		/// </summary>
		private string avgFrameRateStr;

		/// <summary>
		/// Avg frame rate
		/// </summary>
		public string AvgFrameRateStr {
			get { return avgFrameRateStr; }
			set { 
				avgFrameRateStr = value;
				AvgFrameRate = toDecimalRatio(avgFrameRateStr);
			}
		}

		/// <summary>
		/// Average frame rate
		/// </summary>
		public decimal AvgFrameRate { get; private set; }

		/// <summary>
		/// Stream duration in seconds
		/// </summary>
		public decimal Duration { get; internal set; }

		/// <summary>
		/// Start time in seconds
		/// </summary>
		public decimal StartTime { get; internal set; }

		#region Video specific

		/// <summary>
		/// Width of video in pixels
		/// </summary>
		private int videoWidth;

		/// <summary>
		/// Width of video in pixels
		/// </summary>
		public int VideoWidth {
			get { return videoWidth; }
			internal set {
				if(type == MediaStreamType.Video) {
					videoWidth = value;
				} else {
					throw new ArgumentException("Can't set VideoWidth property on no video type.");
				}
			}
		}

		/// <summary>
		/// Height of video in pixels
		/// </summary>
		private int videoHeight;

		/// <summary>
		/// Height of video in pixels
		/// </summary>
		public int VideoHeight {
			get { return videoHeight; }
			internal set {
				if(type == MediaStreamType.Video) {
					videoHeight = value;
				} else {
					throw new ArgumentException("Can't set VideoHeight property on no video type.");
				}
			}
		}

		/// <summary>
		/// Sample aspect ratio
		/// </summary>
		private string videoSampleAspectRatioStr;

		/// <summary>
		/// Sample aspect ratio
		/// </summary>
		public string VideoSampleAspectRatioStr {
			get { return videoSampleAspectRatioStr; }
			internal set {
				if (type == MediaStreamType.Video) {
					videoSampleAspectRatioStr = value;
					VideoSampleAspectRatio = toDecimalRatio(videoSampleAspectRatioStr);
				} else {
					throw new ArgumentException("Can't set ratioStr property on no video type.");
				}
			}
		}

		/// <summary>
		/// Sample aspect ratio
		/// </summary>
		public decimal VideoSampleAspectRatio { get; private set; }

		/// <summary>
		/// Display aspect ratio
		/// </summary>
		private string videoDisplayAspectRatioStr;

		/// <summary>
		/// Display aspect ratio
		/// </summary>
		public string VideoDisplayAspectRatioStr {
			get { return videoDisplayAspectRatioStr; }
			internal set {
				if (type == MediaStreamType.Video) {
					videoDisplayAspectRatioStr = value;
					VideoDisplayAspectRatio = toDecimalRatio(videoDisplayAspectRatioStr);
				} else {
					throw new ArgumentException("Can't set videoDisplayAspectRatio property on no video type.");
				}
			}
		}

		/// <summary>
		/// Display aspect ratio
		/// </summary>
		public decimal VideoDisplayAspectRatio { get; private set; }

		#endregion

		#region Audio specific

		/// <summary>
		/// The number of channels in the stream
		/// </summary>
		private int audioChannels;

		/// <summary>
		/// The number of channels in the stream
		/// </summary>
		public int AudioChannels {
			get { return audioChannels; }
			internal set {
				if(type == MediaStreamType.Audio) {
					audioChannels = value;
				} else {
					throw new ArgumentException("Can't set AudioChannels property on no audio type.");
				}
			}
		}

		/// <summary>
		/// Sample rate (Hz)
		/// </summary>
		private decimal audioSampleRate;

		/// <summary>
		/// Sample rate (Hz)
		/// </summary>
		public decimal AudioSampleRate {
			get { return audioSampleRate; }
			internal set {
				if(type == MediaStreamType.Audio) {
					audioSampleRate = value;
				} else {
					throw new ArgumentException("Can't set AudioSampleRate property on no audio type.");
				}
			}
		}

		#endregion

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Internal Constructor
		/// </summary>
		/// <param name="index">stream index inside the container (0-based)</param>
		/// <param name="codec">codec short name</param>
		/// <param name="type">Stream type</param>
		internal MediaStreamInfo(int index, string codec, MediaStreamType type) {
			this.index = index;
			this.codec = codec;
			this.type = type;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Converts string ratio to decimal representation
		/// </summary>
		/// <param name="ratioStr">string representation of ratio</param>
		/// <returns>decimal representation of ratio</returns>
		private static decimal toDecimalRatio(string ratioStr) {
			decimal ratio = 0;

			var split = Regex.Split(ratioStr, ":|/");
			if(split.Length == 2) {
				decimal numerator = 0;
				decimal denominator = 0;

				try {
					numerator = Decimal.Parse(split[0]);
					denominator = Decimal.Parse(split[1]);	
				} catch(Exception) {
					numerator = 0;
					denominator = 0;
				}
				

				if (numerator > 0 && denominator > 0) {
					ratio = numerator / denominator;
				}	
			}

			return ratio;
		}

		/// <summary>
		/// ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Index: ")
				.Append(index)
				.Append("; ")
				.Append("Codec: ")
				.Append(codec)
				.Append("; ")
				.Append("Codec long name: ")
				.Append(CodecLongName)
				.Append("; ")
				.Append("Codec type: ")
				.Append(type.ToString().ToLower())
				.Append("; ");

			switch (type) {
				case MediaStreamType.Video:
					stringBuilder.Append("Width: ")
						.Append(VideoWidth)
						.Append("; ")
						.Append("Height: ")
						.Append(videoHeight)
						.Append("; ");
					break;
				case MediaStreamType.Audio:
					stringBuilder.Append("Channels: ")
						.Append(audioChannels)
						.Append("; ")
						.Append("Sample rate: ")
						.Append(audioSampleRate)
						.Append("; ");
					break;

			}

			stringBuilder.Append("Duration: ")
				.Append(Duration)
				.Append(" sec;");

			return stringBuilder.ToString();
		}

		/// <summary>
		/// Equals
		/// </summary>
		/// <param name="obj">object for which the equality is checked</param>
		/// <returns>true if objects are equal, false otherwise</returns>
		public override bool Equals(object obj) {
			if (this == obj) {
				return true;
			}

			if (!(obj is MediaStreamInfo)) {
				return false;
			}

			MediaStreamInfo mediaStreamInfo = obj as MediaStreamInfo;
			return index == mediaStreamInfo.Index &&
				codec.Equals(mediaStreamInfo.Codec) &&
				CodecLongName.Equals(mediaStreamInfo.CodecLongName) &&
				type == mediaStreamInfo.Type &&
				videoWidth == mediaStreamInfo.videoWidth &&
				videoHeight == mediaStreamInfo.VideoHeight &&
				VideoSampleAspectRatio == mediaStreamInfo.VideoSampleAspectRatio &&
				videoSampleAspectRatioStr.Equals(mediaStreamInfo.VideoSampleAspectRatioStr) &&
				VideoDisplayAspectRatio == mediaStreamInfo.VideoDisplayAspectRatio &&
				videoDisplayAspectRatioStr.Equals(mediaStreamInfo.VideoDisplayAspectRatioStr) &&
				audioChannels == mediaStreamInfo.AudioChannels &&
				audioSampleRate == mediaStreamInfo.AudioSampleRate &&
				FrameRate == mediaStreamInfo.FrameRate &&
				AvgFrameRate == mediaStreamInfo.AvgFrameRate &&
				StartTime == mediaStreamInfo.StartTime &&
				Duration == mediaStreamInfo.Duration;
		}

		/// <summary>
		/// HashCode
		/// </summary>
		/// <returns>hash code</returns>
		public override int GetHashCode() {
			int result = hashCode;
			if (result == 0) {
				result = INITIAL_HASH_CODE_VALUE;
				result = ODD_PRIME * result + index;
				result = ODD_PRIME * result + codec.GetHashCode();
				result = ODD_PRIME * result + CodecLongName.GetHashCode();
				result = ODD_PRIME * result + type.GetHashCode();
				result = ODD_PRIME * result + videoWidth.GetHashCode();
				result = ODD_PRIME * result + videoHeight.GetHashCode();
				result = ODD_PRIME * result + VideoSampleAspectRatio.GetHashCode();
				result = ODD_PRIME * result + videoSampleAspectRatioStr.GetHashCode();
				result = ODD_PRIME * result + VideoDisplayAspectRatio.GetHashCode();
				result = ODD_PRIME * result + videoDisplayAspectRatioStr.GetHashCode();
				result = ODD_PRIME * result + audioChannels.GetHashCode();
				result = ODD_PRIME * result + audioSampleRate.GetHashCode();
				result = ODD_PRIME * result + FrameRate.GetHashCode();
				result = ODD_PRIME * result + AvgFrameRate.GetHashCode();
				result = ODD_PRIME * result + StartTime.GetHashCode();
				result = ODD_PRIME * result + Duration.GetHashCode();
				hashCode = result;
			}

			return result;
		}

		#endregion

	}
}
