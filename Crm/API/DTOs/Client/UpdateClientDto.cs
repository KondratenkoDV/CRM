using Domain.Enum;

namespace API.DTOs.Client
{
    public class UpdateClientDto
    {
        public string NewName { get; set; }

        public CodeOfTheCountry NewСodeOfTheCountry { get; set; }

        public string NewRegionCode { get; set; }

        public string NewSubscriberNumber { get; set; }
    }
}
