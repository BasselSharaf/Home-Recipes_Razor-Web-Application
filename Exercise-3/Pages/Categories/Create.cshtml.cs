using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Categories
{
    public class CreateModel : PageModel
    {
        ILogger<CreateModel> logger;
        private readonly IConfiguration _config;
        [BindProperty]
        public string Category { get; set; } = default!;
        public CreateModel(IConfiguration config, ILogger<CreateModel> logger)
        {
            _config = config;
            this.logger = logger;
        }
        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = new HttpClient();
            var request = await httpClient.PostAsync(_config["url"] + $"categories?category={Category}",null);
            if(request.IsSuccessStatusCode)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}
