namespace TiebaSign
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length == 1)
			{
				var BDUSS = args[0];
				var signTask = new AutoSign(BDUSS);
				signTask.Start().Wait();
			}
		}
	}
}
