using dependency_injection_examples.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dependency_injection_examples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ConsoleService _consoleService;
        private readonly IStorageService _storageService;
        private readonly IEmailService _emailService;

        public ServicesController(
            ConsoleService consoleService, 
            IStorageService storageService,
            IEmailService emailService
            )
        {
            _consoleService = consoleService;
            _storageService = storageService;
            _emailService = emailService;
        }

        // Route is /api/services/1
        [HttpGet()]
        [Route("1")]
        public ActionResult ExampleOne()
        {
            _consoleService.PrintHelloWold();

            return new ObjectResult("Printed Hello, World to Console!");
        }

        [HttpGet()]
        [Route("2")]
        public ActionResult ExampleTwo()
        {
            _storageService.StoreFile();

            return new ObjectResult("Stored Hello, World file to root!");
        }

        [HttpGet()]
        [Route("3")]
        public ActionResult ExampleThree()
        {
            _emailService.SendMailToAllAddressees("Hello, world!");

            return new ObjectResult("Send emails to all customers!");
        }
    }
}
