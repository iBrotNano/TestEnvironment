using AppExample.Entities;
using AppExample.Pages;
using AppExample.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System.Diagnostics;
using TestEnvironment;

namespace TestProjectExample
{
    // Writing useful fixtures is sometimes hard. I have collected some of my fixtures and put them
    // into factory methods. To use them you could inherit from FixtureFactory.
    public class Lesson_2_HelperMethodsTests : FixtureFactory
    {
        #region Tests

        [Fact]
        public void Test_1_UsingAConfiguration()
        {
            // You can create an IConfiguration as you would use in .NET. You can pass it into
            // services and test them as you would use them in you app.
            _ = CreateConfiguration(new Dictionary<string, string?>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            });
        }

        [Fact]
        public void Test_2_UsingAComplexTypeAsStub()
        {
            // Sometimes i need a complex type to test something but the type is not important.
            // Maybe to verify if a serializer is called or something. For this case i use a stub
            // complex type.
            _ = CreateComplexTypeStub();
        }

        [Fact]
        public void Test_3_UsingAFailingValidator()
        {
            // I can warmly recommend FluentValidation. Sometimes i want to write tests for code
            // reacting on failing validation.

            _ = Should.Throw<ValidationException>(() => new TestService(
                Mock.Of<IOptions<TestServiceOptions>>(o =>
                o.Value == Mock.Of<TestServiceOptions>(io =>
                io.Skip == false)),
                CreateFailingValidatorMock<TestServiceOptions>(),
                new AppDbContext(new DbContextOptions<AppDbContext>())));
        }

        [Fact]
        public void Test_4_OnGetShouldSetTheRequestIdToTheTraceIdentigierOfTheHttpContext()
        {
            // Sometimes i needed a more realistic setup for a PageModel than just new IndexModel()
            // to test my code. Here is a test for the ErrorModel from the WebApp template.
            var pageModel = CreatePageModelFixture<ErrorModel>(
                new object?[] { Mock.Of<ILogger<ErrorModel>>() });

            pageModel.RequestId.ShouldBeNull();
            Activity.Current = null;
            pageModel.OnGet();
            pageModel.RequestId.ShouldBe(pageModel.HttpContext.TraceIdentifier);
        }

        [Fact]
        public void Test_5_UsingRandomFileNames()
        {
            // Need random invalid file names for a test?
            _ = CreateRandomInvalidFileNames();
        }

        [Fact]
        public void Test_6_UsingAServiceProvider()
        {
            // Sometimes i need a service provider in a test case.
            var serviceProvider = CreateServiceProviderMock<TestService>(
                (typeof(ITestService), Mock.Of<ITestService>()));

            var logger = serviceProvider.GetService(typeof(ILogger<TestService>));
            logger.ShouldNotBeNull();
        }

        #endregion Tests
    }
}