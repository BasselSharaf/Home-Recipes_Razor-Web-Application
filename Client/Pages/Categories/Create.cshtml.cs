using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]
        public string Category { get; set; } = default!;
        public CreateModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var request = await client.PostAsync($"categories?category={Category}",null);
            if(request.IsSuccessStatusCode)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}
