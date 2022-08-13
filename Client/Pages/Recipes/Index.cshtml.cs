using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<Recipe> Recipes { get; set; } = new();
        public string? ChoosenCategory { get; set; }
        public IndexModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task OnGetAsync(string? category)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var fetchRecipes = await client.GetFromJsonAsync<List<Recipe>>("recipes");
            if (fetchRecipes is not null)
            {
                if (category is not null)
                {
                    fetchRecipes = fetchRecipes.Where(r => r.Categories.Contains(category)).ToList();
                    ChoosenCategory = category;
                }
                Recipes = fetchRecipes;
            }
                
        }
    }
}
