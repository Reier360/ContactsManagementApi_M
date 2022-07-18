using AutoMapper;
using ContactsService.Interfaces;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Contacts;
using Models.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost("list")]
        [Authorize]
        public async Task<IList<ContactListDto>> List(DatatablePagination paging)
        {
            var items = _customerContext.List(paging.skip, paging.take, paging.orderColumn, paging.ascDesc);
            return _mapper.Map<IList<ContactListDto>>(items);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(ContactAddDto info)
        {
            _messageBusClient.PublishMessage(_mapper.Map<ContactAddEvent>(info));
            return Ok();
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ContactEditDto info)
        {
            _messageBusClient.PublishMessage(_mapper.Map<ContactEditEvent>(info));
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _messageBusClient.PublishMessage(new ContactDeleteEvent
            {
                Id = id
            });
            return Ok();
        }        

        [HttpGet("get/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ContactItemDto> Get(int id)
        {
            var item = _customerContext.Get(id);
            return _mapper.Map<ContactItemDto>(item);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<int> Count()
        {
            return _customerContext.Count();
        }
    }
}
