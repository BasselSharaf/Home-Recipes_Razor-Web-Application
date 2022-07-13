using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public IConfiguration Config { get; set; }
        public List<Recipe> Recipes { get; set; } = new();
        public IndexModel(IConfiguration config,
                         ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            Config = config;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            _logger.LogInformation("About page visited at {DT}",
                DateTime.Now.ToString());
            var httpClient = _httpClientFactory.CreateClient();
            var fetchRecipes = await httpClient.GetFromJsonAsync<List<Recipe>>("recipes");
            if (fetchRecipes is not null)
                Recipes = fetchRecipes;
        }
    }
}
