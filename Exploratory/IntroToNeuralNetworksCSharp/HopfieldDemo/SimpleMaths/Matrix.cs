using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaths
{
    /// <summary>
    /// Very limited matrix class.
    /// </summary>
    public class Matrix
    {
        private double[][] _MatrixArray;

        public Matrix(double[][] matrixArray)
        {
            _MatrixArray = matrixArray;
        }

        /// <summary>
        /// Adds two matrices together. No bounds checking. I just wanted the simplest
        /// thing that would work.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a value from the matrix. Used to decouple
        /// the internal matrix data structure (ie. array, etc) from
        /// external clients.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public double GetValue(int row, int col)
        {
            return _MatrixArray[row][col];
        }

        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
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

        internal static Matrix CreateEmptySquareMatrix(int rowColumnCount)
        {
            double[][] matrix = _GetEmptyMatrix(rowColumnCount);
            return new Matrix(matrix);
        }

        private static double[][] _GetEmptyMatrix(int nodeCount)
        {
            var matrix = new double[nodeCount][];

            for (var row = 0; row < nodeCount; row++)
            {
                matrix[row] = new double[nodeCount];

                for (var col = 0; col < nodeCount; col++)
                {
                    matrix[row][col] = -1.0;
                }
            }
            return matrix;
        }
    }
}
