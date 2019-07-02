
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

        // GET api/matrixdata
        [HttpGet("{startRow}/{count?}")]
        public ActionResult<int[,]> Gets(int startRow, int count = 1)
        {
            if(_matrix.Data == null)
            {
                return BadRequest("Matrix not inicialized!");
            }
            int size = _matrix.Data.GetLength(0);
            int[,] res = new int[count, size];
            Buffer.BlockCopy(_matrix.Data, startRow * size * sizeof(int), res, 0, size * count * sizeof(int));
            return Ok(res);
        }
        // GET api/matrixdata
        [HttpGet("export")]
        public IActionResult Export()
        {
            Stream stream = _reader.Export(_matrix.Data);
            return File(stream, "application/octet-stream", "Matrix.csv");;
        }

        // GET api/matrixdata/generate
        [HttpPost("generate")]
        public ActionResult Generate([FromBody]int size)
        {
            if(size < 1)
            {
                return BadRequest("Size must be greater than 0");
            }
            _matrix.Generate(size);
            return Ok();
        }

        // POST api/matrixdata
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult Import()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                //string newPath = Path.Combine(rootPath, folderName);
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string fullPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        //file.CopyTo(stream);
                        _matrix.Init(_reader.Import(stream));
                    }
                    return Ok();
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

