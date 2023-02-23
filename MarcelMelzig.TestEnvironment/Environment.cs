namespace TestEnvironment
{
    /// <summary>
    /// Offers information about the environment.
    /// </summary>
    public class Environment
    {
        #region Properties

        /// <summary>
        /// Returns a <see cref="DirectoryInfo" /> of the folder used for storing test data.
        /// </summary>
        public static DirectoryInfo ApplicationCommonDataPath
        {
            get
            {
                return Directory.CreateDirectory(
                    Path.Combine(
                        System.Environment.GetFolderPath(
                            System.Environment.SpecialFolder.CommonApplicationData),
                            Consts.TestEnvironmentDataFolder));
            }
        }

        #endregion Properties
    }
}