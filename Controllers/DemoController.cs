using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;

namespace Project1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _myLogger;
        private readonly CollegeDbContext collegeDbContext;

        public DemoController(CollegeDbContext collegeDbContext)
        {
            this.collegeDbContext = collegeDbContext;
        }
        // [HttpGet]
       
    }
}