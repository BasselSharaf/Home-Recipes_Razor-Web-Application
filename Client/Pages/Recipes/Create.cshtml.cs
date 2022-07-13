using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Exercise3.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]
        public Recipe Recipe { get; set; } = new();

        public CreateModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var fetchCategories = await client.GetFromJsonAsync<List<string>>("categories");
            if (fetchCategories == null)
                return NotFound();
            Recipe.Categories = fetchCategories;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid || Recipe == null)
                return Page();
            var client = _httpClientFactory.CreateClient("Recipes");
            var request = await client.PostAsJsonAsync<Recipe>("recipes",Recipe);
            if(request.IsSuccessStatusCode)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}