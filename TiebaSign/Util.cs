using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TiebaSign
{
	public static class Util
	{
		public static async Task<string> Get(string uri, bool useProxy = false)
		{
			var httpClientHandler = new HttpClientHandler
			{
				UseProxy = useProxy
			};
			var httpClient = new HttpClient(httpClientHandler);
			var response = await httpClient.GetAsync(uri);
			response.EnsureSuccessStatusCode();
			var resultStr = await response.Content.ReadAsStringAsync();

			Debug.WriteLine(resultStr);
			return resultStr;
		}

		public static async Task<string> Post(string url, FormUrlEncodedContent content, bool useProxy = false)
		{
			var httpClientHandler = new HttpClientHandler
			{
				UseProxy = useProxy
			};
			var client = new HttpClient(httpClientHandler);
			var result = await client.PostAsync(url, content);
			var resultContent = await result.Content.ReadAsStringAsync();
			Debug.WriteLine(resultContent);
			return resultContent;
		}

		public static string Md5(string input)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			var data = Encoding.UTF8.GetBytes(input);
			var md5Data = md5.ComputeHash(data);
			md5.Clear();
			return md5Data.Aggregate(string.Empty, (current, t) => current + t.ToString(@"x2").PadLeft(2, '0'));
		}
	}
}
