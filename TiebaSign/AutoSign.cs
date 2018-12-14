using System;
using System.Collections.Generic;
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

		private async Task<(int, List<Forum>)> SignAll(IEnumerable<Forum> list, string tbs)
		{
			var success = 0;
			var failList = new List<Forum>();
			foreach (var forum in list)
			{
				var signReply = new SignReply(forum.Name);
				try
				{
					var res = await BaiduNet.Sign(BDUSS, forum.Fid, forum.Name, tbs);
					signReply.Parse(res);
					Console.WriteLine(signReply.ToString());
					if (signReply.ErrorCode == 0L || signReply.ErrorCode == 160002L)
					{
						++success;
					}
					else
					{
						failList.Add(forum);
					}
				}
				catch
				{
					failList.Add(forum);
					Console.WriteLine($@"[{DateTime.Now}] Error {forum.Name}签到失败！");
				}
			}

			return (success, failList);
		}

		private async Task SignAll(int retryTime)
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

			int success;
			List<Forum> failList;


			(success, failList) = await SignAll(forums.Forums, forums.Tbs);

			if (success != forums.Forums.Count)
			{
				Console.WriteLine(@"存在签到失败贴吧，重试开始");
				for (var i = 0; i < retryTime; ++i)
				{
					Console.WriteLine($@"第 {i + 1} 次重试");
					int successT;
					(successT, failList) = await SignAll(failList, forums.Tbs);
					success += successT;
					if (success == forums.Forums.Count)
					{
						break;
					}
				}
			}

			Console.WriteLine($@"签到完成:{success}/{forums.Forums.Count}");
		}

		public async Task Start(int retryTime = 3)
		{
			while (true)
			{
				await SignAll(retryTime);
				var waitTime = Convert.ToInt32(Util.GetCountdown());
				Console.WriteLine($@"等待 {Util.ParseTime(waitTime)} 后执行");
				await Task.Delay(waitTime);
			}
			// ReSharper disable once FunctionNeverReturns
		}
	}
}
