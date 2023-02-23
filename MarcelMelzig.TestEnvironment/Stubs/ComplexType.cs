namespace TestEnvironment
{
    /// <summary>
    /// A complex type to used in a method as a stub.
    /// </summary>
    /// <remarks>
    /// <para>It can be used to test generic types for example.</para>
    /// </remarks>
    public class ComplexType
    {
        #region Properties

        /// <summary>
        /// A nested type.
        /// </summary>
        public InnerType Inner { get; set; } = new InnerType
        {
            TestString = "InnerTestString"
        };

        /// <summary>
        /// A test string.
        /// </summary>
        public string TestString { get; set; } = nameof(TestString);

        #endregion Properties
    }
}