using System;
using System.IO;
using System.Text;
using MatrixRotate.Services;
using Xunit;

namespace MatrixRotateTest
{
    public class Import
    {
        private MatrixCsv matrixIO = new MatrixCsv();
        public Import()
        {
        }

        private Stream StringToStream(string input)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
        [Fact]
        public void Import_Invalid_Dimension_Test()
        {
            Stream inputStream = StringToStream("25,38\n1,7\n21,64\n");
            Assert.Throws<ArgumentException>(() => matrixIO.Import(inputStream));
            inputStream = StringToStream("25,38,12\n1,7,76\n");
            Assert.Throws<ArgumentException>(() => matrixIO.Import(inputStream));
        }

        [Fact]
        public void Matrix_Import_Test()
        {
            Stream stream = StringToStream("25,38,12\n1,7,76\n21,64,54\n");
            int[,] expectedData = new int[3, 3] { { 25, 38, 12 }, { 1, 7, 76 }, { 21, 64, 54 } };
            int[,] dataFromCsv = matrixIO.Import(stream);
            Assert.Equal(expectedData, dataFromCsv);
        }

    }
}
