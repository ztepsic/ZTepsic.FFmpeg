using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ZTepsic.FFmpeg {
	public class RtmpCheckIfOnlineCmd : FFmpegCommand {

		#region Members
		#endregion

		#region Constructors and Init

		public RtmpCheckIfOnlineCmd() {
			parameters = "";
		}

		#endregion

		#region Methods

		protected override void processResult(string output, string error) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
