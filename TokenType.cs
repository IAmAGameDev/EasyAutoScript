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

        // Statements
        Clear,
        Sleep,
        Var,
        Write,

        // Expressions
        GetForegroundWindow,

        // Values
        Boolean,
        Number,
        String,
    }
}