using AutoMapper;
using ContactsService.Interfaces;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Contacts;
using System.Collections.Generic;

namespace ContactsService.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IMessageBusClient _messageBusClient;
        private readonly ICustomerDBContext _customerContext;
        private readonly IMapper _mapper;

        public ContactsController(ILogger<ContactsController> logger, IMessageBusClient messageBusClient, ICustomerDBContext customerContext, IMapper mapper)
        {
            _logger = logger;
            _messageBusClient = messageBusClient;
            _customerContext = customerContext;
            _mapper = mapper;
        }

        [HttpPost("add")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Add(ContactAddDto info)
        {
            _messageBusClient.PublishMessage(_mapper.Map<ContactAddEvent>(info));
            return Ok();
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public IActionResult Edit(ContactEditDto info)
        {
            _messageBusClient.PublishMessage(_mapper.Map<ContactEditEvent>(info));
            return Ok();
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _messageBusClient.PublishMessage(new ContactDeleteEvent
            {
                Id = id
            });
            return Ok();
        }

        [HttpGet]
        //[Authorize]
        public IActionResult List(int skip = 0, int take = 20, string column = "", string ascDesc = "")
        {
            return Ok(_mapper.Map<IList<ContactListDto>>(_customerContext.List(skip, take, column, ascDesc)));
        }
    }
}
