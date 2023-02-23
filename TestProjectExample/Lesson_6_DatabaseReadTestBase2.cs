using Shouldly;
using TestEnvironment;
using TestProjectExample;

namespace Escido.Infrastructure.Tests.ConnectedServices.EesmWebCommunicationService.Validators
{
    [Collection(Consts.DatabaseReadTestsCollectionDefinitionName)]
    public class Lesson_6_DatabaseReadTestBase2
    {
        #region Fields

        private readonly AppDbContextReadTestBase testBase;

        #endregion Fields

        #region Constructors

        public Lesson_6_DatabaseReadTestBase2(
            AppDbContextReadTestBase testBase)
        {
            this.testBase = testBase;
        }

        #endregion Constructors

        #region Tests

        // The in the first test class added item is accessible here.
        [Fact]
        public async Task DatabaseReadShouldReturnData()
        {
            var item2 = await testBase.DbContext.Items.FindAsync(2);
            item2.ShouldNotBeNull();
            item2.Description.ShouldBe("This item is added to show that the test classes Lesson_6_DatabaseReadTestBase1 and Lesson_6_DatabaseReadTestBase2 are using the same database context.");
        }

        #endregion Tests
    }
}