using System;
using System.Runtime.Serialization;

namespace ZTepsic.FFmpeg.Exceptions {
	/// <summary>
	/// FFmpeg Invalid data found when processing input Exception 
	/// </summary>
	public class InvalidDataException : FFmpegException {

		#region Members

		private const string MESSAGE = "Invalid data found when processing input";
		private const int CODE = -1094995529;

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Initializes a new instance of the InvalidDataException class
		/// </summary>
		public InvalidDataException() : base(MESSAGE, CODE) { }

		/// <summary>
		/// Initializes a new instance of the UnknownException class with a reference to the inner exception
		/// that is the cause of this exception.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public InvalidDataException(Exception innerException) : base(MESSAGE, CODE, innerException) { }

		/// <summary>
		/// Initializes a new instance of the UnknownException class with serialized data.
		/// </summary>
		/// <param name="info">The SerializationInfo thet holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

	}
}