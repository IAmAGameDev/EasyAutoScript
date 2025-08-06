namespace EasyAutoScript
{
    public class Lexer(string source)
    {
        private readonly string _source = source;
        private int _current = 0;
        private int _line = 1;
        private readonly List<Token> _tokens = [];

        private readonly Dictionary<string, TokenType> identifiers = new()
        {
            { "Clear", TokenType.Clear },
            { "SetForegroundWindow", TokenType.SetForegroundWindow },
            { "Sleep", TokenType.Sleep },
            { "var", TokenType.Var },
            { "Write", TokenType.Write },

            { "GetAllOpenWindowTitles", TokenType.GetAllOpenWindowTitles },
            { "GetForegroundWindow", TokenType.GetForegroundWindow },
            { "GetWindowTitle", TokenType.GetWindowTitle },

            { "MouseGetPosition", TokenType.MouseGetPosition },
            { "MouseSetPositionRelative", TokenType.MouseSetPositionRelative },

            { "true", TokenType.Boolean },
            { "false", TokenType.Boolean },
        };

        /// <summary>
        /// Tokenises letters/words from a code input
        /// </summary>
        public List<Token> Tokenise()
        {
            while (!IsAtEnd())
            {
                char c = Advance();
                switch (c)
                {
                    // Parenthesis
                    case '(':
                        AddToken(TokenType.OpenParenthesis, "(");
                        break;
                    case ')':
                        AddToken(TokenType.CloseParenthesis, ")");
                        break;

                    // Equals
                    case '=':
                        AddToken(TokenType.Equals, "=");
                        break;

                    // Comma
                    case ',':
                        AddToken(TokenType.Comma, ",");
                        break;

                    // Comment
                    case '/':
                        Comment();
                        break;


                    // Increase line counter
                    case '\n':
                        _line++;
                        break;

                    // Skip whitespace
                    case '\r':
                    case ' ':
                    case '\t':
                        break;

                    // String
                    case '"':
                        String();
                        break;

                    default:
                        // Number
                        if ((c == '-' && char.IsNumber(PeekNext())) || char.IsNumber(c))
                        {
                            Number();
                            break;
                        }
                        // Identifier
                        else if (char.IsLetterOrDigit(c))
                        {
                            Identifier();
                            break;
                        }

                        // Error
                        throw new LexerException($"Unknown character entered: {c} on line: {_line}");
                }
            }

            return _tokens;
        }

        private void Comment()
        {
            if (Peek() == '/')
            {
                Advance();
            }
            while (!IsAtEnd() && Peek() != '\n')
            {
                Advance();
            }
        }

        /// <summary>
        /// Creates a number, checking if it is a decimal, then adding a token Number with that value
        /// </summary>
        private void Number()
        {
            int start = _current - 1;

            while (!IsAtEnd() && char.IsDigit(Peek()))
            {
                Advance();
            }
            if (Peek() == '.')
            {
                Advance();
            }
            while (!IsAtEnd() && char.IsDigit(Peek()))
            {
                Advance();
            }

            string lexeme = _source[start.._current];

            AddToken(TokenType.Number, lexeme, Convert.ToDouble(lexeme));
        }

        /// <summary>
        /// Creates a string like "hello" by advancing until after the last " then creating a substring
        /// </summary>
        private void String()
        {
            int start = _current;

            while (!IsAtEnd() && Peek() != '"')
            {
                Advance();
            }

            string lexeme = _source[start.._current];

            Advance();

            AddToken(TokenType.String, lexeme, lexeme);
        }

        /// <summary>
        /// Creates an identifier if it wasn't a string to check if it is a keyword like Write
        /// </summary>
        private void Identifier()
        {
            int start = _current - 1;

            while (!IsAtEnd() && (char.IsLetterOrDigit(Peek()) || Peek() == '_'))
            {
                Advance();
            }

            string identifier = _source[start.._current];

            if (identifiers.TryGetValue(identifier, out TokenType type))
            {
                if (type == TokenType.Boolean)
                {
                    AddToken(type, identifier, Convert.ToBoolean(identifier));
                    return;
                }
                AddToken(type, identifier);
                return;
            }
            AddToken(TokenType.Identifier, identifier);
        }

        /// <summary>
        /// Adds a token to the _tokens list (without a value)
        /// </summary>
        private void AddToken(TokenType type, string lexeme)
        {
            _tokens.Add(new Token(type, $"\"{lexeme}\"", null, _line));
        }
        /// <summary>
        /// Adds a token to the _tokens list (with a value)
        /// </summary>
        private void AddToken(TokenType type, string lexeme, object literal)
        {
            _tokens.Add(new Token(type, $"\"{lexeme}\"", literal, _line));
        }

        /// <summary>
        /// Return the current char c
        /// </summary>
        /// <returns></returns> Return the current char c
        private char Peek()
        {
            return _source[_current];
        }

        /// <summary>
        /// Return the next char c
        /// </summary>
        /// <returns></returns> Return the next char c
        private char PeekNext()
        {
            return _source[_current + 1];
        }

        /// <summary>
        /// Return the current char c then advance the _current by 1
        /// </summary>
        /// <returns></returns> Return the current char c
        private char Advance()
        {
            char c = _source[_current];
            _current++;
            return c;
        }

        /// <summary>
        /// Return if we are the end of the file
        /// </summary>
        /// <returns></returns> Bool true or false for if at end
        private bool IsAtEnd()
        {
            return _current >= _source.Length;
        }
    }
}