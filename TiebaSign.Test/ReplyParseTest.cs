using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TiebaSign.Reply;

namespace TiebaSign.Test
{
	[TestClass]
	public class ReplyParseTest
	{
		private const string Error = @"{""error_code"":""160002"",""error_msg"":""\u4eb2\uff0c\u4f60\u4e4b\u524d\u5df2\u7ecf\u7b7e\u8fc7\u4e86"",""info"":[],""time"":1544520933,""ctime"":0,""logid"":2132998610,""server_time"":298185}";

		[TestMethod]
		public void SignReplyTest()
		{
			const string signSuccess = @"{""user_info"":{""user_id"":449002741,""is_sign_in"":1,""user_sign_rank"":738,""sign_time"":1544549341,""cont_sign_num"":9,""total_sign_num"":1767,""cout_total_sing_num"":1767,""hun_sign_num"":77,""total_resign_num"":0,""is_org_name"":0,""sign_bonus_point"":8,""miss_sign_num"":1,""level_name"":""\u62ab\u98ce\u6597\u58eb"",""levelup_score"":18000},""contri_info"":[],""server_time"":305827,""time"":1544549341,""ctime"":0,""logid"":1741121128,""error_code"":""0""}";

			var x1 = new SignReply();
			x1.Parse(Error);

			var x2 = new SignReply();
			x2.Parse(signSuccess);

			Console.WriteLine(x1.ToString());
			Assert.AreEqual(@"[2018/12/11 17:35:33] Error 160002:亲，你之前已经签过了", x1.ToString());
			Console.WriteLine(x2.ToString());
			Assert.AreEqual(@"[2018/12/12 1:29:01] Info 披风斗士:今日本吧第 738 个签到，经验 +8，漏签 1 天，连续签到 9 天", x2.ToString());
		}

		[TestMethod]
		public void ForumListTest()
		{
			const string success = @"{""forum_list"":[{""name"":""\u9006\u6218"",""id"":1829631,""is_like"":1,""favo_type"":2,""level_id"":3,""member_count"":null,""avatar"":"""",""slogan"":""""},{""name"":""\u4e03\u96c4\u4e89\u9738"",""id"":2387937,""is_like"":1,""favo_type"":2,""level_id"":133,""member_count"":null,""avatar"":"""",""slogan"":""""},{""name"":""\u5f31\u667a"",""id"":272685,""is_like"":1,""favo_type"":2,""level_id"":13,""member_count"":null,""avatar"":"""",""slogan"":""""},{""name"":""\u5c0f\u5175\u4f20\u5947"",""id"":6688,""is_like"":1,""favo_type"":2,""level_id"":13,""member_count"":null,""avatar"":"""",""slogan"":""""},{""name"":""\u60b2\u60e8\u4e16\u754c"",""id"":362671,""is_like"":1,""favo_type"":2,""level_id"":13,""member_count"":null,""avatar"":"""",""slogan"":""""},{""name"":""\u6b4c\u5267\u9b45\u5f71"",""id"":225536,""is_like"":1,""favo_type"":2,""level_id"":13,""member_count"":null,""avatar"":"""",""slogan"":""""}],""commend_forum_list"":[],""is_login"":1,""page"":{""page_size"":20,""offset"":0,""current_page"":1,""total_count"":0,""total_page"":0,""has_more"":0,""has_prev"":0},""anti"":{""tbs"":""1234""},""server_time"":31003,""time"":1544597962,""ctime"":0,""logid"":3562410370,""error_code"":""0""}";
			const string ans =
@"[2018/12/12 14:59:22] Info 总共有 6 个贴吧
TBS: 1234
逆战(1829631):3级
七雄争霸(2387937):133级
弱智(272685):13级
小兵传奇(6688):13级
悲惨世界(362671):13级
歌剧魅影(225536):13级
";

			var x1 = new ForumList();
			x1.Parse(Error);

			var x2 = new ForumList();
			x2.Parse(success);

			Console.WriteLine(x1.ToString());
			Assert.AreEqual(@"[2018/12/11 17:35:33] Error 160002:亲，你之前已经签过了", x1.ToString());

			Console.WriteLine(x2.ToString());
			Assert.AreEqual(ans, x2.ToString());
		}

	}
}
