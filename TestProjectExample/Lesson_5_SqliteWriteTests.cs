using AppExample.Entities;
using AppExample.Services;
using TestEnvironment;

namespace TestProjectExample
{
    // In this lesson you will learn how to use a SQLite database in memory instead of the EFCore.
    // The EFCore in memory database is not a real database using SQL. It is more a key value store
    // to mock a database. If you prefer a real SQL database but don't want to miss the performance
    // of an in memory store SQLite offers the ability to use it in memory.
    public class Lesson_5_SqliteWriteTests : TestBase<AppDbContext, TestService>
    {
        #region Constructors

        // The setup is as easy as this. Select the SQLite InMomoryProvider an create a in memory
        // SQLite connection.
        public Lesson_5_SqliteWriteTests()
            : base(provider: InMemoryProvider.SQLite,
                  sqliteConnection: Database.CreateSqliteInMemoryConnection())
        { }

        #endregion Constructors

        #region Tests

        // There is no difference to any other provider during writing tests.
        [Fact]
        public async Task UsingSqliteDbContext()
        {
            DbContext.Items.Add(new Item { Id = 1 });
            await DbContext.SaveChangesAsync();
        }

        #endregion Tests
    }
}