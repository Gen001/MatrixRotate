using System;
using System.IO;
using System.Text;
using MatrixRotate.Services;
using Xunit;

namespace MatrixRotateTest
{
    public class Export
    {
        private MatrixCsv matrixIO = new MatrixCsv();
        public Export()
        {
        }
        //TODO Add null argument tests
        
        [Fact]
        public void Export_Invalid_Dimension_Test()
        {
            int[,] data = new int[3, 4];
            Exception ex = Assert.Throws<ArgumentException>(() => matrixIO.Export(data));
        }

        [Fact]
        public void Matrix_Export_Test()
        {
            int[,] data = new int[3,3]{{25, 38, 12}, {1, 7, 76}, {21, 64, 54}};
            string expectedData = "25,38,12\n1,7,76\n21,64,54\n";
            StreamReader reader = new StreamReader(matrixIO.Export(data));
            string csvData = reader.ReadToEnd();
            Assert.Equal<string>(expectedData, csvData);
        }

    }
}
