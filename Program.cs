using EasyAutoScript.Statements;

namespace EasyAutoScript
{
	class Program
	{
		private static readonly bool debugMode = true;

		static async Task Main(string[] args)
		{
			string code = string.Empty;
			if (args.Length > 0)
			{
				code = File.ReadAllText("EasyAutoScriptTest.txt");
			}
			else
			{
				code = File.ReadAllText("EasyAutoScript.txt");
			}

			Console.WriteLine("Parsing Tokens\n");

			Lexer lexer = new(code);
			List<Token> tokens = lexer.Tokenise();

			if (debugMode)
			{
				foreach (var token in tokens)
				{
					Console.WriteLine(token);
				}
				Console.WriteLine();
			}

			Parser parser = new(tokens);
			List<IStatement> statements = parser.ParseTokens();

			if (debugMode)
			{
				foreach (var statement in statements)
				{
					Console.WriteLine(statement);
				}
				Console.WriteLine();
			}

			Console.WriteLine("Finished Parsing, Executing Program:\n");

			Interpreter interpreter = new(statements);
			await interpreter.Interpret();

			await Task.CompletedTask;
		}
	}
}