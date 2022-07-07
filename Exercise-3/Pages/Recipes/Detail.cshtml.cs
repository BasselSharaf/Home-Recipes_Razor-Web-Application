using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Exercise_3.Pages;
namespace Exercise_3.Pages.Recipes
{
    public class DetailModel : PageModel
    {
        public Recipe FetchedRecipe { get; set; }
        public IEnumerable<string> DetailedIngredients { get; set; }
        public IEnumerable<string> DetailedInstructions { get; set; }
        public IEnumerable<string> DetailedCategories { get; set; }

        public IConfiguration Config { get; set; }
        public DetailModel(IConfiguration config)
        {
            Config = config;
        }
        public async Task OnGet(Guid id)
        {
            HttpClient client = new HttpClient();
            Console.WriteLine(Config["url"] + id);
            var request = await client.GetFromJsonAsync<Recipe>(Config["url"]+"recipes/"+id);
            if (request is not null)
            {
                FetchedRecipe = request;
                DetailedIngredients = FetchedRecipe.Ingredients.Split(",").Select(x => $"{x}");
                DetailedInstructions = FetchedRecipe.Instructions.Split(",").Select((x, n) => $"- {x}");
                DetailedCategories = FetchedRecipe.Categories.Select((x, n) => $"- {x}");
            }
            else
                RedirectToPage("./NotFound");
        }
    }
}
