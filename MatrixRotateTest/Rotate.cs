using System;
using Xunit;
using MatrixRotate.Services;

namespace MatrixRotate
{
    public class MatrixRotateTest
    {
        private readonly Matrix _matrix;
        public MatrixRotateTest()
        {
            _matrix = new Matrix();
        }

        [Fact]
        public void RotateInvalidDimensionTest()
        {
            int[,] data = new int[3, 4];
            _matrix.Rotate(data);
            data = new int[5, 4];
            _matrix.Rotate(data);
        }
    }
}
