using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_3.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private IConfiguration _config { get; set; }
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

        public async Task<IActionResult> OnPostAsync(string category)
        {
            HttpClient client = new HttpClient();
            var request = await client.DeleteAsync(_config["url"] + $"categories?category={category}");
            if (request.IsSuccessStatusCode)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}
