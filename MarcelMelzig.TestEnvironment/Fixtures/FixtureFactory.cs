namespace TestEnvironment
{
    /// <summary>
    /// A factory class used a single point to access all factory methods of the project.
    /// </summary>
    public class FixtureFactory
    {
        #region Methods

#pragma warning disable CA1822 // Mark members as static

        /// <summary>
        /// Create a complex type used as a stub for tests.
        /// </summary>
        /// <returns>
        /// An instance of a class with a nested type.
        /// </returns>
        public ComplexType CreateComplexTypeStub() => new();

        /// <summary>
        /// Creates a <see cref="IConfigurationRoot" /> in memory for test purposes.
        /// </summary>
        /// <param name="configurationSource">
        /// The configurationtion data.
        /// </param>
        /// <returns>
        /// An <see cref="IConfigurationRoot" />.
        /// </returns>
        public IConfigurationRoot CreateConfiguration(Dictionary<string, string?>? configurationSource) =>
            Configuration.CreateConfiguration(configurationSource);

        /// <summary>
        /// Creates a failing <see cref="IValidator" /> for test purposes.
        /// </summary>
        /// <typeparam name="TValidator">
        /// The type of the validator.
        /// </typeparam>
        /// <returns>
        /// A failing validator.
        /// </returns>
        public IValidator<TValidator> CreateFailingValidatorMock<TValidator>() =>
            Validation.CreateFailingValidatorMock<TValidator>();

        /// <summary>
        /// Creates a fixture of an <see cref="PageModel" />.
        /// </summary>
        /// <typeparam name="TPageModel">
        /// The type of the <see cref="PageModel" />.
        /// </typeparam>
        /// <param name="args">
        /// Arguments passed into the constructor.
        /// </param>
        /// <returns>
        /// A fixture of an <see cref="PageModel" />.
        /// </returns>
        public TPageModel CreatePageModelFixture<TPageModel>(object?[]? args = null)
            where TPageModel : PageModel => Web.CreatePageModelFixture<TPageModel>(args);

        /// <summary>
        /// Creates a collection of random file names.
        /// </summary>
        /// <returns>
        /// A collection of random file names.
        /// </returns>
        public IEnumerable<string> CreateRandomInvalidFileNames() =>
            FileSystem.CreateRandomInvalidFileNames();

        /// <summary>
        /// Create a service provider mock.
        /// </summary>
        /// <typeparam name="TLoggerCategory">
        /// The type of an category of an <see cref="ILogger" />. A instance of such a logger will
        /// be registered in the service provider.
        /// </typeparam>
        /// <param name="providedServices">
        /// Services to be registered in the service provider.
        /// </param>
        /// <returns>
        /// A mock of an service provider.
        /// </returns>
        public IServiceProvider CreateServiceProviderMock<TLoggerCategory>(
            params (Type ServiceInterface, object MockObject)[] providedServices) =>
            DependencyInjection.CreateServiceProviderMock<TLoggerCategory>(providedServices);

#pragma warning restore CA1822 // Mark members as static

        #endregion Methods
    }
}