namespace TestEnvironment
{
    /// <summary>
    /// Fixtures for the file system.
    /// </summary>
    public class FileSystem
    {
        #region Methods

        /// <summary>
        /// Returns a list of invalid file paths as test data.
        /// </summary>
        /// <returns>
        /// A list of invalid file paths.
        /// </returns>
        public static IEnumerable<string> CreateRandomInvalidFileNames()
        {
            var invalidChars = Path.GetInvalidPathChars();

            foreach (var invalidChar in invalidChars)
            {
                string randomFileName = Path.GetRandomFileName();

                yield return Path.Combine(Path.GetTempPath(),
                    Path.GetFileNameWithoutExtension(randomFileName)
                    + invalidChar
                    + Path.GetExtension(randomFileName));
            }
        }

        #endregion Methods
    }
}