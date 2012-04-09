using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZTepsic.FFmpeg.Exceptions {
	/// <summary>
	/// FFmpeg Input/Output Exception 
	/// </summary>
	public class IOException : FFmpegException {

		#region Members

		private const string MESSAGE = "Input/output error";
		private const int CODE = -5;

		#endregion

		#region Constructors and Init

		/// <summary>
		/// Initializes a new instance of the IOException class
		/// </summary>
		public IOException() : base(MESSAGE, CODE) { }

		/// <summary>
		/// Initializes a new instance of the IOException class with a reference to the inner exception
		/// that is the cause of this exception.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public IOException(Exception innerException) : base(MESSAGE, CODE, innerException) { }

		/// <summary>
		/// Initializes a new instance of the IOException class with serialized data.
		/// </summary>
		/// <param name="info">The SerializationInfo thet holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected IOException(SerializationInfo info, StreamingContext context) : base(info, context) {}

		#endregion

	}
}
