using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MatrixRotate.Services
{
    public class MatrixCsv : IMatrixIO
    {
        public MatrixCsv()
        {
        }

        public Stream Export(int[,] data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.GetLength(1) != data.GetLength(0))
            {
                throw new ArgumentException("Dimensions of matrix is not equal");
            }
            MemoryStream stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);

            int size = data.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                string line = "";
                for (int j = 0; j < size; j++)
                {
                    line = line + data[i,j].ToString() + ",";
                }
                line = line.Remove(line.Length - 1);
                writer.WriteLine(line);
            }
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public int[,] Import(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            int n = 0;
            int[,] matrixData = null;
            var reader = new StreamReader(stream, Encoding.UTF8);
            try
            {
                string line = reader.ReadLine();

                while (!string.IsNullOrEmpty(line))
                {
                    line.Replace(" ", string.Empty);
                    string[] row = line.Split(",");
                    if (matrixData == null)
                    {
                        // Init
                        matrixData = new int[row.LongLength, row.LongLength];
                    }
                    if(n > matrixData.GetLength(1) - 1)
                    {
                        throw new ArgumentException("Dimensions of matrix are not equal");
                    }
                    for (int i = 0; i < row.LongLength; i++)
                    {
                        matrixData[n, i] = int.Parse(row[i]);
                    }
                    n++;
                    line = reader.ReadLine();
                }
                if(matrixData.GetLength(0) != n)
                {
                    throw new ArgumentException("Dimensions of matrix are not equal");
                }
                return matrixData;
            }
            catch (System.FormatException)
            {
                //TODO Add logger
            }
            catch (System.ObjectDisposedException)
            {
                //TODO Add logger

            }
            return null;
        }
    }
}
