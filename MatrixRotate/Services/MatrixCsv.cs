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
            Stream stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);

            int size = data.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                string line = "";
                for (int j = 0; j < size; j++)
                {
                    line = line + data[i,j].ToString() + ",";
                }
                writer.WriteLine(line);
            }
            return stream;
        }

        public int[,] Import(Stream stream)
        {
            int n = 0;
            IMatrix res = new Matrix();
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
                    for (int i = 0; i < row.LongLength; i++)
                    {
                        matrixData[n, i] = int.Parse(row[i]);
                    }
                    n++;
                    line = reader.ReadLine();
                }
                return matrixData;
            }
            catch (System.FormatException)
            {

            }
            catch (System.ObjectDisposedException)
            {

            }
            return null;
        }
    }
}
