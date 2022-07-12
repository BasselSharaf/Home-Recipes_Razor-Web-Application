using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private IConfiguration _config { get; set; }
        [BindProperty]
        public Recipe Recipe { get; set; } = new();
        public EditModel(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var httpClient = new HttpClient();
            var req = await httpClient.GetFromJsonAsync<Recipe>(_config["url"] + "recipes/" + id);
            if (req == null)
                return NotFound();
            Recipe = req;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = new HttpClient();
            var request = await httpClient.PutAsJsonAsync<Recipe>(_config["url"] +"recipes/"+Recipe.Id,Recipe);
            if (request == null || !ModelState.IsValid)
                return Page();
            return RedirectToPage("./Index");
        }
    }
}
