using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]
        public string Category { get; set; }

        public DeleteModel (IConfiguration config,IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            Category = "";
        }

        public void OnGet(string title)
        {
            Category = title;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var request = await client.DeleteAsync($"categories?category={Category}");
            if (request.IsSuccessStatusCode)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}
