using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICustomerDBContext
    {
        void Add(Contact info);
        void Edit(Contact info);
        void Delete(int Id);
        List<Contact> List(int skip, int take, string orderColumn, string ascDesc);
        int Count();
        Contact Get(int Id);
    }
}
