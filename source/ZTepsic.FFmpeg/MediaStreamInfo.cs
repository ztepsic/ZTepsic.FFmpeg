
namespace ZTepsic.FFmpeg {
	/// <summary>
	/// Information about each media stream contained in the input multimedia stream.
	/// </summary>
	public class MediaStreamInfo {

		#region Members

		/// <summary>
		/// Stream index inside the container (0-based)
		/// </summary>
		private int index;

		/// <summary>
		/// Stream type, either "audio" or "video"
		/// </summary>
		private string type;

		/// <summary>
		/// Codec short name (eg. "vorbis", "theora")
		/// </summary>
		private string codec;

		/// <summary>
		/// Codec long/decriptive name
		/// </summary>
		private string codecLongName;

		/// <summary>
		/// Stream duration in seconds
		/// </summary>
		private decimal duration;

		#region Video specific

		/// <summary>
		/// Width of video in pixels
		/// </summary>
		private int videoWidth;

		/// <summary>
		/// Height of video in pixels
		/// </summary>
		private int videoHeight;

		/// <summary>
		/// Average frames per second
		/// </summary>
		private int videoFps;

		#endregion

		#region Audio specific

		/// <summary>
		/// The number of channels in the stream
		/// </summary>
		private int audioChannels;

		/// <summary>
		/// Sample rate (Hz)
		/// </summary>
		private decimal audioSamplerate;

		#endregion

		#endregion

		#region Constructors and Init

		#endregion

		#region Methods

		#endregion

	}
}
