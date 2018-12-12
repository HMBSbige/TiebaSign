using System;

namespace TiebaSign.Reply
{
	public class ErrorReply
	{
		protected long ErrorCode;
		public string ErrorMsg;
		public DateTime Time;

		protected ErrorReply()
		{
			ErrorCode = 110001L;
			ErrorMsg = "\u672a\u77e5\u9519\u8bef";
			Time = new DateTime(1970, 1, 1);
		}

		public virtual void Parse(string jsonStr)
		{
			dynamic s = SimpleJson.SimpleJson.DeserializeObject(jsonStr);
			ErrorCode = Convert.ToInt64(s[@"error_code"]);
			if (ErrorCode != 0)
			{
				ErrorMsg = s[@"error_msg"];
			}
			Time = Ntp.GetTime(Convert.ToString(s[@"time"]));
		}

		public override string ToString()
		{
			return $@"[{Time}] Error {ErrorCode}:{ErrorMsg}";
		}
	}
}
