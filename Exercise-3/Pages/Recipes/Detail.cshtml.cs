using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Exercise_3.Pages;
namespace Exercise_3.Pages.Recipes
{
    public class DetailModel : PageModel
    {
        private readonly IConfiguration _config;
        public Recipe FetchedRecipe { get; set; } = new();
        public IEnumerable<string> DetailedIngredients { get; set; } = new List<string>();
        public IEnumerable<string> DetailedInstructions { get; set; } = new List<string>();
        public IEnumerable<string> DetailedCategories { get; set; } = new List<string>();

        public DetailModel(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            HttpClient client = new HttpClient();
            var request = await client.GetFromJsonAsync<Recipe>(_config["url"]+"recipes/"+id);
            if (request is not null)
            {
                FetchedRecipe = request;
                DetailedIngredients = FetchedRecipe.Ingredients.Split(",").Select(x => $"{x}");
                DetailedInstructions = FetchedRecipe.Instructions.Split(",").Select((x, n) => $"- {x}");
                DetailedCategories = FetchedRecipe.Categories.Select((x, n) => $"- {x}");
                return Page();
            }
            else
                return NotFound();
        }
    }
}
