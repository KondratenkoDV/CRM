﻿using System;
using Persistence;
using API.Tests.Connection;

namespace API.Tests
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
