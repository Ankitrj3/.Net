using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
namespace MyWebApp.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Overview()
        {
            var item = new Item() { Name = "keyboard" };
            return View(item);
        }
        public IActionResult Edit(int id)
        {// The Edit action method takes an integer parameter id, which is typically used to identify the specific item to be edited. The method returns a ContentResult that contains a string representation of the id, indicating which item is being edited.
            return Content("id: " + id);
        }
        public IActionResult querystring(int itemid)
        {
            return Content("Id: "+itemid);
        }
    }
}
