using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaths
{
    public class HopfieldNetwork
    {
        private Matrix _WeightMatrix;

        public HopfieldNetwork(int nodeCount)
        {
            double[][] matrix = _GetEmptyMatrix(nodeCount);
            _WeightMatrix = new Matrix(matrix);
        }

        private double[][] _GetEmptyMatrix(int nodeCount)
        {
            var matrix = new double[nodeCount][];

            for (var row = 0; row < nodeCount; row++)
            {
                matrix[row] = new double[nodeCount];

                for(var col=0; col<nodeCount;col++)
                {
                    matrix[row][col] = -1.0;
                }
            }
            return matrix;
        }

        public void Train(HopfieldPattern pattern)
        {
            _WeightMatrix = _WeightMatrix.Multiply(new Matrix(pattern.GetWeightMatrix()));
        }

        public HopfieldPattern Recognize(HopfieldPattern hopfieldPattern)
        {
            var pattern = hopfieldPattern.GetArray();
            var result = new double[pattern.Length];
            // Get value from each column and place it in an array.
            for(var colIndex=0; colIndex < pattern.Length; colIndex++)
            {
                var columnResult = 0.0;

                for(var rowIndex=0; rowIndex < pattern.Length; rowIndex++)
                {
                    if (pattern[rowIndex] > 0)
                        columnResult += _WeightMatrix.GetValue(rowIndex, colIndex);
                }

                result[colIndex] = columnResult;
            }

            return new HopfieldPattern(result);
        }
    }
}
