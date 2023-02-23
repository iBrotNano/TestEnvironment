using AppExample.Entities;
using TestEnvironment;

namespace TestProjectExample
{
    // This lesson contains three files. This one and two test classes. This file configures a test
    // fixture for all read tests of a database. Read tests don't have side effects, so that they
    // can share the same database context. I use AppDbContext as both. The database context and the
    // logging category.
    public class AppDbContextReadTestBase : TestBase<AppDbContext, AppDbContext>
    {
        #region Constructors

        // The main aspect of creating a specializes fixture for read tests is to seed test data. A
        // method can be passed into the base constructor to do exactly this.
        public AppDbContextReadTestBase()
            : base(seedAction: Seed)
        { }

        #endregion Constructors

        #region Methods

        // In this method you can define all data you need for read tests.
        public static void Seed(AppDbContext dbContext)
        {
            dbContext.Items.Add(new Item
            {
                Id = 1,
                Description = "Description of the item.",
                Name = "Testitem",
                Title = "TestI-Item",
                Url = "https://testitem"
            });

            dbContext.SaveChanges();
        }

        #endregion Methods
    }

    // This is the configuration for xUnit. All test classes getting an AppDbContextReadTestBase
    // instance via constructor injected share the same context for it's tests. Use the const
    // Consts.DatabaseReadTestsCollectionDefinitionName to prevent typos.
    [CollectionDefinition(Consts.DatabaseReadTestsCollectionDefinitionName)]
    public class AppDbContextReadTestsCollection
        : ICollectionFixture<AppDbContextReadTestBase>
    { }
}