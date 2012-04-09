using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace ZTepsic.FFmpeg.Exceptions {
	/// <summary>
	/// FFmpeg error
	/// </summary>
	public class FFmpegException : Exception {

		#region Members

		/// <summary>
		/// FFmpeg error code
		/// </summary>
		public int Code { get; private set; }

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Initializes a new instance of the FFmpegException class
		/// </summary>
		public FFmpegException() { }

		/// <summary>
		/// Initializes a new instance of the FFmpegException class with a specified error messsage.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="code">FFmpeg error code</param>
		public FFmpegException(string message, int code) : base(message) {
			Code = code;
		}

		/// <summary>
		/// Initializes a new instance of the FFmpegException class with a specified error message and a reference to the inner exception
		/// that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="code">FFmpeg error code</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public FFmpegException(string message, int code, Exception innerException) : base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the FFmpegException class with serialized data.
		/// </summary>
		/// <param name="info">The SerializationInfo thet holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected FFmpegException(SerializationInfo info, StreamingContext context) : base(info, context) {}

		#endregion

	}
}
