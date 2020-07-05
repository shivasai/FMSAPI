using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS_Web_Api.Models;
using FMS_Web_Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _repository;
        public DashboardController(IDashboardRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<DashboardController>
        [HttpGet]
        public async Task<ActionResult<DashboardVM>> Get()
        {
            return await _repository.Get();
        }
    }
}
