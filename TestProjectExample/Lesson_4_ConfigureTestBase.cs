using AppExample.Entities;
using AppExample.Pages;
using AppExample.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TestEnvironment;

namespace Escido.Infrastructure.Tests.ConnectedServices.EesmWebCommunicationService.Validators
{
    // This lesson teaches how to configure TestBase with a more complex setup.
    public class Lesson_4_ConfigureTestBase : TestBase<AppDbContext, IndexModel>
    {
        #region Constructors

        // You can configure a lot of behavior via constructor. You can add key-value pairs as a
        // IConfigurationRoot used by .NET. Also a IServiceProvider can be configured. But most
        // useful might be the configuration of the in memory database context. EFCore InMemory can
        // and should be used for the most tests. But sometimes an in memory SQLite could be a good
        // alternative. EFCore InMemory is not a real SQL database. SQLite is. So it is more near to
        // the production system. In a seed method you can define test data in the database.
        public Lesson_4_ConfigureTestBase()
            : base(new Dictionary<string, string?>
                {
                    { "key", "value" }
                },
                seedAction: Seed,
                providedServices: (typeof(ITestService), Mock.Of<ITestService>()))
        { }

        #endregion Constructors

        #region Tests

        // The configured test environment is accessable via properties.
        [Fact]
        public void UseComplexSetup()
        {
            _ = ServiceProvider.GetService<ITestService>();

            _ = Configuration is null
                ? string.Empty
                : Configuration.GetValue<string>("key");

            _ = DbContext.Items.Find(1);
        }

        #endregion Tests

        #region Helpers

        // Data can be seeded into the test database.
        private static void Seed(AppDbContext dbContext)
        {
            dbContext.Items.Add(new Item
            {
                Id = 1,
                Description = "Test",
                Name = "Test",
                Title = "Test",
                Url = "Test"
            });

            dbContext.SaveChanges();
        }

        #endregion Helpers
    }
}