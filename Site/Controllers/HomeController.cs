using Matematics.Graphic;
using Matematics.Other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Sito.Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sito.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IJSRuntime js;

        public HomeController(ILogger<HomeController> logger, IJSRuntime js)
        {
            _logger = logger;

            this.js = js;
        }

        public IActionResult Index(string startRange, string endRange, string equation)
        {
            if (startRange == null || endRange == null || equation == null)
            {
                return View("Index");
            }

            if ((Math.Abs(int.Parse(startRange)) + Math.Abs(int.Parse(endRange))) > 200)
            {
                ViewBag.Error = "Too wide range";
                ViewBag.Points = ThreeConvert.ConvertToThree(new Coordinates[] { }, ";", ":");
                return View("Index");
            }

            CancellationTokenSource source = new();
            CancellationToken token = source.Token;
            var task = Task.Run(() => GetCoordinates(int.Parse(startRange), int.Parse(endRange), equation), token);
            source.CancelAfter(0);
            task.Wait();

            if (task.IsCanceled == false)
            {
                ViewBag.Error = "";
                ViewBag.Points = ThreeConvert.ConvertToThree(task.Result, ";", ":");
            }
            else
            {
                ViewBag.Error = "Too much time required";
                ViewBag.Points = ThreeConvert.ConvertToThree(new Coordinates[] { }, ";", ":");
            }

            return View("Index");
        }

        private static Coordinates[] GetCoordinates(int startRange, int endRange, string equation)
        {
            Graphic gr = new();
            gr.Numbermax = Convert.ToInt32(endRange);
            gr.Numbermin = Convert.ToInt32(startRange);
            gr.Equation = Area.Parse(equation);
            gr.Precision = 1;
            gr.Tridimensional = true;

            gr.Prepare();
            return gr.ListCoordinates.ToArray();
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
