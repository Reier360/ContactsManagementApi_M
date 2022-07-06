using ContactsService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
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
        private readonly IMessageBusClient _messageBusClient;

        public ContactsController(ILogger<ContactsController> logger, IMessageBusClient messageBusClient)
        {
            _logger = logger;
            _messageBusClient = messageBusClient;
        }

        [HttpPost]
        public Object Add(ContactAdd info)
        {

            _messageBusClient.PublishMessage(info);
            return Ok();
        }

        [HttpPut]
        public Object Edit(ContactEdit info)
        {
            _messageBusClient.PublishMessage(info);
            return Ok();
        }

        [HttpDelete]
        public Object Delete(ContactDelete info)
        {
            _messageBusClient.PublishMessage(info);
            return Ok();
        }

        [HttpGet]
        public Object List()
        {
            return Ok();
        }
    }
}
