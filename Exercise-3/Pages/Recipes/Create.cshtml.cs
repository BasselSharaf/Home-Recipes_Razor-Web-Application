using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Exercise_3.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        ILogger<CreateModel> logger;
        private readonly IConfiguration _config;
        public static HttpClient s_httpClient = new();
        public Recipe Recipe { get; set; } = new();

        public CreateModel(IConfiguration config, ILogger<CreateModel> logger)
        {
            _config = config;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var fetchCategories = await s_httpClient.GetFromJsonAsync<List<string>>(_config["url"] + "categories");
            if (fetchCategories == null)
                return NotFound();
            Recipe.Categories = fetchCategories;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid || Recipe == null)
                return Page();
            var request = await s_httpClient.PostAsJsonAsync<Recipe>(_config["url"]+"recipes",Recipe);
            if(request.IsSuccessStatusCode)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}