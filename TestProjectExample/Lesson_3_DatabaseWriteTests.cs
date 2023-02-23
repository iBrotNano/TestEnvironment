using AppExample.Entities;
using AppExample.Services;
using FluentValidation;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using TestEnvironment;

namespace TestProjectExample
{
    // Most of the time i want to persist my data in a database with EFCore. It is best practice to
    // divide write tests from read tests because write tests may have side effects. This lesson
    // shows how to set up an in memory DbContext. All you have to do is inherit your test class
    // from TestBase. Define the type of the DbContext and the class to test as the generic type references.
    public class Lesson_3_DatabaseWriteTests : TestBase<AppDbContext, TestService>
    {
        #region Tests

        [Fact]
        public async Task SaveItemShouldPersistItemInDatabase()
        {
            // The test class has the property DbContext. You can use it to test your database code.
            var service = new TestService(
                Mock.Of<IOptions<TestServiceOptions>>(o =>
                    o.Value == new TestServiceOptions
                    {
                        Skip = false
                    }),
                Mock.Of<IValidator<TestServiceOptions>>(),
                DbContext);

            var count = DbContext.Items.Count();

            await service.SaveItemAsync(new Item
            {
                Id = 1,
                Description = "Description",
                Name = "Name",
                Title = "Title",
                Url = "Url",
            });

            DbContext.Items.Count().ShouldBe(count + 1);
            var item = DbContext.Items.Find(1);
            item.ShouldNotBeNull();
            item.Description.ShouldBe("Description");
            item.Name.ShouldBe("Name");
            item.Title.ShouldBe("Title");
            item.Url.ShouldBe("Url");
        }

        #endregion Tests
    }
}