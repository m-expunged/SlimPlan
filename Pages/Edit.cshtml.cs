using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlimPlan.Models;
using SlimPlan.Services;
using System.ComponentModel.DataAnnotations;

namespace SlimPlan.Pages
{
    public class EditModel : PageModel
    {
        #region Fields

        private readonly PlansService _plans;

        #endregion Fields

        #region Properties

        public Plan? Plan { get; private set; }

        public List<Day>? Week { get; private set; }

        [Required]
        [BindProperty]
        public string Note { get; set; } = string.Empty;

        #endregion Properties

        #region Constructors

        public EditModel(PlansService plans)
        {
            _plans = plans;
        }

        #endregion Constructors

        #region Methods

        public IActionResult OnGet(string name, string date)
        {
            var day = DateTime.Parse(date);

            Plan = _plans.GetPlan(name);

            if (Plan is null || day == DateTime.MinValue)
            {
                return RedirectToPage("./Index");
            }

            Week = Plan.GetOrAddDays(day);

            return Page();
        }

        public IActionResult OnPostDelete(string name, string date, Guid id)
        {
            var day = DateTime.Parse(date);

            if (day == DateTime.MinValue)
            {
                return Page();
            }

            var plan = _plans.GetPlan(name);

            if (plan is null)
            {
                return Page();
            }

            plan.TryRemoveNote(day, id);

            _plans.WriteToFile(plan);

            return RedirectToPage(new { name, date });
        }

        public IActionResult OnPostAdd(string name, string date, int index)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var day = DateTime.Parse(date);

            if (string.IsNullOrWhiteSpace(name) || day == DateTime.MinValue)
            {
                return Page();
            }

            var plan = _plans.GetPlan(name);

            if (plan is null)
            {
                return Page();
            }

            var days = plan.GetOrAddDays(day);

            days[index].AddNote(Note);

            plan.Days.RemoveAll(d => !d.Notes.Any());

            _plans.WriteToFile(plan);

            return RedirectToPage(new { name, date });
        }

        #endregion Methods
    }
}
