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
        SetForegroundWindow,
        Sleep,
        Var,
        Write,

        // Expressions
        GetAllOpenWindowTitles,
        GetForegroundWindow,
        GetWindowTitle,

        // Values
        Boolean,
        Number,
        String,
    }
}