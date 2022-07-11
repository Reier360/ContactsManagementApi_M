using ContactsService.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public Object Add(ContactAdd info)
        {

            _messageBusClient.PublishMessage(info);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public Object Edit(ContactEdit info)
        {
            _messageBusClient.PublishMessage(info);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public Object Delete(int id)
        {
            _messageBusClient.PublishMessage(new ContactDelete
            {
                Id = id
            });
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public Object List()
        {
            return Ok();
        }
    }
}
