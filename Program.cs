namespace EasyAutoScript
{
	class Program
	{
		private static readonly bool debugMode = true;

		static async Task Main(string[] args)
		{
			string code = File.ReadAllText("EasyAutoScript.txt");

			Console.WriteLine("Parsing Tokens");

			Lexer lexer = new(code);
			List<Token> tokens = lexer.Tokenise();

			if (debugMode)
			{
				Console.WriteLine();
				foreach (Token token in tokens)
				{
					Console.WriteLine(token.ToString());
				}
				Console.WriteLine();
			}

			Console.WriteLine("Parsing successful, executing program\n");

			Console.WriteLine("Nothing here yet");

			Console.WriteLine("\nProgram finished");

			await Task.CompletedTask;
		}
	}
}