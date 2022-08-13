using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Exercise3.Pages;
namespace Exercise3.Pages.Recipes
{
    public class DetailModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Recipe FetchedRecipe { get; set; } = new();
        public IEnumerable<string> DetailedIngredients { get; set; } = new List<string>();
        public IEnumerable<string> DetailedInstructions { get; set; } = new List<string>();

        public DetailModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
        
        public async Task<IActionResult> OnGet(Guid? id)
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var request = await client.GetFromJsonAsync<Recipe>("recipes/"+id);
            if (request is not null)
            {
                FetchedRecipe = request;
                DetailedIngredients = FetchedRecipe.Ingredients.Split("\n").Select(x => $"{x}");
                DetailedInstructions = FetchedRecipe.Instructions.Split("\n").Select((x, n) => $"{x}");
                return Page();
            }
            else
                return NotFound();
        }
    }
}
