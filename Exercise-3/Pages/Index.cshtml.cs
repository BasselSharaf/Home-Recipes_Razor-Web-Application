using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
namespace Exercise_3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public static HttpClient s_httpClient = new();
        public static IConfiguration s_config{ get; set; }
        public List<Recipe> Recipes { get; set; } = new();
        public IndexModel(IConfiguration config,
                         ILogger<IndexModel> logger)
        {
            s_config = config;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var fetchRecipes = await s_httpClient.GetFromJsonAsync<List<Recipe>>(s_config["url"]+"recipes");
            if (fetchRecipes is not null)
                Recipes = fetchRecipes;
        }
    }
}