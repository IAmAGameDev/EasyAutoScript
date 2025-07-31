using EasyAutoScript.Exceptions;
using EasyAutoScript.Expressions;
using EasyAutoScript.Statements;

namespace EasyAutoScript
{
    public class Parser(List<Token> tokens)
    {
        private readonly List<Token> _tokens = tokens;
        private int _current = 0;

        public List<IStatement> Parse()
        {
            List<IStatement> _statements = [];

            while (!IsAtEnd())
            {
                _statements.Add(MakeStatement());
            }

            return _statements;
        }

        private IStatement MakeStatement()
        {
            if (Match(TokenType.Clear))
                return MakeClearStatement();
            if (Match(TokenType.SetForegroundWindow))
                return MakeSetForegroundWindow();
            else if (Match(TokenType.Sleep))
                return MakeSleepStatement();
            else if (Match(TokenType.Write))
                return MakeWriteStatement();
            else if (Match(TokenType.Var))
                return MakeVarStatement();
            else if (Check(TokenType.Identifier))
                return MakeVarAssignStatement();
            else
                throw new ParserException($"Unable to parse {_tokens[_current]}");
        }

        #region Statements
        private ClearStatement MakeClearStatement()
        {
            MakeEmptyExpression();
            return new ClearStatement();
        }

        private SetForegroundWindowStatement MakeSetForegroundWindow()
        {
            IExpression[] expressions = MakeInputExpressions(1);
            return new SetForegroundWindowStatement(expressions[0]);
        }

        private SleepStatement MakeSleepStatement()
        {
            IExpression[] expressions = MakeInputExpressions(1);
            return new SleepStatement(expressions[0]);
        }

        private WriteStatement MakeWriteStatement()
        {
            IExpression[] expressions = MakeInputExpressions(1);
            return new WriteStatement(expressions[0]);
        }
        #endregion

        #region VarStatements
        private VarStatement MakeVarStatement()
        {
            string name = Consume(TokenType.Identifier, $"Expected a: {{Name}} but recieved: {Peek().Lexeme}").Lexeme;
            Consume(TokenType.Equals, $"Expected a: '=' but recieved: {Peek().Lexeme}");
            return new VarStatement(name, ParseExpression());
        }

        private VarAssignStatement MakeVarAssignStatement()
        {
            string name = Consume(TokenType.Identifier, $"Expected a: {{Name}} but recieved: {Peek().Lexeme}").Lexeme;
            Consume(TokenType.Equals, $"Expected a: '=' but recieved: {Peek().Lexeme}");
            return new VarAssignStatement(name, ParseExpression());
        }
        #endregion

        #region Expressions
        private void MakeEmptyExpression()
        {
            Consume(TokenType.OpenParenthesis, $"Expected a: '(' but recieved: {Peek().Lexeme}");
            Consume(TokenType.CloseParenthesis, $"Expected a: ')' but recieved: {Peek().Lexeme}");
        }
        private IExpression[] MakeInputExpressions(int expectedInputs)
        {
            Consume(TokenType.OpenParenthesis, $"Expected a: '(' but recieved: {Peek().Lexeme}");

            List<IExpression> expressions = [];
            expressions.Add(ParseExpression());

            while (!Check(TokenType.CloseParenthesis) && !IsAtEnd())
            {
                Consume(TokenType.Comma, $"Expected a: ',' but recieved: {Peek().Lexeme}");
                expressions.Add(ParseExpression());
            }

            Consume(TokenType.CloseParenthesis, $"Expected a: ')' but recieved: {Peek().Lexeme}");

            if (expressions.Count != expectedInputs)
            {
                throw new ParserException($"Expected {expectedInputs} input(s) but recieved {expressions.Count} variables, on line: {_tokens[_current].Line}");
            }

            return [.. expressions];
        }
        #endregion

        #region ParseExpression
        private IExpression ParseExpression()
        {
            if (Check(TokenType.Boolean))
                return new BooleanLiteralExpression(Convert.ToBoolean(Advance().Literal));
            else if (Check(TokenType.Number))
                return new NumberLiteralExpression(Convert.ToDouble(Advance().Literal));
            else if (Check(TokenType.String))
            {
                string value = Convert.ToString(Advance().Literal) ?? string.Empty;
                value = value[1..^1];
                return new StringLiteralExpression(value);
            }
            else if (Match(TokenType.ExclamationMark))
            {
                bool value = !Convert.ToBoolean(Consume(TokenType.Boolean, $"Expected a: 'boolean' but recieved: {Peek().Lexeme}").Literal);
                return new BooleanLiteralExpression(value);
            }
            else if (Check(TokenType.Identifier))
            {
                string name = Convert.ToString(Advance().Lexeme);
                return new IdentifierExpression(name);
            }
            else if (Match(TokenType.GetForegroundWindow))
            {
                MakeEmptyExpression();
                return new GetForegroundWindowExpression();
            }
            else if (Match(TokenType.GetOpenWindowTitle))
            {
                MakeEmptyExpression();
                return new GetOpenWindowTitleExpression();
            }
            else if (Match(TokenType.GetAllOpenWindowTitles))
            {
                if (PeekNext().Type != TokenType.CloseParenthesis)
                {
                    IExpression[] expressions = MakeInputExpressions(1);
                    if (expressions[0] is BooleanLiteralExpression booleanLiteralExpression)
                    {
                        return new GetAllOpenWindowTitlesExpression(booleanLiteralExpression.value);
                    }
                    else if (expressions[0] == null)
                    {
                        return new GetAllOpenWindowTitlesExpression(false);
                    }
                    throw new ParserException($"Expected a: 'boolean' but recieved: {Peek().Lexeme}");
                }
                MakeEmptyExpression();
                return new GetAllOpenWindowTitlesExpression(false);
            }
            else
            {
                throw new ParserException($"Unable to parse expression: {Peek()}");
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Checks if the ScanStatement Token matches with this given token
        /// </summary>
        /// <param name="Type"></param> The TokenType to check for
        /// <returns></returns> Boolean value for if a Check(Type) matches
        private bool Match(TokenType[] Types)
        {
            if (Types.Contains(Peek().Type))
            {
                Advance();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the ScanStatement Token matches with this given token
        /// </summary>
        /// <param name="Type"></param> The TokenType to check for
        /// <returns></returns> Boolean value for if a Check(Type) matches
        private bool Match(TokenType Type)
        {
            if (Check(Type))
            {
                Advance();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is this token of this type?
        /// </summary>
        /// <param name="Type"></param> The type we are checking against a Peek()
        /// <returns></returns> Yes the token matches the Peek()
        private bool Check(List<TokenType> Type)
        {
            return Type.Contains(Peek().Type);
        }
        /// <summary>
        /// Is this token of this type?
        /// </summary>
        /// <param name="Type"></param> The type we are checking against a Peek()
        /// <returns></returns> Yes the token matches the Peek()
        private bool Check(TokenType Type)
        {
            return Peek().Type == Type;
        }

        /// <summary>
        /// Method to advance the current token and return it if needed.
        /// </summary>
        /// <returns></returns> Returns the current token then increases _current by 1
        private Token Advance()
        {
            Token token = _tokens[_current];
            _current++;
            return token;
        }

        /// <summary>
        /// Returns the _current token
        /// </summary>
        /// <returns></returns> Returns the _current token
        private Token Peek()
        {
            return _tokens[_current];
        }

        /// <summary>
        /// Returns the _current token + 1
        /// </summary>
        /// <returns></returns> Returns the _current token + 1
        private Token PeekNext()
        {
            return _tokens[_current + 1];
        }

        /// <summary>
        /// Returns the _current token Then Advances _current + 1
        /// </summary>
        /// <returns></returns> Returns the _current token
        public Token Consume(TokenType type, string message)
        {
            if (Match(type))
            {
                return _tokens[_current - 1];
            }
            throw new ParserException(message);
        }

        private bool IsAtEnd()
        {
            return _current >= _tokens.Count - 1;
        }
        #endregion
    }
}