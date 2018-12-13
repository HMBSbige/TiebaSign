using System;
using System.Collections.Generic;
using System.Text;

namespace TiebaSign.Reply
{
	public class Forum
	{
		public string Name;
		public long Fid;
		public long Level;

		public Forum()
		{
			Name = string.Empty;
			Fid = 0;
			Level = 0;
		}

		public static Forum Parse(string jsonStr)
		{
			dynamic s = SimpleJson.SimpleJson.DeserializeObject(jsonStr);
			var res = new Forum
			{
				Name = s[@"name"],
				Fid = s[@"id"],
				Level = s[@"level_id"]
			};
			return res;
		}

		public override string ToString()
		{
			return $@"{Name}({Fid}):{Level}级";
		}
	}

	public class ForumList : ErrorReply
	{
		public readonly List<Forum> Forums;
		public string Tbs;

		public ForumList()
		{
			Forums = new List<Forum>();
			Tbs = string.Empty;
		}

		public override void Parse(string jsonStr)
		{
			base.Parse(jsonStr);
			if (ErrorCode == 0)
			{
				dynamic s = SimpleJson.SimpleJson.DeserializeObject(jsonStr);
				var list = s[@"forum_list"];
				Forums.Clear();
				foreach (var forum in list)
				{
					Forums.Add(Forum.Parse(forum.ToString()));
				}

				Tbs = s[@"anti"][@"tbs"];
				Time = Ntp.GetTime(Convert.ToString(s[@"time"])).ToLocalTime();
			}
		}

		public override string ToString()
		{
			if (ErrorCode == 0)
			{
				var sb = new StringBuilder();
				sb.AppendLine($@"[{Time}] Info 总共有 {Forums.Count} 个贴吧");
				sb.AppendLine($@"TBS: {Tbs}");
				foreach (var forum in Forums)
				{
					sb.AppendLine(forum.ToString());
				}
				return sb.ToString();
			}
			else
			{
				return base.ToString();
			}
		}
	}
}
