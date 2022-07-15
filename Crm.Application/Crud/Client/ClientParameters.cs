using System;
using Crm.Domain;

namespace Crm.Application.Crud.Client
{
    public class ClientParameters
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CodeOfTheCountry СodeOfTheCountry { get; set; }

        public string RegionCode { get; set; }

        public string SubscriberNumber { get; set; }
    }
}
