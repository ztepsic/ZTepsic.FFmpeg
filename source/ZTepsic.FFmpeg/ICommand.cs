using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZTepsic.FFmpeg {
	public interface ICommand {
		/// <summary>
		/// Execute command
		/// </summary>
		void Execute();
	}
}
