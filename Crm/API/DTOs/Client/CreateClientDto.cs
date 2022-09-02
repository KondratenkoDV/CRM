using Domain.Enum;
using System.Text.Json.Serialization;

namespace API.DTOs.Client
{
    public class CreateClientDto
    {
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CodeOfTheCountry СodeOfTheCountry { get; set; }

        public string RegionCode { get; set; }

        public string SubscriberNumber { get; set; }
    }
}
