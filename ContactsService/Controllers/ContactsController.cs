using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsService.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Object Add()
        {
            return Ok();
        }

        [HttpPut]
        public Object Edit()
        {
            return Ok();
        }

        [HttpDelete]
        public Object Delete()
        {
            return Ok();
        }

        [HttpGet]
        public Object List()
        {
            return Ok();
        }
    }
}
