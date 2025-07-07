namespace EasyAutoScript
{
    public class Lexer(string code)
    {
        private readonly string _code = code;
        private readonly List<Token> _tokens = [];
        private int _current = 0;
        private int _line = 1;

        public List<Token> Tokenise()
        {
            while (!IsAtEnd())
            {
                ScanToken();
            }
            AddToken(TokenType.EOF, "");

            return _tokens;
        }

        public void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.OpenParenthesis, "(");
                    break;
                case ')':
                    AddToken(TokenType.CloseParenthesis, ")");
                    break;

                case '\n': // New Line
                    _line++;
                    break;

                case '\t': // White space characters
                case ' ':
                case '\r':
                    break;

                case '"':
                    String();
                    break;

                default:
                    if (char.IsNumber(c))
                    {
                        Number();
                        return;
                    }

                    throw new Exception($"Unknown character: {c} on line: '{_line}'");
            }
        }

        private void Number()
        {
            string Lexeme = _code[_current - 1].ToString();

            while (!IsAtEnd())
            {
                char c = Peek();
                if (!char.IsNumber(c))
                {
                    break;
                }
                Lexeme += c;
                _current++;
            }

            // Check if it is a decimal
            if (!IsAtEnd() && Peek() == '.')
            {
                Lexeme += Advance();
                while (!IsAtEnd())
                {
                    char c = Peek();
                    if (!char.IsNumber(c))
                    {
                        break;
                    }
                    Lexeme += c;
                    _current++;
                }
            }

            AddToken(TokenType.Number, Lexeme, Convert.ToDouble(Lexeme));
        }

        private void String()
        {
            string Lexeme = _code[_current - 1].ToString();

            while (!IsAtEnd())
            {
                char c = Advance();
                if (c == '"')
                {
                    Lexeme += c;
                    break;
                }
                Lexeme += c;
            }

            AddToken(TokenType.String, Lexeme, Lexeme);
        }

        #region Helpers
        public void AddToken(TokenType Type, string Lexeme)
        {
            _tokens.Add(new(Type, Lexeme, null, _line));
        }

        public void AddToken(TokenType Type, string Lexeme, object Literal)
        {
            _tokens.Add(new(Type, Lexeme, Literal, _line));
        }

        /// <summary>
        /// Returns the _current char (c)
        /// </summary>
        /// <returns></returns> Returns the _current char (c)
        private char Peek()
        {
            return _code[_current];
        }

        /// <summary>
        /// Returns the _current char (c) Then Advances _current + 1
        /// </summary>
        /// <returns></returns> Returns the _current char (c)
        public char Advance()
        {
            char c = _code[_current];
            _current++;
            return c;
        }

        /// <summary>
        /// Checks if we are at the end of the file
        /// </summary>
        /// <returns></returns> Boolean for if at end of file
        public bool IsAtEnd()
        {
            return _current >= _code.Length;
        }
        #endregion
    }
}