using Microsoft.AspNetCore.Mvc;
using Soil.Models;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Soil.Controllers
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

        public FileResult DownloadSoilResults()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes("/home/pi/soil/soilReadings.txt");
            string fileName = "soilMeasurements.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public async Task<string> PostMoisture([Bind("Id,Location,Timestamp,RawReading")] MoistureReading tempReading)
        {
            tempReading.Timestamp = DateTime.Now;
            var txt = "Request received!!!  ID: " + tempReading.Id + ", Location: " + tempReading.Location + ", Timestamp: " +
                tempReading.Timestamp + ", RawReading: " + tempReading.RawReading + ".";

            System.IO.File.AppendAllText("/home/pi/soil/soilReadings.txt", "" + tempReading.Timestamp + "," + tempReading.Id +
                "," + tempReading.Location + "," + tempReading.RawReading + Environment.NewLine);
            return txt;
         
        }
    }
}