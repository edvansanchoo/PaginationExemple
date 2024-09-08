using Microsoft.AspNetCore.Mvc;
using PaginationTest.Models;
using PaginationTest.Repository.Interface;
using System.Diagnostics;

namespace PaginationTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly INameRepository _nameRepository;
        public HomeController(ILogger<HomeController> logger, INameRepository nameRepository)
        {
            _logger = logger;
            _nameRepository = nameRepository;
        }

        public IActionResult Index(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            IEnumerable<string> names;
            int totalPages;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Tenta buscar nos nomes da página atual
                names = _nameRepository.SearchNames(searchTerm, page, pageSize);

                if (!names.Any())
                {
                    // Se não encontrar, busca na lista completa
                    names = _nameRepository.SearchAllNames(searchTerm);

                    if (!names.Any())
                    {
                        ViewBag.Message = "Nenhum resultado encontrado.";
                    }
                }

                totalPages = (int)Math.Ceiling((double)_nameRepository.SearchAllNames(searchTerm).Count() / pageSize);
            }
            else
            {
                names = _nameRepository.GetNames(page, pageSize);
                totalPages = (int)Math.Ceiling((double)_nameRepository.Count() / pageSize);
            }

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchTerm;

            ViewBag.Names = names;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
