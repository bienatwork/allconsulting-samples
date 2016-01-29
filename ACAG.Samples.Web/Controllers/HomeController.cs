// ACAG.Samples.Web.Controllers
// *****************************************************************************************
//
// Name:        HomeController.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Web.Mvc;

namespace ACAG.Samples.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        } 
    }
}