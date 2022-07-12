using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Contacts
{
    public class ContactDelete
    {
        public int Id { get; set; }

        public EnumContactActions Event { get; } = EnumContactActions.Delete;
    }
}
