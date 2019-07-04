using System;

namespace MatrixRotate.Services
{
    public interface IMatrix
    {
        int[,] Data {get;}
        void Init (int[,] data);
        /// <summary>
        /// Generates matrix of specified size with random values
        /// </summary>
        void Generate (uint size);
        void Rotate ();
    }
}
