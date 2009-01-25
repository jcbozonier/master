using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlFuck
{
    public class TextReader
    {
        string _buffer;
        string _parseText;
        int _currentPosition;

        public string Buffer { get; set; }

        public TextReader(string text)
        {
            _buffer = String.Empty;
            _parseText = text;
            _currentPosition = 0;
        }

        /// <summary>
        /// This looks at the current position in _parseText
        /// but doesn't increment the position.
        /// </summary>
        /// <returns></returns>
        public char? Peek()
        {
            if (_parseText.Length >= _currentPosition)
                return _parseText[_currentPosition];
            return null;
        }

        public string GetNextCharacter()
        {
            var result = _parseText[_currentPosition].ToString();
            _currentPosition++;
            return result;
        }

        public bool IsEof()
        {
            return _currentPosition > _parseText.Length - 1;
        }
    }
}
