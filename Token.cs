namespace EasyAutoScript
{
    public class Token(TokenType Type, string Lexeme, object? Literal, int Line)
    {
        public TokenType Type { get; } = Type; // The token found = Number
        public string Lexeme { get; } = Lexeme; // The keyword/idenifier = 123
        public object? Literal { get; } = Literal; // The actual value = 123
        public int Line { get; } = Line; // For parser debugging

        public override string ToString()
        {
            return $"{Type} {Lexeme} {Literal} {Line}";
        }
    }
}