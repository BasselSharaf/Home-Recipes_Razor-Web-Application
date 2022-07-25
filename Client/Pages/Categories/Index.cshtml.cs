using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IConfiguration _config { get; set; }
        public List<string> Categories { get; set; } = new();
        public IndexModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("Recipes");
            var request = await client.GetFromJsonAsync<List<string>>("categories");
            if (request == null)
                return NotFound();
            Categories = request;
            return Page();
        }
    }
}
