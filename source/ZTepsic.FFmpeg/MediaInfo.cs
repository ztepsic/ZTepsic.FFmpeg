using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// Information about media object.
	/// </summary>
	public class MediaInfo {

		#region Members

		/// <summary>
		/// MediaFormatInfo
		/// </summary>
		private readonly MediaFormatInfo format;

		/// <summary>
		/// Gets and Sets(internal) MediaFormatInfo
		/// </summary>
		public MediaFormatInfo Format { get { return format; } }

		/// <summary>
		/// MediaStreamInfos
		/// </summary>
		private IList<MediaStreamInfo> streams;

		/// <summary>
		/// Gets MediaStreamInfo
		/// </summary>
		public IList<MediaStreamInfo> Streams {
			get { return new ReadOnlyCollection<MediaStreamInfo>(streams); }
		}

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Internal Constructor
		/// </summary>
		/// <param name="format">MediaFormatInfo</param>
		/// <param name="streams">list of MediaStreamInfo objects</param>
		internal MediaInfo(MediaFormatInfo format, IList<MediaStreamInfo> streams) {
			this.format = format;
			this.streams = streams;
		}

		/// <summary>
		/// Internal Constructor
		/// </summary>
		/// <param name="format">MediaFormatInfo</param>
		internal MediaInfo(MediaFormatInfo format) {
			this.format = format;
			streams = new List<MediaStreamInfo>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds stream
		/// </summary>
		/// <param name="stream">stream</param>
		internal void AddStream(MediaStreamInfo stream) {
			streams.Add(stream);
		}

		#endregion

	}
}
