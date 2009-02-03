using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaths
{
    public class HopfieldPattern
    {
        private double[] _MatrixArray;

        public HopfieldPattern(double[] matrixArray)
        {
            for (var i = 0; i < matrixArray.Length; i++ )
            {
                matrixArray[i] = (matrixArray[i] <= 0) ? -1 : 1;
            }

            _MatrixArray = matrixArray;
        }

        internal double[][] GetWeightMatrix()
        {
            var result = new double[_MatrixArray.Length][];

            for (var row = 0; row < _MatrixArray.Length; row++ )
            {
                result[row] = new double[_MatrixArray.Length];
                for(var col=0; col<_MatrixArray.Length; col++)
                {

                    result[row][col] = (row == col) 
                        ? 0 
                        : _MatrixArray[row]*_MatrixArray[col];
                }
            }

            return result;
        }

        internal double[] GetArray()
        {
            return _MatrixArray;
        }

        public override bool Equals(object obj)
        {
            var pattern = obj as HopfieldPattern;
            if(pattern == null) throw new ArgumentException("Wrong type.");

            var equal = true;

            for (var i = 0; i < GetArray().Length; i++ )
            {
                equal &= _MatrixArray[i] == pattern.GetArray()[i];
            }

            return equal;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            
            foreach(var number in _MatrixArray)
            {
                result.Append(number.ToString() + ",");
            }

            return result.ToString().Remove(result.Length-1,1);
        }
    }
}
