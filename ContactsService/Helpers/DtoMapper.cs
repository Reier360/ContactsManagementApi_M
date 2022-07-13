using AutoMapper;
using Models.Contacts;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsService.Helpers
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ContactEditDto, ContactEditEvent>();
            CreateMap<ContactAddDto, ContactAddEvent>();
            CreateMap<Contact, ContactListDto>();
        }
    }
}
