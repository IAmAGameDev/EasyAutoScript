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
        Comment,
        Equals,
        ExclamationMark,
        // Statements / Identifiers / Keywords
        Identifier,

        Clear,
        Sleep,
        Write,

        Var,

        // End Of File
        EOF,
    }
}