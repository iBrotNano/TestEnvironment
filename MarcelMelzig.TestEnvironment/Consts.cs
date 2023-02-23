namespace TestEnvironment
{
    /// <summary>
    /// This class encapsulates constant values used in the test environment.
    /// </summary>
    public static class Consts
    {
        #region Fields

        /// <summary>
        /// The name is used to identify a collection of xUnit tests to test read scenarios for a database.
        /// </summary>
        public const string DatabaseReadTestsCollectionDefinitionName = "DatabaseReadTests";

        /// <summary>
        /// A stub path to use in tests.
        /// </summary>
        public const string PathStub = @"C:\Test";

        /// <summary>
        /// Name of the folder used to store test data.
        /// </summary>
        public const string TestEnvironmentDataFolder = "TestEnvironment";

        #endregion Fields
    }
}