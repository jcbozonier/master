using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace HopfieldDemo
{
    public class MyFirstHopfieldNetwork
    {
        public MyFirstHopfieldNetwork()
        {
            var pattern1 = Matrix.Create(new[]
                                             {
                                                 new []{-1.0, 1.0, -1.0, 1.0}
                                             });
            var inversePattern1 = Matrix.Create(new[]
                                                    {
                                                        new []{-1.0},
                                                        new []{1.0},
                                                        new []{-1.0},
                                                        new []{1.0}
                                                    });

            var pattern1Prime = pattern1.Multiply(inversePattern1);
            pattern1Prime[0, 0] = 0;
            pattern1Prime[1, 1] = 0;
            pattern1Prime[2, 2] = 0;
            pattern1Prime[3, 3] = 0;

            var pattern2 = Matrix.Create(new[,] { { 1.0, -1.0, -1.0, 1.0 } });
            var inversePattern2 = Matrix.Create(new[,] { { 1.0 }, { -1.0 }, { -1.0 }, { 1.0 } });

            var pattern2Prime = pattern2.Multiply(inversePattern2);
            pattern2Prime[0, 0] = 0;
            pattern2Prime[1, 1] = 0;
            pattern2Prime[2, 2] = 0;
            pattern2Prime[3, 3] = 0;

            var contributionMatrix = pattern1Prime.Clone();
            contributionMatrix.Add(pattern2Prime);

            var result = contributionMatrix.GetArray();

        }
    }
}
