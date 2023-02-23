using AppExample.Entities;
using TestEnvironment;
using TestProjectExample;

namespace Escido.Infrastructure.Tests.ConnectedServices.EesmWebCommunicationService.Validators
{
    // The tests must be added to the collection.
    [Collection(Consts.DatabaseReadTestsCollectionDefinitionName)]
    public class Lesson_6_DatabaseReadTestBase1
    {
        #region Fields

        private readonly AppDbContextReadTestBase testBase;

        #endregion Fields

        #region Constructors

        // The test database context is injected into the class and stored into a private field for
        // using in the facts.
        public Lesson_6_DatabaseReadTestBase1(
            AppDbContextReadTestBase testBase)
        {
            this.testBase = testBase;
        }

        #endregion Constructors

        #region Tests

        // Writing something into the database for read tests is not what i want to show here. The
        // test shows that the added item is accessible in the second test class because the share
        // the same database context.
        [Fact]
        public async Task DatabaseReadShouldReturnData()
        {
            testBase.DbContext.Items.Add(new Item
            {
                Description = "This item is added to show that the test classes Lesson_6_DatabaseReadTestBase1 and Lesson_6_DatabaseReadTestBase2 are using the same database context.",
                Id = 2,
                Name = "Item 2",
                Title = "Title 2",
                Url = "https://item2"
            });

            await testBase.DbContext.SaveChangesAsync();
        }

        #endregion Tests
    }
}