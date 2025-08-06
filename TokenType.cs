namespace EasyAutoScript
{
    public enum TokenType
    {
        OpenParenthesis,
        CloseParenthesis,

        Equals,
        ForwardSlash,
        Comma,

        // Identifier
        Identifier,

        // Values
        Boolean,
        Number,
        String,

        // Statements
        Clear,
        Sleep,
        Var,
        Write,

        // Expressions
    }
}