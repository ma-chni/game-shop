using System.Collections.Generic;

namespace GameShop.Domain.Models
{
    class Customer
    {
        public Customer(string firstName, string lastName, string socialSecurityNumber, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            Address = address;
        }

        public Customer()
        {

        }

        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string SocialSecurityNumber { get; protected set; }
        public Address Address { get; protected set; }
        public List<Order> Order { get; protected set; } = new List<Order>();
    }
}
