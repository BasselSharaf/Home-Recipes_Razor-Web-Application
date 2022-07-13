using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]
        public Recipe Recipe { get; set; } = new();
        public EditModel(IHttpClientFactory httpClientFactory) =>_httpClientFactory= httpClientFactory;
        

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("Recipes");
            var req = await httpClient.GetFromJsonAsync<Recipe>("recipes/" + id);
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
