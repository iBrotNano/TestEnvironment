namespace TestEnvironment
{
    /// <summary>
    /// Fixtures for validation.
    /// </summary>
    public class Validation
    {
        #region Methods

        /// <summary>
        /// Erzeugt ein Mock eines Validators, der beim Aufruf von ValidateAndThrow eine Exception wirft.
        /// </summary>
        /// <typeparam name="TValidator">
        /// Typ des Validators.
        /// </typeparam>
        /// <returns>
        /// Validator vom Typ <typeparamref name="TValidator" />.
        /// </returns>
        public static IValidator<TValidator> CreateFailingValidatorMock<TValidator>()
        {
            var mockValidator = new Mock<IValidator<TValidator>>();

            var failureResult = new ValidationResult(new List<ValidationFailure>() {
                new ValidationFailure("Foo", "Bar")
            });

            // Setup the Validate/ValidateAsync overloads that take an instance. These will never
            // throw exceptions.
            mockValidator.Setup(p => p.Validate(It.IsAny<TValidator>()))
              .Returns(failureResult).Verifiable();

            mockValidator.Setup(p =>
                p.ValidateAsync(It.IsAny<TValidator>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(failureResult);

            // Setup the Validate/ValidateAsync overloads that take a context. This is the method
            // called by ValidateAndThrow, so will potentially support throwing the exception. Setup
            // method invocations for with an exception and without.
            mockValidator.Setup(p =>
                p.Validate(It.Is<ValidationContext<TValidator>>(context => context.ThrowOnFailures)))
                .Throws(new ValidationException(failureResult.Errors));

            mockValidator.Setup(p =>
                p.ValidateAsync(It.Is<ValidationContext<TValidator>>(context => context.ThrowOnFailures),
                    It.IsAny<CancellationToken>()))
                .Throws(new ValidationException(failureResult.Errors));

            // If ThrowOnFailures is false, return the result.
            mockValidator.Setup(p =>
                p.Validate(It.Is<ValidationContext<TValidator>>(context => !context.ThrowOnFailures)))
                .Returns(failureResult).Verifiable();

            mockValidator.Setup(p =>
                p.ValidateAsync(It.Is<ValidationContext<TValidator>>(context => !context.ThrowOnFailures),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(failureResult);

            return mockValidator.Object;
        }

        #endregion Methods
    }
}