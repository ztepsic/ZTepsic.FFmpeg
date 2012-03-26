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
		/// 
		/// </summary>
		private readonly MediaFormatInfo format;

		/// <summary>
		/// 
		/// </summary>
		public MediaFormatInfo Format { get; internal set; }

		/// <summary>
		/// 
		/// </summary>
		private IList<MediaStreamInfo> streams;

		/// <summary>
		/// 
		/// </summary>
		public IList<MediaStreamInfo> Streams {
			get { return new ReadOnlyCollection<MediaStreamInfo>(streams); }
		}

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Internal Constructor
		/// </summary>
		/// <param name="format"></param>
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
