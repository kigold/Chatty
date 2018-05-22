using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chatty.Models;
using NETCore.MailKit.Core;

namespace Chatty.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _EmailService;

        public HomeController(IEmailService emailService)
        {
            _EmailService = emailService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            //_EmailService.Send("kingsleybox@yahoo.com", "ASP.NET Core mvc send email example", "Send from asp.net core mvc action");
            _EmailService.SendAsync("kingsleybox@yahoo.com", "ASP.NET Core mvc send email example", "Send from asp.net core mvc action");


            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
