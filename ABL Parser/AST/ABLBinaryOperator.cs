namespace ABL_Parser
{
    public class ABLBinaryOperator : ABLExpression
    {
        public string Type { get; set; }
        public ABLExpression Alpha { get; private set; }
        public ABLExpression Omega { get; private set; }
        public ABLBinaryOperator(string type, ABLExpression alpha, ABLExpression omega)
        {
            Type = type;
            Alpha = alpha;
            Omega = omega;
        }

        public override string ToString()
        {
            return $"{Alpha} {Omega} {Type}";
        }
    }
}