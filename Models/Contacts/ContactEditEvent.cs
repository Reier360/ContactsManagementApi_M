using Models.Enums;
using System;

namespace Models.Contacts
{
    public class ContactEditEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }

        public EnumContactActions Event { get; } = EnumContactActions.ContactEdit;
    }
}
