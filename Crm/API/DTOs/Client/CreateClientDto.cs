using Domain.Enum;

namespace API.DTOs.Client
{
    public class CreateClientDto
    {
        public string Name { get; set; }

        public CodeOfTheCountry СodeOfTheCountry { get; set; }

        public string RegionCode { get; set; }

        public string SubscriberNumber { get; set; }
    }
}
