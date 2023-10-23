using ABL_Parser;

namespace ABL_Parser_Tests;

public class ExpressionTests
{
    [Fact]
    public void CanParseExpression()
    {
        var source = "2 + 3 * 4";
        Tokenizer tokenizer = new(source);
        ParseResult parseResult = ABLExpression.Create(tokenizer);

        Assert.True(parseResult.Success);
        Assert.IsType<ABLBinaryOperator>(parseResult.Value);
    }
}
