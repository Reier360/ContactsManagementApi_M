using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsService.Interfaces
{
    public interface IMessageBusClient
    {
        void PublishMessage<T>(T message);
    }
}
