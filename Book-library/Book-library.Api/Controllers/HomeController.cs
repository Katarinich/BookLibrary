using BookLibrary.Api.DAL;
using System.Web.Mvc;

namespace BookLibrary.Api.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            var context = new BookLibraryContext();
            context.Database.CreateIfNotExists();

            return "";
        }
    }
}
