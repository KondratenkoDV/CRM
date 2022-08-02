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
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var context = new CrmContext(optionsBuilder);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(CrmContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
