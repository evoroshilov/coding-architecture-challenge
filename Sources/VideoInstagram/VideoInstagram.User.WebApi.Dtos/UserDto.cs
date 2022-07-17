namespace VideoInstagram.WebApi.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset Birthday { get; set; }

        public string Phone { get; set; }

        public DateTimeOffset JoinDate { get; set; }
        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string ZipCode { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
