using System;

namespace TiebaSign.Reply
{
	public class SignReply : ErrorReply
	{
		public string LevelName { get; private set; }
		public DateTime SignTime { get; private set; }
		public long SignBonusPoint { get; private set; }
		public long UserId { get; private set; }
		public long MissSignNum { get; private set; }
		public long ContSignNum { get; private set; }
		public long UserSignRank { get; private set; }

		private SignReply()
		{
			LevelName = string.Empty;
			SignTime = new DateTime(1970, 1, 1);
			SignBonusPoint = 0L;
			UserId = 0L;
			MissSignNum = 0L;
			ContSignNum = 0L;
			UserSignRank = 0L;
		}

		public SignReply(string name) : this()
		{
			Name = name;
		}

		public override void Parse(string jsonStr)
		{
			base.Parse(jsonStr);
			if (ErrorCode == 0)
			{
				dynamic s = SimpleJson.SimpleJson.DeserializeObject(jsonStr);
				LevelName = s[@"user_info"][@"level_name"];
				SignTime = Ntp.GetTime(Convert.ToString(s[@"user_info"][@"sign_time"])).ToLocalTime();
				SignBonusPoint = s[@"user_info"][@"sign_bonus_point"];
				UserId = s[@"user_info"][@"user_id"];
				MissSignNum = s[@"user_info"][@"miss_sign_num"];
				ContSignNum = s[@"user_info"][@"cont_sign_num"];
				UserSignRank = s[@"user_info"][@"user_sign_rank"];
			}
		}

		public override string ToString()
		{
			if (ErrorCode == 0)
			{
				return $@"[{SignTime}] Info {Name}:{LevelName}:今日本吧第 {UserSignRank} 个签到，经验 +{SignBonusPoint}，漏签 {MissSignNum} 天，连续签到 {ContSignNum} 天";
			}
			else
			{
				return base.ToString();
			}
		}
	}
}
