using AspenCapitalProject.Models;
using BsusinessLogic.Contracts;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspenCapitalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectionDataBL _projectionDataBL;

        public HomeController(ILogger<HomeController> logger, IProjectionDataBL projectionDataBL)
        {
            _logger = logger;
            _projectionDataBL = projectionDataBL;
        }


        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult DisplayProjection(RequestDto requestDto) 
        {
           
            var projections = _projectionDataBL.GetAllProjections(requestDto);
            return PartialView("_ResultProjections", projections);
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
