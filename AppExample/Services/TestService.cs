using AppExample.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AppExample.Services
{
    public class TestService : ITestService
    {
        #region Fields

        private readonly AppDbContext appDbContext;
        private readonly IOptions<TestServiceOptions> options;
        private readonly IValidator<TestServiceOptions> optionsValidator;

        #endregion Fields

        #region Constructors

        public TestService(IOptions<TestServiceOptions> options,
            IValidator<TestServiceOptions> optionsValidator,
            AppDbContext appDbContext)
        {
            this.options = options;
            this.optionsValidator = optionsValidator;
            this.optionsValidator.ValidateAndThrow(this.options.Value);
            this.appDbContext = appDbContext;
        }

        #endregion Constructors

        #region Methods

        public Task<Item> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveItemAsync(Item item)
        {
            if (options.Value.Skip)
                return;

            appDbContext.Items.Add(item);
            await appDbContext.SaveChangesAsync();
        }

        #endregion Methods
    }
}