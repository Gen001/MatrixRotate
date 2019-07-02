using System;

namespace MatrixRotate.Services
{
    public interface IMatrix
    {
        int[,] Rotate (int[,] data);
    }
}
