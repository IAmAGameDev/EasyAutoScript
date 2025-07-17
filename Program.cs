using EasyAutoScript.Statements;

namespace EasyAutoScript
{
	class Program
	{
		private static readonly bool debugMode = true;

		static async Task Main(string[] args)
		{
			string code = File.ReadAllText("EasyAutoScript.txt");

			Console.WriteLine("Parsing Tokens\n");

			Lexer lexer = new(code);
			List<Token> tokens = lexer.Tokenise();

			if (debugMode)
			{
				foreach (Token token in tokens)
				{
					Console.WriteLine(token.ToString());
				}
				Console.WriteLine();
			}

			Parser parser = new(tokens);
			List<IStatement> statements = parser.Parse();

			if (debugMode)
			{
				foreach (IStatement statement in statements)
				{
					Console.WriteLine(statement);
				}
				Console.WriteLine();
			}

			Console.WriteLine("Parsing successful, executing program\n");

			Interpreter interpreter = new(statements);
			interpreter.Interpret();

			Console.WriteLine("\nProgram finished");

			await Task.CompletedTask;
		}
	}
}