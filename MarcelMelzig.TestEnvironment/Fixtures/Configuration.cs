namespace TestEnvironment
{
    /// <summary>
    /// Fixtures for configuration tasks.
    /// </summary>
    public class Configuration
    {
        #region Methods

        /// <summary>
        /// Returns an in memory configuration use by .NET Core apps.
        /// </summary>
        /// <param name="configurationSource">
        /// A dictionary with configuration data.
        /// </param>
        /// <returns>
        /// A <see cref="IConfigurationRoot" /> instance to stub configuration for unit tests.
        /// </returns>
        public static IConfigurationRoot CreateConfiguration(Dictionary<string, string?>? configurationSource)
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(configurationSource)
                .Build();
        }

        #endregion Methods
    }
}