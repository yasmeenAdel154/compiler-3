using Microsoft.AspNetCore.Mvc;
using compilerProject2.Controllers;
namespace compilerProject2.Controllers
{
    public class ParserController : Controller
    {
        public List<string> Tokens { get; set; } = new List<string>();

        ScannerController sc = new ScannerController();
        public IActionResult Index()
        {
            return View();
        }
        void preCompile ()
        {
            Tokens = sc.Tokens;
        }
        public IActionResult Parsing ()
        {
            Stack<string> s = new Stack<string>();
            s.Push("$");
            s.Push("");
            return View();
        }

    }
}
