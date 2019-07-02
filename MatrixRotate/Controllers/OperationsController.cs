using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatrixRotate.Services;
using Microsoft.AspNetCore.Mvc;
//using MatrixRotate.Models;

namespace MatrixRotate.Controllers
{
    [Route("api/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly IMatrix _matrix;
        public OperationsController(IMatrix matrix)
        {
            _matrix = matrix;
        }

        // POST api/rotate
        [HttpPost("rotate")]
        public IActionResult Rotate()
        {
            try
            {
                _matrix.Rotate();
                return Ok(_matrix.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
