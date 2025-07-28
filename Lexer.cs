using EasyAutoScript.Exceptions;

namespace EasyAutoScript
{
    public class Lexer(string code)
    {
        private readonly string _code = code;
        private readonly List<Token> _tokens = [];
        private int _current = 0;
        private int _line = 1;
        private Dictionary<string, TokenType> _identifiers = new()
        {
            // Values
            { "true", TokenType.Boolean},
            { "false", TokenType.Boolean },

            // Data holders
            { "var", TokenType.Var },

            // Statements
            { "Clear", TokenType.Clear },
            { "Sleep", TokenType.Sleep },
            { "Write", TokenType.Write },

            // Expressions
            { "GetForegroundWindow", TokenType.GetForegroundWindow },
            { "GetOpenWindowTitle", TokenType.GetOpenWindowTitle },
        };

        public List<Token> Tokenise()
        {
            while (!IsAtEnd())
            {
                ScanToken();
            }
            AddToken(TokenType.EOF, "EOF");

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
                case '=':
                    AddToken(TokenType.Equals, "=");
                    break;
                case '!':
                    AddToken(TokenType.ExclamationMark, "!");
                    break;
                case '/':
                    Comment();
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
                    else if (IsAlphaNumeric(c))
                    {
                        Identifier();
                        return;
                    }

                    throw new LexerException($"Unknown character: {c} on line: '{_line}'");
            }
        }

        private void Comment()
        {
            Advance();

            if (Peek() == '/')
            {
                Advance();
            }

            while (!IsAtEnd())
            {
                if (Peek() == '\n')
                {
                    Advance();
                    break;
                }
                Advance();
            }
        }

        private void Identifier()
        {
            string identifier = _code[_current - 1].ToString();

            while (!IsAtEnd())
            {
                char c = Peek();
                if (!char.IsLetter(c))
                {
                    break;
                }
                identifier += Advance();
            }

            if (_identifiers.TryGetValue(identifier, out TokenType Type))
            {
                AddToken(Type, identifier, identifier);
                return;
            }

            _identifiers.Add(identifier, TokenType.Identifier);
            AddToken(TokenType.Identifier, identifier, identifier);
        }

        private void Number()
        {
            string value = _code[_current - 1].ToString();

            while (!IsAtEnd())
            {
                char c = Peek();
                if (!char.IsNumber(c))
                {
                    break;
                }
                value += c;
                Advance();
            }

            // Check if it is a decimal
            if (!IsAtEnd() && Peek() == '.' && char.IsNumber(PeekNext()))
            {
                value += Advance();
                while (!IsAtEnd())
                {
                    char c = Peek();
                    if (!char.IsNumber(c))
                    {
                        break;
                    }
                    value += c;
                    Advance();
                }
            }

            AddToken(TokenType.Number, value, Convert.ToDouble(value));
        }

        private void String()
        {
            string value = _code[_current - 1].ToString();

            while (!IsAtEnd())
            {
                char c = Advance();
                if (c == '"')
                {
                    value += c;
                    break;
                }
                value += c;
            }

            string Lexeme = value[1..^1];
            AddToken(TokenType.String, Lexeme, value);
        }

        #region Helpers
        /// <summary>
        /// Adds a token 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Lexeme"></param>
        public void AddToken(TokenType Type, string Lexeme)
        {
            _tokens.Add(new(Type, $"\"{Lexeme}\"", null, _line));
        }

        public void AddToken(TokenType Type, string Lexeme, object Literal)
        {
            _tokens.Add(new(Type, $"\"{Lexeme}\"", Literal, _line));
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
        /// Returns the next _current char (c)
        /// </summary>
        /// <returns></returns> Returns the next _current char (c)
        private char PeekNext()
        {
            return _code[_current + 1];
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
        /// Returns true if it is a number/letter/'_'
        /// </summary>
        /// <param name="c"></param> The character to check
        /// <returns></returns> Returns true if it is a number/letter/'_'
        private bool IsAlphaNumeric(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
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