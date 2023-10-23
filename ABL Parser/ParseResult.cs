namespace ABL_Parser
{
    public class ParseResult
    {
        public bool Success { get; set; }
        public ABLExpression Value { get; set; }

        public ParseResult(bool success, ABLExpression expr)
        {
            Success = success;
            Value = expr;
        }
    }
}