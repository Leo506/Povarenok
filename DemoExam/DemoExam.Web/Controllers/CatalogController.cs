using DemoExam.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoExam.Web.Controllers;

public class CatalogController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(new ListOfProductsViewModel());
    }
}