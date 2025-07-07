namespace EasyAutoScript
{
    public enum TokenType
    {
        // Values/Literals
        Boolean,
        String,
        Number,

        // Parenthesis
        OpenParenthesis,
        CloseParenthesis,

        // Statements
        Write,

        // End Of File
        EOF,
    }
}