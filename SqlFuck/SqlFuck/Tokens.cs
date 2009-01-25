using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlFuck.Tokens
{
    public class EndOfStatementToken : Token
    {

    }

    public class SetValueToken : Token
    {

    }

    public class VerbToken : Token
    {
        public string Value { get; set; }
        public VerbToken(string value)
        {
            Value = value;
        }
    }

    public class ListItemSeparatorToken : Token
    {

    }

    public class OptionToken : Token
    {
        public string Value { get; set; }
        public OptionToken(string value)
        {
            Value = value;
        }
    }

    public class DataTypeToken : Token
    {
        public string Value { get; set; }

        public DataTypeToken(string value)
        {
            Value = value;
        }
    }

    public class ListStartToken : Token
    {

    }

    public class ListEndToken : Token
    {

    }

    public class ChildAccessorToken : Token
    {

    }

    public class ValueToken : Token
    {
        public string Value { get; set; }
        public ValueToken(string text)
        {
            Value = text;
        }
    }

    public class CreateTableToken : Token
    {

    }

    public class Token
    {

    }
}
