using System;

namespace Crm.Application.Crud.Contract
{
    public class ContractParameters
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public int ClientId { get; set; }
    }
}
