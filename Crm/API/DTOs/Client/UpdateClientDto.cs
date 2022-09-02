using Domain.Enum;
using System.Text.Json.Serialization;

namespace API.DTOs.Client
{
    public class UpdateClientDto
    {
        public string NewName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CodeOfTheCountry NewСodeOfTheCountry { get; set; }

        public string NewRegionCode { get; set; }

        public string NewSubscriberNumber { get; set; }
    }
}
