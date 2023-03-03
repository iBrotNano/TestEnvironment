namespace TestEnvironment
{
    /// <summary>
    /// Offers database related methods and fixtures.
    /// </summary>
    public class Database
    {
        #region Methods

        /// <summary>
        /// Returns a DbContext for the DbContext Type T for testing in an in memory database.
        /// </summary>
        /// <typeparam name="TDbContext">
        /// Type of the DbContext.
        /// </typeparam>
        /// <param name="provider">
        /// The type of InMemory database provider to use. The Type is defined by the enumeration
        /// <see cref="InMemoryProvider" />.
        /// </param>
        /// <param name="sqliteConnection">
        /// A connection for a SQLite database. It must be passed in when a SQLite database is used.
        /// </param>
        /// <param name="seedAction">
        /// Method to seed test data.
        /// </param>
        /// <returns>
        /// DbContext of an in memory test database.
        /// </returns>
        public static TDbContext CreateDbContext<TDbContext>(
            InMemoryProvider provider = InMemoryProvider.EFCore,
            SqliteConnection? sqliteConnection = default,
            Action<TDbContext>? seedAction = default)
            where TDbContext : DbContext
        {
            var options = CreateCbContextOptions<TDbContext>(provider, sqliteConnection);
            var dbContext = Activator.CreateInstance(typeof(TDbContext), options) as TDbContext;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            dbContext.Database.EnsureCreated();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (seedAction is not null)
                seedAction(dbContext);

            return dbContext;
        }

        /// <summary>
        /// Creates a SQLite database connection string for an InMemory database.
        /// </summary>
        /// <returns>
        /// <see cref="SqliteConnection" /> for an InMemory database.
        /// </returns>
        public static SqliteConnection CreateSqliteInMemoryConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:;Foreign Keys=False;");
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Destroys the given database context.
        /// </summary>
        /// <typeparam name="TDbContext">
        /// The type of the database context.
        /// </typeparam>
        /// <param name="context">
        /// The database context to destroy.
        /// </param>
        public static void Destroy<TDbContext>(TDbContext context)
            where TDbContext : DbContext
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        /// <summary>
        /// Returns DbContextOptions used to configure an in memory test database DbContext.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the DbContext.
        /// </typeparam>
        /// <param name="provider">
        /// The type of InMemory database provider to use. The Type is defined by the enumeration
        /// <see cref="InMemoryProvider" />.
        /// </param>
        /// <param name="sqliteConnection">
        /// A connection for a SQLite database. It must be passed in when a SQLite database is used.
        /// </param>
        /// <returns>
        /// DbContextOptions to configure an in memory test database.
        /// </returns>
        private static DbContextOptions<T> CreateCbContextOptions<T>(
            InMemoryProvider provider,
            SqliteConnection? sqliteConnection = null)
            where T : DbContext
        {
            if (sqliteConnection is null
                && provider == InMemoryProvider.SQLite)
                throw new ArgumentNullException(nameof(sqliteConnection));

            DbContextOptionsBuilder<T>? builder = null;

            if (provider == InMemoryProvider.EFCore)
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                builder = new DbContextOptionsBuilder<T>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .UseInternalServiceProvider(serviceProvider);
            }

            if (provider == InMemoryProvider.SQLite)
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlite()
                    .BuildServiceProvider();

#pragma warning disable CS8604 // Possible null reference argument.
                builder = new DbContextOptionsBuilder<T>()
                    .UseSqlite(sqliteConnection)
                    .UseInternalServiceProvider(serviceProvider);
#pragma warning restore CS8604 // Possible null reference argument.
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return builder.Options;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        #endregion Methods
    }
}