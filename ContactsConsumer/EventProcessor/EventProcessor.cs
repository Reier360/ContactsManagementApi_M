using ContactsConsumer.Interfaces;
using DataAccess.Interfaces;
using Models.Entities;
using Models.Enums;
using Models.Events;
using System;
using System.Text.Json;

namespace ContactsConsumer.EventProcessor
{
    public class EventProcessor : IEventProcessor
    {
        private readonly ICustomerDBContext _customerContext;

        public EventProcessor(ICustomerDBContext customerContext)
        {
            _customerContext = customerContext;
        }

        public void ConsumeEvent(string eventMessage)
        {
            var eventAction = DetermineEvent(eventMessage);

            if(eventAction == null)
            {
                return;
            }

            switch(eventAction)
            {
                case EnumContactActions.ContactAdd:
                    _customerContext.Add(JsonSerializer.Deserialize<Contact>(eventMessage));
                    break;
                case EnumContactActions.ContactEdit:
                    _customerContext.Edit(JsonSerializer.Deserialize<Contact>(eventMessage));
                    break;
                case EnumContactActions.ContactDelete:
                    _customerContext.Delete(JsonSerializer.Deserialize<Contact>(eventMessage).Id);
                    break;
            }             
        }

        private EnumContactActions? DetermineEvent(string eventMessage)
        {
            return JsonSerializer.Deserialize<GenericEventDto>(eventMessage).Event;           
        }
    }
}
