using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise_3.Pages.Categories
{
    public class EditModel : PageModel
    {
        private IConfiguration _config { get; set; }
        [BindProperty]
        public string Category { get; set; }
        [BindProperty]
        public string NewCategory { get; set; }
        public EditModel(IConfiguration config)
        {
            _config = config;
            Category = "";
            NewCategory = "";   
        }

        public void OnGet(string title)
        {
            Category = title;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            HttpClient client = new HttpClient();
            var request = await client.PutAsync(_config["url"] + $"categories?category={Category}&newCategory={NewCategory}",null);
            if (request.IsSuccessStatusCode)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}
