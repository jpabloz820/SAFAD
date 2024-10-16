using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Safad.Interfaces;

namespace Safad.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public AccountController(IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user != null && user.Password == password)
            {
                var role = await _roleRepository.GetById(user.RoleId);
                var claims = new List<Claim> 
                {
                    new Claim(ClaimTypes.Name, user.UserEmail),
                    new Claim(ClaimTypes.Role, role.RoleName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                if(role.RoleId == 1)
                {
                    return RedirectToAction("Index", "Athlete");
                }
                if (role.RoleId == 2)
                {
                    if (!user.Registration)
                    {
                        return RedirectToAction("CreateUserCoach", "Coach");
                    }
                    else
                    {
                        return RedirectToAction("IndexCoach", "Coach");
                    }
                }
                if (role.RoleId == 3)
                {
                    return RedirectToAction("Index", "Professional");
                }
                if (role.RoleId == 4)
                {
                    return RedirectToAction("Index", "Familiar");
                }
                if (role.RoleId == 5)
                {
                    return RedirectToAction("Index", "Administrator");
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
            return View();
        }
    }
}
