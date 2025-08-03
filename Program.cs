namespace EasyAutoScript
{
	class Program
	{
		private static readonly bool debugMode = true;

		static async Task Main(string[] args)
		{
			string code = File.ReadAllText("EasyAutoScript.txt");

			Console.WriteLine("Parsing Tokens\n");

			Console.WriteLine("Finished Parsing, Executing Program:\n");

			await Task.CompletedTask;
		}
	}
}