using System;
using MatrixRotate.Services;
using Xunit;

namespace MatrixRotateTest
{
    public class Rotate
    {

        public Rotate()
        {
        }

        [Fact]
        public void Matrix_NotInicialized_Test()
        {
            IMatrix matrix = new Matrix();
            Exception ex = Assert.Throws<InvalidOperationException>(() => matrix.Rotate());
        }

        [Fact]
        public void Matrix_Rotate_Test()
        {
            IMatrix matrix = new Matrix();
            int[,] data = new int[3,3]{{25, 38, 12}, {1, 7, 76}, {21, 64, 54}};
            int[,] expectedData = new int[3,3]{{21, 1, 25}, {64, 7, 38}, {54, 76, 12}};
            matrix.Init(data);
            matrix.Rotate();
            Assert.Equal(expectedData, matrix.Data);
        }
    }
}
