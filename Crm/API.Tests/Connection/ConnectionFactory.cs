using System;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Tests.Connection
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
