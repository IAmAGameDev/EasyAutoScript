using EasyAutoScript.Statements;

namespace EasyAutoScript
{
	class Program
	{
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

			Lexer lexer = new(code);
			List<Token> tokens = lexer.Tokenise();

			Parser parser = new(tokens);
			List<IStatement> statements = parser.ParseTokens();

			Interpreter interpreter = new(statements);
			await interpreter.Interpret();

			await Task.CompletedTask;
		}
	}
}