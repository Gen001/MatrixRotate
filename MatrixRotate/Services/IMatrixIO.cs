using System;
using System.IO;

namespace MatrixRotate.Services
{
    public interface IMatrixIO
    {
        int[,] Import(Stream stream);
        Stream Export(int[,] data);
    }
}
