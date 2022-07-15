using System;
using Microsoft.EntityFrameworkCore;
using Crm.Persistence;



namespace Crm.Application.Tests
{
    public static class ConnectionFactory
    {
        public static CrmContext Generate()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CrmContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            return new CrmContext(optionsBuilder.Options);
        }
    }
}
