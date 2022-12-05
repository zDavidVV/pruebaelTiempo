using Microsoft.AspNetCore.Mvc;
using pruebaelTiempo.Models;
using System.Diagnostics;
using System.Text.Json;

namespace pruebaelTiempo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            response datos = new response();

            using (var httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync("https://images-api.nasa.gov/search?q=apollo%2011");
                task.Wait();
                var result = task.Result.Content.ReadAsStringAsync().Result;

                datos = JsonSerializer.Deserialize<response>(result);
            }

            datos.collection.items = datos.collection.items.GetRange(0, 10);

            return View(datos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}