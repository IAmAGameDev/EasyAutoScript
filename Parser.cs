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
                statements.Add(ScanToken());
            }

            return statements;
        }

        /// <summary>
        /// Creates a IStatement to be added with a optional/needed IExpression if needed
        /// </summary>
        /// <returns></returns> Returns the newly created IStatement
        /// <exception cref="Exception"></exception> Throws an error if encountering a unexprected Token
        private IStatement ScanToken()
        {
            if (Match(TokenType.Clear))
            {
                ParseEmptyExpression();
                return new ClearStatement();
            }
            else if (Match(TokenType.Sleep))
            {
                return new SleepStatement(ParseVariableExpression());
            }
            else if (Match(TokenType.Write))
            {
                return new WriteStatement(ParseVariableExpression());
            }
            else
            {
                throw new ParserException($"END: {tokens[_current]}");
            }
        }

        private void ParseEmptyExpression()
        {
            Consume(TokenType.OpenParenthesis, $"Expected a \"(\" but recieved: {tokens[_current]}");
            Consume(TokenType.CloseParenthesis, $"Expected a \")\" but recieved: {tokens[_current]}");
        }

        private IExpression ParseVariableExpression()
        {
            Consume(TokenType.OpenParenthesis, $"Expected a \"(\" but recieved: {tokens[_current]}");
            IExpression expression = ParseExpression();
            Consume(TokenType.CloseParenthesis, $"Expected a \")\" but recieved: {tokens[_current]}");
            return expression;
        }

        /// <summary>
        /// Makes the Expression like a Number value from a TokenType
        /// </summary>
        /// <returns></returns> Returns the new IExpression
        /// <exception cref="Exception"></exception> Throws an error if unhandled or incorrect TokenType
        private IExpression ParseExpression()
        {
            Token token = tokens[_current];
            Advance();
            return token.Type switch
            {
                TokenType.Boolean => new BooleanLiteralExpression(Convert.ToBoolean(token.Literal)),
                TokenType.Number => new NumberLiteralExpression(Convert.ToDouble(token.Literal)),
                TokenType.String => new StringLiteralExpression(Convert.ToString(token.Literal) ?? string.Empty),
                _ => throw new ParserException($"Unexpected token recieved expected a value recieved: {token}")
            };
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