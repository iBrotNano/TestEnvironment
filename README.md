| Name                       | Status                                                       |
| -------------------------- | ------------------------------------------------------------ |
| Pipeline for `master`      | [![pipeline status](https://gitlab.3h-co.de/private/project/badges/master/pipeline.svg)](https://gitlab.3h-co.de/private/project/-/commits/master) |
| Code coverage for `master` | [![coverage report](https://gitlab.3h-co.de/private/project/badges/master/coverage.svg)](https://gitlab.3h-co.de/private/project/-/commits/master) |

[[_TOC_]]

# Readme

Maintainer: <marcel@3h-co.de>

Version: 4.7.1.1

## What is it?

The project sets up an environment for xUnit tests. This environment makes creating fixtures for .NET tests easy.

## Documentation

You will find all documents written while the development of the project in the Wiki at [Uri to the wiki].

The api documentation will be published at [Uri to the api docs].

## Using the environment for your own tests

Using the environment is pretty easy. Start with a regular xUnit test project from a template. It’s project file might look like this:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>

    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" />
    <PackageReference Include="xunit" />
    <PackageReference Include="xunit.runner.visualstudio">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="coverlet.collector">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>

</Project>
```

xUnit, Microsoft.NET.Test.Sdk and Coverlet are already included. Just add the NuGet `MarcelMelzig.TestEnvironment`. Done!

To see how the environment is used the repository contains an example app and a test project. The test project is organized in lessons. You can test the topics explained here directly in you IDE. So let’s see what the library offers step by step. In this document I will show those lessons in a reduced form to point out only the important aspects.

### Lesson 1 - Additional asserts

I use Shouldly for writing asserts because it is much more readable. I have added additional methods to test the logging. I plan to extend those methods for other topics in the future. But for now testing calls on `ILogger` as easy as that:

```c#
// The lib offers some asserts as extension methods to test logging. Logging is one of every
// apps basic features or at least should be. So it should be tested as any other code, right?
public class Lesson_1_AssertTests
{
    private readonly IndexModel indexModel;
    private readonly ILogger<IndexModel> logger;
    
    public Lesson_1_AssertTests()
    {
        // Usually the logger is injected via dependency injection. It is possible to log mocks
        // and instances of ILogger.
        logger = Mock.Of<ILogger<IndexModel>>();
        indexModel = new IndexModel(logger);
    }
    
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
}
```

## Lesson 2 - Helper methods

I often have to write fixtures for similar tests. I collect reusable fixtures in factory class to have a standard way to create them. Those fixtures are part of the lib and can be used like this:

```c#
// Writing useful fixtures is sometimes hard. I have collected some of my fixtures and put them
// into factory methods. To use them you could inherit from FixtureFactory.
public class Lesson_2_HelperMethodsTests : FixtureFactory
{
    [Fact]
    public void Test_1_UsingAConfiguration()
    {
        // You can create an IConfiguration as you would use in .NET. You can pass it into
        // services and test them as you would use them in you app.
        _ = CreateConfiguration(new Dictionary<string, string?>
        {
            { "key1", "value1" },
            { "key2", "value2" }
            ...
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
            	o.Value == Mock.Of<TestServiceOptions>(),
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
```

## Lesson 3 - Database write tests

```c#
// Most of the time i want to persist my data in a database with EFCore. It is best practice to
// divide write tests from read tests because write tests may have side effects. This lesson
// shows how to set up an in memory DbContext. All you have to do is inherit your test class
// from TestBase. Define the type of the DbContext and the class to test as the generic type references.
public class Lesson_3_DatabaseWriteTests : TestBase<AppDbContext, TestService>
{
    [Fact]
    public async Task SaveItemShouldPersistItemInDatabase()
    {
        // The test class has the property DbContext. You can use it to test your database code.
        var service = new TestService( DbContext);
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
}
```

## Lesson 4 - Configuring the TestBase for a more specific setup

```c#
// This lesson teaches how to configure TestBase with a more complex setup.
public class Lesson_4_ConfigureTestBase : TestBase<AppDbContext, IndexModel>
{
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
}
```

## Lesson 5 - Using SQLite as an in memory database

```c#
// In this lesson you will learn how to use a SQLite database in memory instead of the EFCore.
// The EFCore in memory database is not a real database using SQL. It is more a key value store
// to mock a database. If you prefer a real SQL database but don't want to miss the performance
// of an in memory store SQLite offers the ability to use it in memory.
public class Lesson_5_SqliteWriteTests : TestBase<AppDbContext, TestService>
{
    // The setup is as easy as this. Select the SQLite InMomoryProvider an create a in memory
    // SQLite connection.
    public Lesson_5_SqliteWriteTests()
        : base(provider: InMemoryProvider.SQLite,
              sqliteConnection: Database.CreateSqliteInMemoryConnection())
    { }
    
    // There is no difference to any other provider during writing tests.
    [Fact]
    public async Task UsingSqliteDbContext()
    {
        DbContext.Items.Add(new Item { Id = 1 });
        await DbContext.SaveChangesAsync();
    }
}
```

## Lesson 6 - Read tests

All read tests can share the same `DbContext` because they don’t have side effects. When they are added to a `CollectionDefinition` the context is created only once and disposed after the last tests is run. This also means you can define test data in a single point for all tests. The setup is a bit more complicated, but it's worth the effort.

First you have to create a specialized test base class by inheriting from `TestBase`.

```c#
// I use AppDbContext as both. The database context and the
// logging category.
public class AppDbContextReadTestBase : TestBase<AppDbContext, AppDbContext>
{
    // The main aspect of creating a specializes fixture for read tests is to seed test data. A
    // method can be passed into the base constructor to do exactly this.
    public AppDbContextReadTestBase()
        : base(seedAction: Seed)
    { }

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
}

// This is the configuration for xUnit. All test classes getting an AppDbContextReadTestBase
// instance via constructor injected share the same context for it's tests. Use the const
// Consts.DatabaseReadTestsCollectionDefinitionName to prevent typos.
[CollectionDefinition(Consts.DatabaseReadTestsCollectionDefinitionName)]
public class AppDbContextReadTestsCollection
    : ICollectionFixture<AppDbContextReadTestBase>
{ }
```

 The test classes now can use this `DbContext`.

```c#
[Collection(Consts.DatabaseReadTestsCollectionDefinitionName)]
public class Lesson_6_DatabaseReadTestBase2
{
    private readonly AppDbContextReadTestBase testBase;
    
    public Lesson_6_DatabaseReadTestBase2(
        AppDbContextReadTestBase testBase)
    {
        this.testBase = testBase;
    }

    [Fact]
    public async Task DatabaseReadShouldReturnData()
    {
        var item2 = await testBase.DbContext.Items.FindAsync(1);
        item2.ShouldNotBeNull();
    }
}
```

## Development

### Clone the project

You can clone the project with:

```bash
git clone https://username@repository.uri/project.git
```

### Environment

Visual Studio is uses for development. No other tool is needed.

#### Git LFS

https://git-lfs.github.com/

The project uses Git LFS to manage large binary files.

To add new types of files which should be managed by Git LFS you can add them to *.gitattributes* by command line:

```bash
git lfs track "*.format-extension"
```

#### NuGet

Regular NuGets are obtained from the NuGet Repository. This is managed and hosted by Microsoft. It is already preconfigured in Visual Studio. No further steps are necessary.

## Known Issues

No issues are known. Happy coding!

### Dependencies in test projects

Most of the dependencies are added just by referencing the `MarcelMelzig.TestEnvironment` project. But not all can be added in a way, that `dotnet test` works properly. Some dependencies must be added explicitly to test projects.

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" />
    <PackageReference Include="xunit" />
    <PackageReference Include="xunit.runner.visualstudio">
  		<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  		<PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="coverlet.collector">
  		<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  		<PrivateAssets>all</PrivateAssets>
    </PackageReference>
</ItemGroup>
```

Those dependencies are needed to run the tests and to collect coverage information with Coverlet.
