using System;
using System.Text;

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
		/// Stream duration in seconds
		/// </summary>
		public decimal Duration { get; internal set; }

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
		/// Average frames per second
		/// </summary>
		private int videoFps;

		/// <summary>
		/// Average frames per second
		/// </summary>
		public int VideoFps {
			get { return videoFps; }
			internal set {
				if (type == MediaStreamType.Video) {
					videoFps = value;
				} else {
					throw new ArgumentException("Can't set VideoFps property on no video type.");
				}
			}
		}

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
				videoFps == mediaStreamInfo.VideoFps &&
				audioChannels == mediaStreamInfo.AudioChannels &&
				audioSampleRate == mediaStreamInfo.AudioSampleRate &&
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
				result = ODD_PRIME * result + videoFps.GetHashCode();
				result = ODD_PRIME * result + audioChannels.GetHashCode();
				result = ODD_PRIME * result + audioSampleRate.GetHashCode();
				hashCode = result;
			}

			return result;
		}

		#endregion

	}
}
