using Microsoft.AspNetCore.Mvc;
using Safad.Interfaces;
using Safad.Models;
using Safad.Repositories;
using System.Security.Claims;

namespace Safad.Controllers
{
    public class AthleteController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAthleteRepository _userAthleteRepository;

        public AthleteController(IUserRepository userRepository,
            IUserAthleteRepository userAthleteRepository)
        {
            _userRepository = userRepository;
            _userAthleteRepository = userAthleteRepository;
        }
        public IActionResult CreateUserAthlete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAthlete(User_Athlete model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var lastUserAthlete = await _userAthleteRepository.GetSequence(new User_Athlete());
            int newUserAthleteId = (lastUserAthlete != null) ? lastUserAthlete.UserAthleteId + 1 : 1;
            var newUserAthlete = new User_Athlete
            {
                UserAthleteId = newUserAthleteId,
                NameAthlete = model.NameAthlete,
                DniAthlete = model.DniAthlete,
                Cellphone = model.Cellphone,
                Address = model.Address,
                Weight = model.Weight,
                Height = model.Height,  
                Position = model.Position,
                UserId = userId
            };
            await _userAthleteRepository.Add(newUserAthlete);
            var user = await _userRepository.GetById(userId);
            user.Registration = true;
            await _userRepository.Update(user);
            return RedirectToAction("IndexAthlete");
        }


        public IActionResult IndexAthlete()
        {
            return View();
        }

    }
}
