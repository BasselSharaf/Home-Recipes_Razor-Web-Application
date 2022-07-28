using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]
        public Recipe Recipe { get; set; } = new();
        [BindProperty]
        public List<string> Categories { get; set; } = new();
        public EditModel(IHttpClientFactory httpClientFactory) =>_httpClientFactory= httpClientFactory;
        

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var categoriesReq = await client.GetFromJsonAsync<List<string>>("categories");
            var recipeReq = await client.GetFromJsonAsync<Recipe>("recipes/" + id);
            if (recipeReq == null || categoriesReq == null)
                return NotFound();
            Recipe = recipeReq;
            Categories = categoriesReq;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var request = await client.PutAsJsonAsync<Recipe>("recipes/"+Recipe.Id,Recipe);
            if (request == null || !ModelState.IsValid)
                return Page();
            return RedirectToPage("./Index");
        }
    }
}
