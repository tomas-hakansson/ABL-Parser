namespace ABL_Parser
{
    public enum TokenType
    {
        Placeholder,
        Integer,
        Operator,
    }

    public class Token
    {
        public string Value { get; private set; }
        public TokenType Type { get; private set; }

        public Token(string token, TokenType type)
        {
            Value = token;
            Type = type;
        }

    }
}