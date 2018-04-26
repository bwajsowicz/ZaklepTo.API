using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastructure.Entities;
using ZaklepTo.Infrastructure.Services.Implementations;

namespace ZaklepTo.API.Controllers
{
    public class DummyController : Controller
    {
        private readonly DataBaseService _dataBaseService;

        public DummyController(DataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
