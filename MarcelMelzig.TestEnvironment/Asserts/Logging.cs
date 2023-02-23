namespace TestEnvironment
{
    /// <summary>
    /// Asserts for logging.
    /// </summary>
    public static class Logging
    {
        #region Methods

        /// <summary>
        /// Prüft ob ein Logger-Mock mit einer gegebenen Nachricht aufgerufen wird, um das Logging
        /// einer Klasse zu testen.
        /// </summary>
        /// <typeparam name="TCategoryName">
        /// Kategorie des Loggers.
        /// </typeparam>
        /// <param name="logger">
        /// Der Mock einer <see cref="ILogger" />.
        /// </param>
        /// <param name="logLevel">
        /// Der LogLevel, der für eine Log-Operation genutzt wird.
        /// </param>
        /// <param name="message">
        /// Das Template einer Nachricht, die geloggt werden soll.
        /// </param>
        /// <param name="times">
        /// Anzahl der Aufrufe.
        /// </param>
        public static void ShouldLog<TCategoryName>(this ILogger<TCategoryName> logger,
            LogLevel logLevel,
            string message,
            Times times)
        {
            ShouldLog(Mock.Get(logger), logLevel, message, times);
        }

        /// <summary>
        /// Prüft ob ein Logger-Mock mit einer gegebenen Nachricht aufgerufen wird, um das Logging
        /// einer Klasse zu testen.
        /// </summary>
        /// <typeparam name="TCategoryName">
        /// Kategorie des Loggers.
        /// </typeparam>
        /// <param name="logger">
        /// Der Mock einer <see cref="ILogger" />.
        /// </param>
        /// <param name="logLevel">
        /// Der LogLevel, der für eine Log-Operation genutzt wird.
        /// </param>
        /// <param name="message">
        /// Das Template einer Nachricht, die geloggt werden soll.
        /// </param>
        /// <param name="times">
        /// Anzahl der Aufrufe.
        /// </param>
        public static void ShouldLog<TCategoryName>(this Mock<ILogger<TCategoryName>> logger,
            LogLevel logLevel,
            string message,
            Times times)
        {
            logger
                .Verify(l =>
                    l.Log(It.Is<LogLevel>(l => l == logLevel),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString() == message),
                        It.IsAny<Exception>(),
                        It.IsAny<Func<It.IsAnyType, Exception?, string>>()), times);
        }

        #endregion Methods
    }
}