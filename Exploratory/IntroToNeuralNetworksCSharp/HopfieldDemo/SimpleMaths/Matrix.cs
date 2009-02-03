using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaths
{
    public class Matrix
    {
        private double[][] _MatrixArray;

        public Matrix(double[][] matrixArray)
        {
            _MatrixArray = matrixArray;
        }

        internal Matrix Add(Matrix matrix)
        {
            var result = (double[][])_MatrixArray.Clone();
            for (var row = 0; row < result.Length; row++)
            {

                for (var col = 0; col < result[row].Length; col++)
                {
                    result[row][col] += matrix.GetValue(row, col);
                }
            }

            return new Matrix(result);
        }

        public double GetValue(int row, int col)
        {
            return _MatrixArray[row][col];
        }

        internal Matrix Multiply(Matrix matrix)
        {
            var result = (double[][])_MatrixArray.Clone();
            for (var row = 0; row < result.Length; row++)
            {

                for (var col = 0; col < result[row].Length; col++)
                {
                    result[row][col] *= matrix.GetValue(row, col);
                }
            }

            return new Matrix(result);
        }
    }
}
