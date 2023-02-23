using AppExample.Entities;
using AppExample.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using TestEnvironment;

namespace TestProjectExample
{
    // The lib offers some asserts as extension methods to test logging. Logging is one of every
    // apps basic features or at least should be. So it should be tested as any other code, right?
    public class Lesson_1_AssertTests
    {
        #region Fields

        private readonly IndexModel indexModel;
        private readonly ILogger<IndexModel> logger;

        #endregion Fields

        #region Constructors

        public Lesson_1_AssertTests()
        {
            // Usually the logger is injected via dependency injection. It is possible to log mocks
            // and instances of ILogger.
            logger = Mock.Of<ILogger<IndexModel>>();
            indexModel = new IndexModel(logger);
        }

        #endregion Constructors

        #region Tests

        [Fact]
        public void Test_1_SimpleLogging()
        {
            // Call the method which does logging.
            _ = indexModel.OnGet();

            // The test is green if exactly this message is logged with this level once.
            logger.ShouldLog(LogLevel.Information,
                "This is useless information.",
                Times.Once());
        }

        [Fact]
        public void Test_2_LoggingWithData()
        {
            var item = new Item
            {
                Id = 1,
                Name = "TestItem"
            };

            // Call the method which does logging.
            _ = indexModel.OnPostSaveItemAsync(item);

            // The resulting message contains the item data.
            logger.ShouldLog(LogLevel.Debug,
                $"Saving item {item.Name} [{item.Id}].",
                Times.Once());
        }

        #endregion Tests
    }
}