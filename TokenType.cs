namespace EasyAutoScript
{
    public enum TokenType
    {
        OpenParenthesis,
        CloseParenthesis,

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
        Write,

        // Expressions
    }
}