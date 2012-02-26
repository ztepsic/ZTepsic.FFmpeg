using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZTepsic.FFmpeg {
	/// <summary>
	/// Command interface
	/// </summary>
	public interface ICommand {
		/// <summary>
		/// Execute command
		/// </summary>
		void Execute();
	}
}
