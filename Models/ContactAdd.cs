using System;

namespace Models
{
    public class ContactAdd
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TelephoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public EnumContactActions Event { get; } = EnumContactActions.Add;
    }
}
