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
            for (var i = 0; i < pattern.Length; i++ )
            {
                pattern[i] = (pattern[i] <= 0) ? -1 : 1;
            }

            _Pattern = pattern;
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
                    result[row][col] = (row == col) 
                        ? 0 
                        : _Pattern[row]*_Pattern[col];
                }
            }

            return result;
        }

        internal double[] GetArray()
        {
            return _Pattern;
        }

        public override bool Equals(object obj)
        {
            var pattern = obj as HopfieldPattern;
            if(pattern == null) throw new ArgumentException("Wrong type.");

            var equal = true;

            for (var i = 0; i < GetArray().Length; i++ )
            {
                equal &= _Pattern[i] == pattern.GetArray()[i];
            }

            return equal;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            
            foreach(var number in _Pattern)
            {
                result.Append(number.ToString() + ",");
            }

            return result.ToString().Remove(result.Length-1,1);
        }
    }
}
