using System;
using Persistence;
using Application.Tests.Connection;

namespace Application.Tests
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly CrmContext Context;

        public TestCommandBase()
        {
            Context = ConnectionFactory.Generate();
        }

        public void Dispose()
        {
            ConnectionFactory.Destroy(Context);
        }
    }
}
