using ABL_Parser;

namespace ABL_Parser_Tests;

public class TokenizerTests
{
    [Fact]
    public void CanTokenizeText()
    {
        var source = "some text";
        Tokenizer tokenizer = new(source);

        Assert.Equal("some", tokenizer.NextInLine()?.Value);
        Assert.Equal("text", tokenizer.NextInLine()?.Value);
    }

    [Fact]
    public void CanTokenizeEmptyText()
    {
        var source = string.Empty;
        Tokenizer tokenizer = new(source);

        Assert.Null(tokenizer.NextInLine());
    }

    [Fact]
    public void CanTokenizeNumber()
    {
        var source = "42";
        Tokenizer tokenizer = new(source);
        var token = tokenizer.NextInLine();

        if (token == null)
            Assert.Fail("should never happen");
        else
        {
            Assert.Equal("42", token.Value);
            Assert.Equal(TokenType.Integer, token.Type);
        }
    }

    [Fact]
    public void CanTokenizeOperator()
    {
        var source = "+";
        Tokenizer tokenizer = new(source);
        var token = tokenizer.NextInLine();

        if (token == null)
            Assert.Fail("should never happen");
        else
        {
            Assert.Equal("+", token.Value);
            Assert.Equal(TokenType.Operator, token.Type);
        }
    }
}