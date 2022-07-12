using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private IConfiguration _config { get; set; }
        [BindProperty]
        public string Category { get; set; }

        public DeleteModel (IConfiguration config)
        {
            _config = config;
            Category = "";
        }

        public void OnGet(string title)
        {
            Category = title;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            HttpClient client = new HttpClient();
            var request = await client.DeleteAsync(_config["url"] + $"categories?category={Category}");
            if (request.IsSuccessStatusCode)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}
