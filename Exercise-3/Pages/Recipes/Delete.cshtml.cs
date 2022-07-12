using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Recipes
{
    public class DeleteModel : PageModel
    {
        private IConfiguration _config { get; set; }
        public Recipe FetchedRecipe { get; set; } = new();
        public IEnumerable<string> DetailedIngredients { get; set; } = new List<string>();
        public IEnumerable<string> DetailedInstructions { get; set; } = new List<string>();
        public IEnumerable<string> DetailedCategories { get; set; } = new List<string>();

        public DeleteModel(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            HttpClient client = new HttpClient();
            var request = await client.GetFromJsonAsync<Recipe>(_config["url"] + "recipes/" + id);
            if (request is null)
                return NotFound(); ;
            FetchedRecipe = request;
            DetailedIngredients = FetchedRecipe.Ingredients.Split("\n").Select(x => $"{x}");
            DetailedInstructions = FetchedRecipe.Instructions.Split("\n").Select((x, n) => $"- {x}");
            DetailedCategories = FetchedRecipe.Categories.Select((x, n) => $"- {x}");
            return Page();
                
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            HttpClient client = new HttpClient();
            var request = await client.DeleteAsync(_config["url"]+$"recipes/"+id);
            return RedirectToPage("./Index");
        }
    }
}
