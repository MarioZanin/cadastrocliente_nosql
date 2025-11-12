using Microsoft.AspNetCore.Mvc;
namespace CadastroClienteApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Retorna a View localizada em Views/Home/Index.cshtml
        }
    }
}