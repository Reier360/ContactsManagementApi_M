using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsConsumer.Interfaces
{
    public interface IEventProcessor
    {
        void ConsumeEvent(string message);
    }
}
