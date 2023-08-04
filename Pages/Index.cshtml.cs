using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlimPlan.Services;

namespace SlimPlan.Pages
{
    public class IndexModel : PageModel
    {
        #region Fields

        private readonly ILogger<IndexModel> _logger;
        private readonly PlansService _plans;

        #endregion Fields

        #region Properties

        public List<string>? Plans { get; private set; }

        #endregion Properties

        #region Constructors

        public IndexModel(ILogger<IndexModel> logger, PlansService planService)
        {
            _logger = logger;
            _plans = planService;
        }

        #endregion Constructors

        #region Methods

        public void OnGet()
        {
            Plans = _plans.GetNames();
        }

        public IActionResult OnPostDelete(string name)
        {
            _plans.Delete(name);

            return RedirectToPage();
        }

        #endregion Methods
    }
}
