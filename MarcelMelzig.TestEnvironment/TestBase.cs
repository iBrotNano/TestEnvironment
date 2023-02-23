namespace TestEnvironment
{
    /// <summary>
    /// The class ist the basic test class implementing some fixture for test cases.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The class makes it possible to setup some common test fixture. You can define a
    /// configuration, a service provider and a database context in memory. Those components can be
    /// accessed via properties. Also <see cref="FixtureFactory" /> offers a single point to access
    /// factory method to create fixtures for single test cases.
    /// </para>
    /// </remarks>
    /// <typeparam name="TDbContext">
    /// The type of the <see cref="DbContext" /> used for tests.
    /// </typeparam>
    /// <typeparam name="TLoggerCategory">
    /// A type, used as a category for the <see cref="ILogger" /> interface. The class mocks a
    /// logger in the service provider.
    /// </typeparam>
    public class TestBase<TDbContext, TLoggerCategory> : IDisposable
        where TDbContext : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of the class.
        /// </summary>
        /// <param name="configurationSource">
        /// A configuration is build in memory from those key value pairs.
        /// </param>
        /// <param name="provider">
        /// Defines the database provider used by EF Core. <see cref="InMemoryProvider" /> defines
        /// the valid types.
        /// </param>
        /// <param name="sqliteConnection">
        /// A connection if SQLite is used to create integrations tests.
        /// </param>
        /// <param name="seedAction">
        /// An action used to seed data into the database. It might be helpful to build a class with
        /// seed data in your test project.
        /// </param>
        /// <param name="providedServices">
        /// Those services are registered into the service provider.
        /// </param>
        public TestBase(Dictionary<string, string?>? configurationSource = default,
            InMemoryProvider provider = InMemoryProvider.EFCore,
            SqliteConnection? sqliteConnection = default,
            Action<TDbContext>? seedAction = default,
            params (Type ServiceInterface, object MockObject)[] providedServices)
        {
            DbContext = Database.CreateDbContext(provider, sqliteConnection, seedAction);

            Configuration = configurationSource == default ?
                FixtureFactory.CreateConfiguration(new Dictionary<string, string?>()) :
                FixtureFactory.CreateConfiguration(configurationSource);

            ServiceProvider = FixtureFactory.CreateServiceProviderMock<TLoggerCategory>(providedServices);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// A stub to test paths.
        /// </summary>
        public string PathStub = Consts.PathStub;

        /// <summary>
        /// A temporary data folder for integration tests. Don't use it in unit tests. Mock the file
        /// system instead.
        /// </summary>
        public DirectoryInfo ApplicationCommonDataPath => Environment.ApplicationCommonDataPath;

        /// <summary>
        /// An in memory configuration used for tests.
        /// </summary>
        public IConfigurationRoot? Configuration { get; }

        /// <summary>
        /// An in memory database used for tests.
        /// </summary>
        public TDbContext DbContext { get; }

        /// <summary>
        /// A single point to access factory methods to build test fixtures.
        /// </summary>
        public FixtureFactory FixtureFactory => new();

        /// <summary>
        /// A service provider used in tests.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Disposes instances of this class.
        /// </summary>
        public void Dispose()
        {
            Database.Destroy(DbContext);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Setup of a mocked context for <see cref="IAsyncActionFilter" /> s.
        /// </summary>
        /// <param name="context">
        /// The <see cref="ActionExecutingContext" /> mocked by this setup.
        /// </param>
        /// <param name="actionExecutedContext">
        /// The <see cref="ActionExecutedContext" /> mocked by this setup.
        /// </param>
        /// <param name="executionDelegate">
        /// The <see cref="ActionExecutionDelegate" /> mocked by this setup is the next method in
        /// the pipeline.
        /// </param>
        /// <param name="httpMethod">
        /// The HTTPMethod used in the filter. I use this method to write validation filters for a
        /// API policy. The default ist <c>null</c>.
        /// </param>
        /// <param name="actionArguments">
        /// ActionArguments of the context. Default is <c>null</c>.
        /// </param>
        public void SetupIAsyncActionFilterContext(out ActionExecutingContext context,
            out ActionExecutedContext actionExecutedContext,
            out ActionExecutionDelegate executionDelegate,
            string? httpMethod = null,
            IDictionary<string, object>? actionArguments = null)
        {
            Web.SetupIAsyncActionFilterContext(out context, out actionExecutedContext, out executionDelegate, httpMethod, actionArguments);
        }

        #endregion Methods
    }
}