using System;
using Xunit;
using Moq;
using MatrixRotate.Services;

namespace MatrixRotateTest
{
    public class MatrixInitTest
    {
        private IMatrix _matrix;
        public MatrixInitTest()
        {
            _matrix = new Matrix();
        }

        [Fact]
        public void Init_Invalid_Dimension_Test()
        {
            int[,] data = new int[3, 4];
            Exception ex = Assert.Throws<ArgumentException>(() => _matrix.Init(data));
        }
        
        [Fact]
        public void Init_Valid_Dimension_Test()
        {
            int[,] data = new int[4, 4];
            _matrix.Init(data);
            Assert.Equal(data, _matrix.Data);
        }
    }
}
