using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Runtime.Serialization;

namespace ZTepsic.FFmpeg.Exceptions {
	/// <summary>
	/// FFmpeg Operation not permited Exception 
	/// </summary>
	public class OperationNotPermittedException : FFmpegException {

		#region Members

		private const string MESSAGE = "Operation not permitted";
		private const int CODE = -1;

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Initializes a new instance of the OperationNotPermittedException class
		/// </summary>
		public OperationNotPermittedException() : base(MESSAGE, CODE) { }

		/// <summary>
		/// Initializes a new instance of the OperationNotPermittedException class with a reference to the inner exception
		/// that is the cause of this exception.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public OperationNotPermittedException(Exception innerException) : base(MESSAGE, CODE, innerException) { }

		/// <summary>
		/// Initializes a new instance of the OperationNotPermittedException class with serialized data.
		/// </summary>
		/// <param name="info">The SerializationInfo thet holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected OperationNotPermittedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

	}
}
