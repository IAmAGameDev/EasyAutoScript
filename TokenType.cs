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

        Write,

        Var,

        // End Of File
        EOF,
    }
}