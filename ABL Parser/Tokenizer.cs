namespace ABL_Parser;

public class Tokenizer
{
    string _source;
    int _cursor;

    public Tokenizer(string source)
    {
        _source = source;
        _cursor = 0;

        CleanUpWhiteSpace();
    }

    public Token? NextInLine()
    {
        var token = string.Empty;

        for (; _cursor < _source.Length; _cursor++)
        {
            var cc = _source[_cursor];
            if (char.IsWhiteSpace(cc))
                break;
            else
            {
                token += cc;
            }
        }

        CleanUpWhiteSpace();

        return string.IsNullOrWhiteSpace(token) ? null : new(token, global::ABL_Parser.Tokenizer.GetType(token));
    }

    void CleanUpWhiteSpace()
    {
        while (_cursor < _source.Length && char.IsWhiteSpace(_source[_cursor]))
            _cursor++;
    }

    static TokenType GetType(string token)
    {
        TokenType type;
        if (token.All(c => char.IsDigit(c)))
            type = TokenType.Integer;
        else if ("+*".Contains(token))
            type = TokenType.Operator;
        else
            type = TokenType.Placeholder;
        return type;
    }
}
