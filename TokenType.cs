namespace EasyAutoScript
{
    public enum TokenType
    {
        // Values/Literals
        Boolean,
        String,
        Number,

        // Parenthesis / Equals
        OpenParenthesis,
        CloseParenthesis,
        Equals,

        // Statements / Identifiers / Keywords
        Identifier,

        Clear,
        Write,

        Var,

        // End Of File
        EOF,
    }
}