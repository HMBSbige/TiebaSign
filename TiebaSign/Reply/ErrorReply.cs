using System;

namespace TiebaSign.Reply
{
	public class ErrorReply
	{
		public long ErrorCode { get; protected set; }
		public string ErrorMsg { get; protected set; }
		public DateTime Time { get; protected set; }
		public string Name;

		public ErrorReply()
		{
			ErrorCode = 110001L;
			ErrorMsg = "\u672a\u77e5\u9519\u8bef";
			Time = new DateTime(1970, 1, 1);
		}

		protected ErrorReply(string name) : this()
		{
			Name = name;
		}

		public virtual void Parse(string jsonStr)
		{
			dynamic s = SimpleJson.SimpleJson.DeserializeObject(jsonStr);
			ErrorCode = Convert.ToInt64(s[@"error_code"]);
			if (ErrorCode != 0)
			{
				ErrorMsg = s[@"error_msg"];
			}
			Time = Ntp.GetTime(Convert.ToString(s[@"time"])).ToLocalTime();
		}

		public override string ToString()
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				return $@"[{Time}] Error {ErrorCode}:{ErrorMsg}";
			}
			return $@"[{Time}] Error {Name}:{ErrorCode}:{ErrorMsg}";
		}
	}
}
