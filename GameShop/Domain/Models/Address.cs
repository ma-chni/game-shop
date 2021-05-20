namespace GameShop.Domain.Models
{
    class Address
    {
        public Address(string street, string city, string postcode)
        {
            Street = street;
            City = city;
            Postcode = postcode;
        }

        public int Id { get; protected set; }
        public string Street { get; protected set; }
        public string City { get; protected set; }
        public string Postcode { get; protected set; }
        public int CustomerId { get; protected set; }
        public Customer Customer { get; protected set; }
    }
}
