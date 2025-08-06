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
        MouseGetPosition,

        // Values
        Boolean,
        Number,
        String,
    }
}