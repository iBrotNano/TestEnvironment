namespace TestEnvironment
{
    /// <summary>
    /// Code to write tests with dependency injection.
    /// </summary>
    public class DependencyInjection
    {
        #region Methods

        /// <summary>
        /// Return a mock of a IServiceProvider used in .NET Core application.
        /// </summary>
        /// <typeparam name="TLoggerCategory">
        /// Name of the category used by the logger.
        /// </typeparam>
        /// <param name="providedServices">
        /// An array of tuples with a <see cref="Type" /> of a service as the first parameter and a
        /// mock of the service as an <see cref="object" />.
        /// </param>
        public static IServiceProvider CreateServiceProviderMock<TLoggerCategory>(
            params (Type ServiceInterface, object MockObject)[] providedServices)
        {
            var logger = Mock.Of<ILogger<TLoggerCategory>>();

            var serviceProviderMock = Mock.Of<IServiceProvider>(serviceProvider =>
                serviceProvider.GetService(typeof(ILogger<TLoggerCategory>)) == logger);

            foreach (var (ServiceInterface, MockObject) in providedServices)
                Mock.Get(serviceProviderMock)
                    .Setup(spm => spm.GetService(ServiceInterface))
                    .Returns(MockObject);

            return serviceProviderMock;
        }

        #endregion Methods
    }
}