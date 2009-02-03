using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaths
{
    public class HopfieldPattern
    {
        private double[] _Pattern;

        public HopfieldPattern(params double[] pattern)
        {   
            // Convert the pattern to bipolar form upon
            // instantiation since we don't ever care
            // about the boolean form.
            for (var i = 0; i < pattern.Length; i++ )
            {
                pattern[i] = (pattern[i] <= 0) ? -1 : 1;
            }

            _Pattern = pattern;
        }

        public int Length
        {
            get { return _Pattern.Length; }
        }

        /// <summary>
        /// Converts the pattern to a weight matrix that can be used as a contribution matrix.
        /// </summary>
        /// <returns></returns>
        internal double[][] GetWeightMatrix()
        {
            var result = new double[_Pattern.Length][];

            for (var row = 0; row < _Pattern.Length; row++ )
            {
                result[row] = new double[_Pattern.Length];

                for(var col=0; col < _Pattern.Length; col++)
                {
                    // We need to place zeroes in a diagnol line down and
                    // to the right through the matrix.
                    result[row][col] = (row == col) 
                        ? 0 
                        : _Pattern[row] * _Pattern[col];
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the pattern from the object as an array of doubles.
        /// </summary>
        /// <returns></returns>
        internal double[] GetPattern()
        {
            return _Pattern;
        }

        internal double this[int i]
        {
            get
            {
                // Not sure if it's bad that I am normalizing the
                // pattern value here. For now it makes the tests
                // pass though! :)
                return _Pattern[i] <= 0 ? -1 : 1;
            }
            set
            {
                _Pattern[i] = value;
            }
        }

        public override bool Equals(object obj)
        {
            var pattern = obj as HopfieldPattern;
            if(pattern == null) throw new ArgumentException("Wrong type.");

            var equal = true;

            for (var i = 0; i < GetPattern().Length; i++ )
            {
                equal &= _Pattern[i] == pattern[i];
            }

            return equal;
        }

        /// <summary>
        /// Useful for debugging purposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = new StringBuilder();
            
            foreach(var number in _Pattern)
            {
                result.Append(number.ToString() + ",");
            }

            return result.ToString().Remove(result.Length-1,1);
        }

        public static HopfieldPattern CreateEmptyPattern(int length)
        {
            return new HopfieldPattern(new double[length]);
        }
    }
}
