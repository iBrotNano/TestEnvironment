using AppExample.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppExample.Pages
{
    public class IndexModel : PageModel
    {
        #region Fields

        private readonly ILogger<IndexModel> _logger;

        #endregion Fields

        #region Constructors

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        #endregion Constructors

        #region Methods

        public IActionResult OnGet()
        {
            _logger.LogInformation("This is useless information.");
            return Page();
        }

        public IActionResult OnPostSaveItemAsync(Item item)
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                _logger.LogDebug("Saving item {name} [{id}].", item.Name, item.Id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An exception occurred during action.");
                ModelState.AddModelError(string.Empty, "ErrorMessage");
            }

            return Page();
        }

        #endregion Methods
    }
}