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
    public class RotateController : ControllerBase
    {
        private readonly IMatrix _matrix;
        public RotateController(IMatrix matrix) 
        { 
            _matrix = matrix;
        }

        // POST api/rotate
        [HttpPost]
        public IActionResult Post([FromBody] int[,] value) 
        {
            return Ok(_matrix.Rotate(value));
        }
    }
}
