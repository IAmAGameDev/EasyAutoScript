using EasyAutoScript.Expressions;
using EasyAutoScript.Statements;

namespace EasyAutoScript
{
    public class Parser(List<Token> tokens)
    {
        private int _current = 0;
        private readonly List<IStatement> statements = [];

        /// <summary>
        /// Parses the tokens into IStatements for the Interpreter to use
        /// </summary>
        public List<IStatement> ParseTokens()
        {
            while (!IsAtEnd())
            {
                statements.Add(ParseStatement());
            }

            return statements;
        }

        /// <summary>
        /// Creates a IStatement to be added with a optional/needed IExpression if needed
        /// </summary>
        /// <returns></returns> Returns the newly created IStatement
        /// <exception cref="Exception"></exception> Throws an error if encountering a unexprected Token
        private IStatement ParseStatement()
        {
            Token current = Advance();

            if (Peek().Type != TokenType.OpenParenthesis)
            {
                if (current.Type == TokenType.Var)
                {
                    string name = Advance().Lexeme;
                    Consume(TokenType.Equals, $"Expected a \"=\" but recieved: {tokens[_current]}");
                    return new VarStatement(name, ParseExpression());
                }
                else if (current.Type == TokenType.Identifier)
                {
                    string name = current.Lexeme;
                    Consume(TokenType.Equals, $"Expected a \"=\" but recieved: {tokens[_current]}");
                    return new VarAssignStatement(name, ParseExpression());
                }
                else
                {
                    throw new Exception($"Unexpected token encountered: {Peek()}");
                }
            }

            Consume(TokenType.OpenParenthesis, $"Expected a \"(\" but recieved: {tokens[_current]}");

            IStatement statement;
            switch (current.Type)
            {
                case TokenType.Clear:
                    statement = new ClearStatement();
                    break;
                case TokenType.MouseSetPosition:
                    {
                        List<IExpression?> expressions = ParseMultiExpression(3);
                        statement = new MouseSetPositionStatement(expressions[0]!, expressions[1]!, expressions[2]);
                        break;
                    }
                case TokenType.MouseSetPositionRelative:
                    {
                        List<IExpression?> expressions = ParseMultiExpression(2);
                        statement = new MouseSetPositionRelativeStatement(expressions[0]!, expressions[1]!);
                        break;
                    }
                case TokenType.SetForegroundWindow:
                    {
                        var expression = ParseExpression() ?? throw new ParserException("SetForegroundWindowStatement requires a non-null expression.");
                        statement = new SetForegroundWindowStatement(expression);
                        break;
                    }
                case TokenType.Sleep:
                    {
                        var expression = ParseExpression() ?? throw new ParserException("SetForegroundWindowStatement requires a non-null expression.");
                        statement = new SleepStatement(expression);
                        break;
                    }
                case TokenType.Write:
                    {
                        var expression = ParseExpression() ?? throw new ParserException("SetForegroundWindowStatement requires a non-null expression.");
                        statement = new WriteStatement(expression);
                        break;
                    }

                default:
                    throw new ParserException($"Trying to parse an unexpected/unhandled token: {current}");
            }
            Consume(TokenType.CloseParenthesis, $"Expected a \")\" but recieved: {tokens[_current]}");
            return statement;
        }

        private List<IExpression?> ParseMultiExpression(int expectedInputs)
        {
            List<IExpression?> expressions = [];
            expressions.Add(ParseExpression());

            while (!IsAtEnd() && Peek().Type != TokenType.CloseParenthesis)
            {
                Consume(TokenType.Comma, $"Expected a \",\" but recieved: {tokens[_current]}");
                expressions.Add(ParseOptionalExpression());
            }

            if (expressions.Count > expectedInputs)
            {
                throw new ParserException($"Expected {expectedInputs} inputs but recieved {expressions.Count}");
            }

            while (expressions.Count != expectedInputs)
            {
                expressions.Add(new EmptyExpression());
            }

            return expressions;
        }

        private IExpression? ParseOptionalExpression()
        {
            if (Peek().Type == TokenType.CloseParenthesis)
            {
                return null;
            }
            IExpression expression = ParseExpression();
            return expression;
        }

        /// <summary>
        /// Makes the Expression like a Number value from a TokenType
        /// </summary>
        /// <returns></returns> Returns the new IExpression
        /// <exception cref="Exception"></exception> Throws an error if unhandled or incorrect TokenType
        private IExpression ParseExpression()
        {
            Token token = Advance();
            switch (token.Type)
            {
                case TokenType.Boolean:
                    return new BooleanLiteralExpression(Convert.ToBoolean(token.Literal));
                case TokenType.Number:
                    return new NumberLiteralExpression(Convert.ToDouble(token.Literal));
                case TokenType.String:
                    return new StringLiteralExpression(Convert.ToString(token.Literal) ?? string.Empty);

                case TokenType.Identifier:
                    return new IdentifierExpression(token.Lexeme);

                case TokenType.GetAllOpenWindowTitles:
                    {
                        Consume(TokenType.OpenParenthesis, $"Expected a \"(\" but recieved: {tokens[_current]}");
                        GetAllOpenWindowTitlesExpression expression = new(ParseOptionalExpression());
                        Consume(TokenType.CloseParenthesis, $"Expected a \")\" but recieved: {tokens[_current]}");
                        return expression;
                    }
                case TokenType.GetForegroundWindow:
                    Consume(TokenType.OpenParenthesis, $"Expected a \"(\" but recieved: {tokens[_current]}");
                    Consume(TokenType.CloseParenthesis, $"Expected a \")\" but recieved: {tokens[_current]}");
                    return new GetForegroundWindowExpression();
                case TokenType.GetWindowTitle:
                    {
                        Consume(TokenType.OpenParenthesis, $"Expected a \"(\" but recieved: {tokens[_current]}");
                        GetWindowTitleExpression expression = new(ParseOptionalExpression());
                        Consume(TokenType.CloseParenthesis, $"Expected a \")\" but recieved: {tokens[_current]}");
                        return expression;
                    }

                case TokenType.MouseGetPosition:
                    {
                        Consume(TokenType.OpenParenthesis, $"Expected a \"(\" but recieved: {tokens[_current]}");
                        MouseGetPositionExpression expression = new(ParseOptionalExpression());
                        Consume(TokenType.CloseParenthesis, $"Expected a \")\" but recieved: {tokens[_current]}");
                        return expression;
                    }

                default:
                    throw new ParserException($"Unexpected token recieved expected a value, recieved: {token}");

            }
        }

        /// <summary>
        /// Checks if the current token is a what I want it to be if so advance otherwise log a user error
        /// </summary>
        /// <param name="type"></param> The TokenType I am checking against
        /// <param name="message"></param> The message to display in case of an error
        private void Consume(TokenType type, string message)
        {
            if (type == Peek().Type)
            {
                Advance();
                return;
            }
            throw new ParserException(message);
        }

        /// <summary>
        /// Check if the current token is what I want it to be if so Advance
        /// </summary>
        /// <param name="type"></param> The TokenType I am checking against
        /// <returns></returns> Boolean value for if it was a match or not
        private bool Match(TokenType type)
        {
            Token token = Peek();
            if (type == token.Type)
            {
                Advance();
            }
            return type == token.Type;
        }

        /// <summary>
        /// Return the current Token c
        /// </summary>
        /// <returns></returns> Return the current Token c
        private Token Peek()
        {
            return tokens[_current];
        }

        /// <summary>
        /// Return the current Token c then advance the _current by 1
        /// </summary>
        /// <returns></returns> Return the current Token c
        private Token Advance()
        {
            Token c = tokens[_current];
            _current++;
            return c;
        }

        /// <summary>
        /// Return if we are the end of the the tokens
        /// </summary>
        /// <returns></returns> Bool true or false for if at end
        private bool IsAtEnd()
        {
            return _current >= tokens.Count;
        }
    }
}