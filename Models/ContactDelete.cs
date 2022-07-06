using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ContactDelete
    {
        public int Id { get; set; }

        public EnumContactActions Event { get; } = EnumContactActions.Delete;
    }
}
