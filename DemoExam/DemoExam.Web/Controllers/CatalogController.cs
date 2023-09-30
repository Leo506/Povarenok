using DemoExam.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoExam.Web.Controllers;

public class CatalogController : Controller
{
    public IActionResult Index(string searchString, int category)
    {
        var model = new CatalogViewModel();
        return View(model);
    }
}