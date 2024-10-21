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
        private readonly IUserCoachRepository _userCoachRepository;
        private readonly IUserAthleteRepository _userAthleteRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountController(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserCoachRepository userCoachRepository,
            IUserAthleteRepository userAthleteRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userCoachRepository = userCoachRepository;
            _userAthleteRepository = userAthleteRepository;
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
                string displayname = "Usuario";
                string profilePicture = "/img/default-profile.png";
                switch (user.RoleId)
                {
                    case 1:
                        var userAthlete = await _userAthleteRepository.GetByUserId(user.UserId);
                        if (userAthlete != null)
                        {
                            displayname = userAthlete.NameAthlete;
                            profilePicture = userAthlete.PhotoPath ?? "/img/default-profile.png";
                        }
                        break;
                }
                var claims = new List<Claim> 
                {
                    new Claim(ClaimTypes.Email, user.UserEmail),
                    new Claim(ClaimTypes.Role, role.RoleName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, displayname),
                    new Claim("ProfilePicture",profilePicture)                   
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                if(role.RoleId == 1)
                {
                    if (!user.Registration)
                    {
                        return RedirectToAction("CreateUserAthlete", "Athlete");
                    }
                    else
                    {
                        return RedirectToAction("IndexAthlete", "Athlete");
                    }
                    
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
                    if (!user.Registration)
                    {
                        return RedirectToAction("CreateUserProfesional", "Professional");
                    }
                    else
                    {
                        return RedirectToAction("IndexProfesional", "Professional");
                    }
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
