namespace EasyAutoScript
{
    public enum TokenType
    {
        OpenParenthesis,
        CloseParenthesis,

        ForwardSlash,

        // Identifier
        Identifier,

        // Values
        Boolean,
        Number,
        String,

        // Statements
        Sleep,
        Write,

        // Expressions
    }
}