using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_3.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        ILogger<CreateModel> logger;
        public readonly IConfiguration Config;
        public Recipe? Recipe { get; set; }
        public CreateModel(IConfiguration config, ILogger<CreateModel> logger)
        {
            Config = config;
            this.logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid || Recipe == null)
                return Page();
            HttpClient client = new HttpClient();
            var request = await client.PostAsJsonAsync<Recipe>(Config["url"]+"recipes",Recipe);
            return RedirectToPage("./Index");
        }
    }
}
