using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_3.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private IConfiguration _config { get; set; }
        public List<string> Categories { get; set; } = new();
        public IndexModel(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = new HttpClient();
            var request = await httpClient.GetFromJsonAsync<List<string>>(_config["url"] + "categories");
            if (request == null)
                return NotFound();
            Categories = request;
            return Page();
        }
    }
}
