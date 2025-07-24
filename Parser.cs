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

        private WriteStatement MakeWriteStatement()
        {
            return new WriteStatement(MakeSingleInputExpression());
        }

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
        private IExpression MakeSingleInputExpression()
        {
            Consume(TokenType.OpenParenthesis, $"Expected a: '(' but recieved: {Peek().Lexeme}");
            IExpression expression = ParseExpression();
            Consume(TokenType.CloseParenthesis, $"Expected a: ')' but recieved: {Peek().Lexeme}");
            return expression;
        }
        #endregion

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
            else
                throw new ParserException($"Unable to parse expression: {Peek()}");
        }

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