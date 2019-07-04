
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MatrixRotate.Services;
using Microsoft.AspNetCore.Mvc;
//using MatrixRotate.Models;

namespace MatrixRotate.Controllers
{
    [Route("api/[controller]")]
    public class MatrixDataController : ControllerBase
    {
        private readonly IMatrix _matrix;
        private readonly IMatrixIO _reader;
        public MatrixDataController(IMatrix matrix, IMatrixIO reader)
        {
            _matrix = matrix;
            _reader = reader;
        }

        /// <summary>
        /// GET api/matrixdata/begin/count
        /// </summary>
        /// <param name="startRow">Start row of the matrix</param>
        /// <param name="count">count of the rows from the matrix</param>
        /// <returns>int[,]</returns>
        [HttpGet("{startRow}/{count?}")]
        public ActionResult<int[,]> Gets(int startRow, int count = 1)
        {
            if (_matrix.Data == null)
            {
                return BadRequest("Matrix not inicialized!");
            }
            int size = _matrix.Data.GetLength(0);
            if (count > size)
            {
                count = size;
            }
            int[,] res = new int[count, size];
            Buffer.BlockCopy(_matrix.Data, startRow * size * sizeof(int), res, 0, size * count * sizeof(int));
            return Ok(res);
        }
         // GET api/matrixdata/size
        [HttpGet("size")]
        public ActionResult<int> GetSize()
        {
           return Ok(_matrix.Data?.GetLength(0) ?? 0);
        }

        // GET api/matrixdata
        [HttpGet("export")]
        public FileStreamResult Export()
        {
            Stream stream = _reader.Export(_matrix.Data);
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, "Matrix.csv");
        }

        // GET api/matrixdata/generate
        [HttpPost("generate")]
        public ActionResult Generate([FromBody]int size)
        {
            if (size < 1)
            {
                return BadRequest("Size must be greater than 0");
            }
            _matrix.Generate(size);
            return Ok();
        }

        /// <summary>
        /// POST api/matrixdata
        /// Imports a matrix from the CSV file
        /// </summary>
        /// <returns>Size of the matrix</returns>
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult Import()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _matrix.Init(_reader.Import(file.OpenReadStream()));
                    return Ok(_matrix.Data.GetLength(0));
                }
                return BadRequest("File length is zero");
            }
            catch (System.Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }
        }

    }
}

