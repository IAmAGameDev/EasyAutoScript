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
        Equals,
        ExclamationMark,

        // Statements / Identifiers / Keywords
        Identifier,

        Clear,
        Write,

        Var,

        // End Of File
        EOF,
    }
}