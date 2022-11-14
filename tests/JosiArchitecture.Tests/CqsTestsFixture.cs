using JosiArchitecture.Core.Shared;
using JosiArchitecture.Core.Shared.Persistence;
using JosiArchitecture.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace JosiArchitecture.Tests
{
    public class CqsTestsFixture : IDisposable
    {
        private IServiceScope scope;

        public IMediator Mediator { get; private set; }

        public Mock<ILogger> LoggerMock { get; private set; }

        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }

        public CqsTestsFixture()
        {
            Reset();
        }

        public void Reset()
        {
            var services = new ServiceCollection();

            services.AddDbContext<DataStore>(options => options.UseInMemoryDatabase("Tests"));
            services.AddScoped<IQueryDataStore, DataStore>();
            services.AddScoped<ICommandDataStore, DataStore>();
            services.AddScoped<DataStore, DataStore>();
            services.AddScoped<IUnitOfWork, DataStore>();

            services.AddCqs(typeof(CqsTestsFixture));

            // We mock the the IUnitOfWork to be able to do verifications on it,
            // but we still call our datastore.CompleteAsync to mimic the real behavior
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UnitOfWorkMock
                .Setup(uow => uow.CompleteAsync(It.IsAny<CancellationToken>()))
                .Callback(async () =>
                {
                    await scope.ServiceProvider.GetService<DataStore>().CompleteAsync(CancellationToken.None);
                });
            services.AddScoped(s => UnitOfWorkMock.Object);

            LoggerMock = new Mock<ILogger>();
            services.AddScoped(s => LoggerMock.Object);

            var serviceProvider = services.BuildServiceProvider();
            scope = serviceProvider.CreateScope();
            Mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }

    [CollectionDefinition(Name)]
    public class CqsTestsCollection : ICollectionFixture<CqsTestsFixture>
    {
        public const string Name = "CqsTests";

        protected CqsTestsFixture fixture;

        public CqsTestsCollection(CqsTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        // ... write tests, using fixture.Db to get access to the SQL Server ...
    }
}
