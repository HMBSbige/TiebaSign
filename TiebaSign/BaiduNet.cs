using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TiebaSign
{
	public static class BaiduNet
	{
		private const string SignUrl = @"http://c.tieba.baidu.com/c/c/forum/sign";
		private const string TiebaListUrl = @"http://c.tieba.baidu.com/c/f/forum/favocommend";//http://c.tieba.baidu.com/c/f/forum/like
		private const string TbsUrl = @"http://tieba.baidu.com/dc/common/tbs";

		public static async Task<string> Sign(string BDUSS, long fid, string kw, string tbs)
		{
			var sign = Util.Md5($@"BDUSS={BDUSS}fid={fid}kw={kw}tbs={tbs}tiebaclient!!!").ToUpper();

			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>(@"BDUSS", BDUSS),
				new KeyValuePair<string, string>(@"fid", Convert.ToString(fid)),
				new KeyValuePair<string, string>(@"kw", kw),
				new KeyValuePair<string, string>(@"tbs", tbs),
				new KeyValuePair<string, string>(@"sign", sign),
			});

			var res = await Util.Post(SignUrl, content);
			return res;
		}

		public static async Task<string> GetForum(string BDUSS)
		{
			var sign = Util.Md5($@"BDUSS={BDUSS}tiebaclient!!!").ToUpper();

			var content = new FormUrlEncodedContent(new[]
			{
					new KeyValuePair<string, string>(@"BDUSS", BDUSS),
					new KeyValuePair<string, string>(@"sign", sign),
			});

			var res = await Util.Post(TiebaListUrl, content);
			return res;
		}

		public static async Task<string> GetTbs(string BDUSS)
		{
			var content = new FormUrlEncodedContent(new[]
			{
					new KeyValuePair<string, string>(@"BDUSS", BDUSS)
			});

			var res = await Util.Post(TbsUrl, content);
			return res;
		}
	}
}
