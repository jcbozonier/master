using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SqlFuck.Tokens;

namespace SqlFuck
{
    public class Scanner
    {
        private TextReader _reader;

        public Scanner(string text)
        {
            _reader = new TextReader(text);
        }      

        /// <summary>
        /// Method gets count number of tokens from scanner. NOT zero based.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Token GetNextToken(int count)
        {
            while (count-- >= 2)
            {
                GetNextToken();
            }

            return GetNextToken();
        }

        public Token GetNextToken()
        {
            while (!_reader.IsEof())
            {
                var character = _reader.GetNextCharacter();
                _reader.Buffer += character;
                if (_isToken(_reader.Buffer))
                {
                    var tokenText = _reader.Buffer;
                    _reader.Buffer = String.Empty;
                    
                    return _createToken(tokenText);
                }
            }

            return null;
        }

        private bool _isToken(string buffer)
        {
            if (_isEndOfStatementToken(buffer))
                return true;
            if (_isSetValueToken(buffer))
                return true;
            if (_isListItemSeparatorToken(buffer))
                return true;
            if (_isDataTypeToken(buffer))
                return true;
            if (_isListStartToken(buffer))
                return true;
            if (_isListEndToken(buffer))
                return true;
            if (_isChildAccessorToken(buffer))
                return true;
            if (_isValueToken(buffer))
                return true;
            if (_isVerbToken(buffer))
                return true;
            if (_isOptionToken(buffer))
                return true;
            return false;
        }

        private Token _createToken(string buffer)
        {
            if (_isEndOfStatementToken(buffer))
                return new EndOfStatementToken();
            if (_isSetValueToken(buffer))
                return new SetValueToken();
            if (_isListItemSeparatorToken(buffer))
                return new ListItemSeparatorToken();
            if (_isDataTypeToken(buffer))
                return new DataTypeToken(buffer.Trim().ToLowerInvariant());
            if (_isListStartToken(buffer))
                return new ListStartToken();
            if (_isListEndToken(buffer))
                return new ListEndToken();
            if (_isChildAccessorToken(buffer))
                return new ChildAccessorToken();
            if (_isValueToken(buffer))
                return new ValueToken(buffer.Trim().Replace("'", ""));
            if (_isVerbToken(buffer))
                return new VerbToken(buffer.Trim().ToLowerInvariant());
            if (_isOptionToken(buffer))
                return new OptionToken(buffer.Trim().ToLowerInvariant());

            throw new ArgumentException("Invalid attempt to create a token. We shouldn't have gotten here.");
        }

        private bool _isNextCharNonValueCharacter()
        {
            var peek = _reader.Peek();
            return peek == ' ' ||
                peek == ')' ||
                peek == '(' ||
                peek == '\n' ||
                peek == ',' ||
                peek == '=' ||
                peek == ';';
        }

        #region Token Discovery
        private bool _isEndOfStatementToken(string buffer)
        {
            return buffer.Trim() == ";";
        }

        private bool _isListItemSeparatorToken(string buffer)
        {
            return buffer.Trim() == ",";
        }

        private bool _isSetValueToken(string buffer)
        {
            return buffer.Trim() == "=";
        }

        private bool _isListEndToken(string buffer)
        {
            return buffer.Trim().StartsWith(")");
        }

        private bool _isOptionToken(string buffer)
        {
            // An option token is really any word that
            // isn't in quotes and it ends when we encounter a
            // non-value character.
            if (!buffer.Trim().StartsWith("'") &&
                _isNextCharNonValueCharacter() &&
                buffer.Trim() != "")
            {
                return true;
            }
            return false;
        }

        private bool _isDataTypeToken(string buffer)
        {
            switch (buffer.Trim().ToLowerInvariant())
            {
                case "bigint":
                    return true;
                default:
                    return false;
            }
        }

        private bool _isListStartToken(string buffer)
        {
            return buffer.Trim().StartsWith("(");
        }

        private bool _isChildAccessorToken(string buffer)
        {
            if (buffer.Trim() == ".")
                return true;
            return false;
        }

        private static bool _isVerbToken(string text)
        {
            return text.ToUpperInvariant() == "CREATE";
        }

        private bool _isValueToken(string text)
        {
            Int32 result;
            if (text.Trim().LastIndexOf("'") > 0 &&
               text.Trim().StartsWith("'"))
            {
                return true;
            }
            if (Int32.TryParse(text.Trim(), out result) &&
               _isNextCharNonValueCharacter())
            {
                return true;
            }

            return false;
        } 
        #endregion
    }
}
