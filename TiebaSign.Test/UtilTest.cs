using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using TiebaSign.Reply;

namespace TiebaSign.Test
{
	[TestClass]
	public class UtilTest
	{
		[TestMethod]
		public void TestTime()
		{
			const int i = 1544520933;
			var time = Ntp.GetTime($@"{i}").ToLocalTime();

			Console.WriteLine(time.ToString(CultureInfo.CurrentCulture));

			var ans = new DateTime(2018, 12, 11, 17, 35, 33);
			Assert.AreEqual(ans, time);
		}

		[TestMethod]
		public void TestMd5()
		{
			const string t1 = @"123456";
			const string t2 = @"abcdef";
			const string t3 = @"±É¸ç";
			const string t4 = @"HMBSbige";
			const string a1 = @"e10adc3949ba59abbe56e057f20f883e";
			const string a2 = @"e80b5017098950fc58aad83c8c14978e";
			const string a3 = @"6948cbac42bd269cc4bcf94a7bacbf17";
			const string a4 = @"89d8365f7e928e337e36cd0b4c5216eb";
			Assert.AreEqual(Util.Md5(t1), a1);
			Assert.AreEqual(Util.Md5(t2), a2);
			Assert.AreEqual(Util.Md5(t3), a3);
			Assert.AreEqual(Util.Md5(t4), a4);
		}
	}
}
