using Microsoft.AspNetCore.Mvc;

namespace compilerProject2.Controllers
{
    public class ScannerForFrontEndController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult getScannerCode ()
        {

            return View() ;
        }
        [HttpPost]
        public IActionResult getScannerCode(IFormCollection form)
        {

            string textArea = form["textArea"];
            if (form["filename"].Equals(""))
                textArea = form["textArea"];
            else
                textArea = System.IO.File.ReadAllText(@form["filename"]);
            return RedirectToAction("readTokens", "Scanner", new { code = textArea, action = "readTokens", submitAll = true });
        }
    }
}
