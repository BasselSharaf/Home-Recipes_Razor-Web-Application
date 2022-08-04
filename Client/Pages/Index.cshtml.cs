using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
namespace Exercise3.Pages
{
    public class IndexModel : PageModel
    {

        public IActionResult OnGet()
        {
            return Redirect("/Recipes");
        }
    }
}