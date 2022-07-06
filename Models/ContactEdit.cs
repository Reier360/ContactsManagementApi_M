using System;

namespace Models
{
    public class ContactEdit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TelephoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public EnumContactActions Event { get; } = EnumContactActions.Delete;
    }
}
