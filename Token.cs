namespace EasyAutoScript
{
    public class Token(TokenType type, string lexeme, object? literal, int line)
    {
        public TokenType Type { get; } = type; // The TokenType we are using: String
        public string Lexeme { get; } = lexeme; // The string text that was found: "Hello"
        public object? Literal { get; } = literal; // The object value of what was found so in this case the string: Hello
        public int Line { get; } = line; // The line it was found on: 1

        public override string ToString()
        {
            return $"{Type} {Lexeme} {Literal} {Line}";
        }
    }
}