using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Exercise_3.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        ILogger<CreateModel> logger;
        private readonly IConfiguration _config;
        public static HttpClient s_httpClient = new();
        [BindProperty]
        public Recipe Recipe { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();
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
            Categories = fetchCategories.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a,
                    Value = a,
                    Selected = false
                };
            });
            Categories.ForEach(c => Console.WriteLine(c));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Categories.ForEach(x => Recipe.Categories.Add(x.Value));
            Categories.ForEach(c => Console.WriteLine(c));
            if(!ModelState.IsValid || Recipe == null)
                return Page();
            HttpClient client = new HttpClient();
            var request = await client.PostAsJsonAsync<Recipe>(_config["url"]+"recipes",Recipe);
            return RedirectToPage("./Index");
        }
    }
}
