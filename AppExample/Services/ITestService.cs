using AppExample.Entities;

namespace AppExample.Services
{
    public interface ITestService
    {
        #region Methods

        Task<Item> GetAllAsync();

        Task SaveItemAsync(Item item);

        #endregion Methods
    }
}