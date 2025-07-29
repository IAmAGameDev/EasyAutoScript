namespace EasyAutoScript
{
    public enum TokenType
    {
        // Values/Literals
        Boolean,
        String,
        Number,

        // Parenthesis / Equals / ExclamationMark
        OpenParenthesis,
        CloseParenthesis,
        Comma,
        Comment,
        Equals,
        ExclamationMark,
        // Statements / Identifiers / Keywords
        Identifier,

        Clear,
        Sleep,
        Write,

        Var,

        // Expressions
        GetAllOpenWindowTitles,
        GetForegroundWindow,
        GetOpenWindowTitle,

        // End Of File
        EOF,
    }
}