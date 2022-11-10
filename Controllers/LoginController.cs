using LibraryReservedSystem.Data;
using LibraryReservedSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryReservedSystem.Controllers
{
    public class LoginController : Controller
    {
        const string SessionId = "RoleId";
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Authorize(LibraryReservedSystem.Models.UserProfile user)
        {
            var userDetails = _context.UserProfile.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
            //HttpContext.Session.SetInt32(SessionId, userDetails.RoleId);

            if (userDetails == null)
            {
                // Creating message for dialog box
                TempData["WarningMessage"] = "Invalid Crap Homie";
                return RedirectToAction("Index");
            }
            else
            {
                if (userDetails.RoleId == 1)
                {
                    //HttpContext.Session.SetInt32("Id", userDetails.Id);
                    return View("Librarian");
                }
                else if (userDetails.RoleId == 2)
                {
                    //HttpContext.Session.SetInt32("Id", userDetails.Id);
                    return View("LibrarianAssistant");
                }
                else if (userDetails.RoleId == 3)
                {
                    //HttpContext.Session.SetInt32("Id", userDetails.Id);
                    return View("Student");
                }
                else if (userDetails.RoleId == 4)
                {
                    //HttpContext.Session.SetInt32("Id", userDetails.Id);
                    return View("Professor");
                }
                return View("Student");
            }
        }
    }
}
