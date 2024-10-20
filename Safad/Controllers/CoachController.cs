using Microsoft.AspNetCore.Mvc;
using Safad.Interfaces;
using Safad.Models;
using System.Security.Claims;

namespace Safad.Controllers
{
    public class CoachController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCoachRepository _userCoachRepository;

        public CoachController(IUserRepository userRepository,
            IUserCoachRepository userCoachRepository)
        {
            _userRepository = userRepository;
            _userCoachRepository = userCoachRepository;
        }
        public IActionResult CreateUserCoach()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserCoach(UserCoach model, IFormFile photo)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            string photoPath = null;
            if (photo != null && photo.Length > 0)
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var filePath = Path.Combine(directory, photo.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                photoPath = $"/img/{photo.FileName}";
            }
            int userId = int.Parse(userIdClaim.Value);
            var lastUserCoach = await _userCoachRepository.GetSequence(new UserCoach());
            int newUserCoachId = (lastUserCoach != null) ? lastUserCoach.UserCoachId + 1 : 1;
            var newUserCoach = new UserCoach
            {
                UserCoachId = newUserCoachId,
                NameCoach = model.NameCoach,
                DniCoach = model.DniCoach,
                Cellphone = model.Cellphone,
                Address = model.Address,
                UserId = userId,
                PhotoPath = photoPath
            };
            await _userCoachRepository.Add(newUserCoach);
            var user = await _userRepository.GetById(userId);
            user.Registration = true;
            await _userRepository.Update(user);
            return RedirectToAction("IndexCoach");
        }

        public IActionResult IndexCoach()
        {
            return View();
        }
    }
}
