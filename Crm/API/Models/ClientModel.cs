using Domain.Enum;

namespace API.Models
{
    public class ClientModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CodeOfTheCountry СodeOfTheCountry { get; set; }

        public string RegionCode { get; set; }

        public string SubscriberNumber { get; set; }
    }
}
