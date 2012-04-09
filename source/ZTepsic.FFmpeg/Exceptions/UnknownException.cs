using System;
using System.Runtime.Serialization;

namespace ZTepsic.FFmpeg.Exceptions {
	/// <summary>
	/// FFmpeg Unknown error Exception 
	/// </summary>
	public class UnknownException : FFmpegException {

		#region Members

		private const string MESSAGE = "Unknown error";
		private const int CODE = -10060;

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Initializes a new instance of the UnknownException class
		/// </summary>
		public UnknownException() : base(MESSAGE, CODE) { }

		/// <summary>
		/// Initializes a new instance of the UnknownException class with a reference to the inner exception
		/// that is the cause of this exception.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public UnknownException(Exception innerException) : base(MESSAGE, CODE, innerException) { }

		/// <summary>
		/// Initializes a new instance of the UnknownException class with serialized data.
		/// </summary>
		/// <param name="info">The SerializationInfo thet holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected UnknownException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

	}
}