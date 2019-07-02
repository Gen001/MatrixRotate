using System;

namespace MatrixRotate.Services
{
    public class Matrix : IMatrix
    {
        private int[,] data;
        public Matrix()
        {
        }

        public int[,] Data => data;

        public void Generate(int size)
        {
            data = new int[size, size];
            Random rndValue = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    data[i, j] = rndValue.Next(int.MinValue, int.MaxValue);
                }
            }
        }

        public void Init(int[,] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException();
            }
            if (data.GetLength(0) != data.GetLength(1))
            {
                throw new ArgumentException("Dimensions not equal!");
            }
            this.data = data;
        }

        public void Rotate()
        {
            if (data == null)
            {
                throw new InvalidOperationException("Matrix not initialized propertly");
            }
            int dimension = data.GetLength(0);
            for (long i = 0; i < dimension / 2; i++)
            {
                for (long j = i; j < dimension - i - 1; j++)
                {
                    //swap
                    int temp = data[i, j];
                    data[i, j] = data[dimension - j - 1, i];
                    data[dimension - j - 1, i] = data[dimension - i - 1, dimension - j - 1];
                    data[dimension - i - 1, dimension - j - 1] = data[j, dimension - i - 1];
                    data[j, dimension - i - 1] = temp;
                }
            }
        }
    }
}
