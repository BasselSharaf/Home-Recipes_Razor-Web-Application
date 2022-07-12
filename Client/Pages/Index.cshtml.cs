using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
namespace Exercise3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IConfiguration _config{ get; set; }
        public List<Recipe> Recipes { get; set; } = new();
        public IndexModel(IConfiguration config,
                         ILogger<IndexModel> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var httpClient = new HttpClient();
            var fetchRecipes = await httpClient.GetFromJsonAsync<List<Recipe>>(_config["url"]+"recipes");
            if (fetchRecipes is not null)
                Recipes = fetchRecipes;
        }
    }
}