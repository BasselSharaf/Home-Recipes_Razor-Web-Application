using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IConfiguration Config { get; set; }
        public List<Recipe> Recipes { get; set; } = new();
        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task OnGetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("Recipes");
            var fetchRecipes = await httpClient.GetFromJsonAsync<List<Recipe>>("recipes");
            if (fetchRecipes is not null)
                Recipes = fetchRecipes;
        }
    }
}
