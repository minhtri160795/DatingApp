using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    // http://localhost:5000/api/ValueController
    [ApiController]
    [Route("api/[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly DataContext context;
        public ValueController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var result = await context.Value.ToListAsync();
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var result = await context.Value.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(result);
        }

          [HttpGet("TinhThanh/{Id}")]
        public async Task<IActionResult> TinhThanh(int id)
        {
            var result = await context.Value.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(result);
        }
    }
}
