using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<Recipe> Recipes { get; set; } = new();
        public IndexModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var fetchRecipes = await client.GetFromJsonAsync<List<Recipe>>("recipes");
            if (fetchRecipes is not null)
                Recipes = fetchRecipes;
        }
    }
}
