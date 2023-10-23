namespace ABL_Parser
{
    internal class ABLInteger : ABLExpression
    {
        public string Value { get; private set; }
        public ABLInteger(string value)
        {
            Value = value;
        }
    }
}