namespace EasyAutoScript.Exceptions
{
    public class LexerException(string message) : Exception(message) { }
    public class ParserException(string message) : Exception(message) { }
    public class InterpreterException(string message) : Exception(message) { }
}