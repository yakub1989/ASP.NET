using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ChatApp.Models;
using CoolChat.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ChatContext _context;
        public AccountController(ChatContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }
    }
}
