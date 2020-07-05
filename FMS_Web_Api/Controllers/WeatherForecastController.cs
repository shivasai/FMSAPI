﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.IO;

namespace FMS_Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpcontextaccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpcontextaccessor;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // POST api/Attachments
        [HttpPost]
        public async Task<IActionResult> PostFiles(ICollection<IFormFile> files)
        {
            try
            {
                System.Console.WriteLine("You received the call!");
               // WriteLog("PostFiles call received!", true);
                //We would always copy the attachments to the folder specified above but for now dump it wherver....
                long size = files.Sum(f => f.Length);

                // full path to file in temp location
                var filePath = Path.GetTempFileName();
                var fileName = Path.GetTempFileName();

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                            //formFile.CopyToAsync(stream);
                        }
                    }
                }

                // process uploaded files
                // Don't rely on or trust the FileName property without validation.
                //Displaying File Name for verification purposes for now -Rohit

                return Ok(new { count = files.Count, fileName, size, filePath });
            }
            catch (Exception exp)
            {
                //System.Console.WriteLine("Exception generated when uploading file - " + exp.Message);
                //WriteLog("Exception generated when uploading file - " + exp.Message, true);
                //string message = $"file / upload failed!";
                //return Json(message);
                return Ok();
            }
        }

    }
}
