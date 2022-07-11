using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsService.Interfaces
{
    public interface IApiAuth
    {
        string AuthenticateUser(string username, string password);
    }
}
