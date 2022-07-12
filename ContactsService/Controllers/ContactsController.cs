using ContactsService.Interfaces;
using DataAccess.Interfaces;
using DataAccess.PostgreSQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Contacts;
using System;

namespace ContactsService.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IMessageBusClient _messageBusClient;
        private readonly ICustomerDBContext _customerContext;

        public ContactsController(ILogger<ContactsController> logger, IMessageBusClient messageBusClient, ICustomerDBContext customerContext)
        {
            _logger = logger;
            _messageBusClient = messageBusClient;
            _customerContext = customerContext;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(ContactAdd info)
        {

            _messageBusClient.PublishMessage(info);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ContactEdit info)
        {
            _messageBusClient.PublishMessage(info);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _messageBusClient.PublishMessage(new ContactDelete
            {
                Id = id
            });
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult List(int skip = 0, int take = 20, string column = "", string ascDesc = "")
        {
            return Ok(_customerContext.List(skip, take, column, ascDesc));
        }
    }
}
