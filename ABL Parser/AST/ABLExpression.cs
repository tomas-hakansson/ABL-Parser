using System.Diagnostics.CodeAnalysis;

namespace ABL_Parser
{
    //sources:
    //  might contain correct information about BASIC operator associativity:
    //      http://www.vintage-basic.net/downloads/Vintage_BASIC_Users_Guide.html
    //  https://en.wikipedia.org/wiki/Shunting_yard_algorithm
    public abstract class ABLExpression
    {
        /// <summary>
        /// This method uses the shunting-yard algorithm to parse expressions.
        /// </summary>
        /// <param name="tokenizer"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ParseResult Create(Tokenizer tokenizer)
        {
            Stack<Token> ops = new();
            Stack<ABLExpression> output = new();

            Token? ct;
            while ((ct = tokenizer.NextInLine()) != null)
            {
                switch (ct.Type)
                {
                    case TokenType.Integer:
                        ABLInteger i = new(ct.Value);
                        output.Push(i);
                        break;
                    case TokenType.Operator:
                        while ((ops.TryPeek(out var oldOp) && oldOp.Value != "(") &&
                               (OP(oldOp) > OP(ct) ||
                                ((OP(oldOp) == OP(ct)) && OA(ct) == Associativity.Left)))
                        {
                            var omega = output.Pop();
                            var alpha = output.Pop();
                            output.Push(new ABLBinaryOperator(ops.Pop().Value, alpha, omega));
                        }
                        ops.Push(ct);
                        break;
                    case TokenType.OpeningParenthesis:
                        ops.Push(ct);
                        break;
                    case TokenType.ClosingParenthesis:
                        while (ops.Any() && ops.Peek().Type != TokenType.OpeningParenthesis)
                        {
                            var omega = output.Pop();
                            var alpha = output.Pop();
                            output.Push(new ABLBinaryOperator(ops.Pop().Value, alpha, omega));
                        }
                        if (ops.Any())
                        {
                            ops.Pop();
                        }
                        else
                        {
                            //error: mismatched parentheses.
                        }
                        break;
                    default:
                        break;
                }
            }

            foreach (var op in ops)
            {
                var omega = output.Pop();
                var alpha = output.Pop();
                output.Push(new ABLBinaryOperator(op.Value, alpha, omega));
            }

            return new(true, output.Pop());
        }

        /// <summary>
        /// Operator precedence.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        static int OP(Token op) => op.Value switch
        {
            "OR" => 0,
            "AND" => 1,
            "NOT" => 2,
            "+" or "-" => 4,
            "*" or "/" => 5,
            "_" => 6,//Negation.
            "^" => 7,
            "(" or ")" => 8,
            string when new[] { "=", "<>", "!=", "<", ">", "<=", ">=" }.Contains(op.Value) => 3,
            _ => throw new NotImplementedException("unimplemented operator")
        };

        /// <summary>
        /// Operator associativity.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        static Associativity OA(Token op) => op.Value switch
        {
            "NOT" or "_" => Associativity.NA,//'_' is negation
            "^" => Associativity.Right,
            _ => Associativity.Left,//Ponder: This might cause issues since this covers almost all strings.
        };

        enum Associativity
        {
            Left,
            Right,
            NA
        }
    }
}