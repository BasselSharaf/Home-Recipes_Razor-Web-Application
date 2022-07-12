using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public static HttpClient s_httpClient = new();
        public IConfiguration Config { get; set; }
        public List<Recipe> Recipes { get; set; } = new();
        public IndexModel(IConfiguration config,
                         ILogger<IndexModel> logger)
        {
            Config = config;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            _logger.LogInformation("About page visited at {DT}",
                DateTime.Now.ToString());
            var fetchRecipes = await s_httpClient.GetFromJsonAsync<List<Recipe>>(Config["url"] + "recipes");
            if (fetchRecipes is not null)
                Recipes = fetchRecipes;
        }
    }
}
