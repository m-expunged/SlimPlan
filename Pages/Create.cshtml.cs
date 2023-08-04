using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlimPlan.Models;
using SlimPlan.Services;
using System.ComponentModel.DataAnnotations;

namespace SlimPlan.Pages
{
    public class CreateModel : PageModel
    {
        #region Fields

        private readonly PlansService _plans;

        #endregion Fields

        #region Properties

        [Required]
        [BindProperty]
        public string Name { get; set; } = string.Empty;

        #endregion Properties

        #region Constructors

        public CreateModel(PlansService plans)
        {
            _plans = plans;
        }

        #endregion Constructors

        #region Methods

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Name) || _plans.Exists(Name))
            {
                return Page();
            }

            _plans.WriteToFile(new Plan(Name));

            return RedirectToPage("./Index");
        }

        #endregion Methods
    }
}
