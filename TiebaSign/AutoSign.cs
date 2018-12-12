using System;
using System.Threading.Tasks;
using TiebaSign.Reply;

namespace TiebaSign
{
	public class AutoSign
	{
		private readonly string BDUSS;
		public AutoSign(string bduss)
		{
			BDUSS = bduss;
		}

		public async Task Start()
		{
			var forums = new ForumList();
			try
			{
				var forumStr = await BaiduNet.GetForum(BDUSS);
				forums.Parse(forumStr);
				Console.WriteLine(@"获取贴吧列表成功！");
				Console.WriteLine(forums.ToString());
			}
			catch
			{
				Console.WriteLine(@"获取贴吧列表失败！");
				return;
			}

			var success = 0;
			foreach (var forum in forums.Forums)
			{
				var signReply = new SignReply(forum.Name);
				try
				{
					var res = await BaiduNet.Sign(BDUSS, forum.Fid, forum.Name, forums.Tbs);
					signReply.Parse(res);
					Console.WriteLine(signReply.ToString());
					if (signReply.ErrorCode == 0L || signReply.ErrorCode == 160002L)
					{
						++success;
					}
				}
				catch
				{
					Console.WriteLine($@"[{DateTime.Now}] Error {forum.Name}签到失败！");
				}
			}
			Console.WriteLine($@"签到完成:{success}/{forums.Forums.Count}");
		}
	}
}
