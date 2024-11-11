using Microsoft.AspNetCore.Mvc;
using Safad.Dtos;
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
        public async Task<IActionResult> CreateUserAthlete(User_Athlete model, IFormFile photo)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var lastUserAthlete = await _userAthleteRepository.GetSequence(new User_Athlete());
            int newUserAthleteId = (lastUserAthlete != null) ? lastUserAthlete.UserAthleteId + 1 : 1;
            string photoPath = null;
            if (photo != null && photo.Length > 0)
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                if (Directory.Exists(directory))
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
                    UserId = userId,
                    PhotoPath = photoPath,
                    Age = model.Age

                };
                await _userAthleteRepository.Add(newUserAthlete);
                var user = await _userRepository.GetById(userId);
                user.Registration = true;
                await _userRepository.Update(user);
                return RedirectToAction("Login","Account");
            }


            public IActionResult IndexAthlete()
            {
                return View();
            }

        [HttpGet]
        public async Task<IActionResult> GetUserAthlete()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var userAthlete = await _userAthleteRepository.GetByUserId(userId);
            var user = await _userRepository.GetById(userId);
            if (userAthlete == null || user == null)
            {
                return NotFound("No se encontraron datos del Deportista para este usuario.");
            }
            var userAthleteDTO = new UserAthleteDto
            {
                NameAthlete = userAthlete.NameAthlete,
                DniAthlete = userAthlete.DniAthlete,
                Cellphone = userAthlete.Cellphone,
                Address = userAthlete.Address,
                Age = userAthlete.Age,
                Weight = userAthlete.Weight,
                PhotoPath = userAthlete.PhotoPath,
                Height = userAthlete.Height,
                Position = userAthlete.Position,
                UserEmail = user.UserEmail,
                Password = new string('*', user.Password.Length)
            };
            return View(userAthleteDTO);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserAthlete()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var userAthlete = await _userAthleteRepository.GetByUserId(userId);
            if (userAthlete == null)
            {
                return NotFound("No se encontraron datos del entrenador.");
            }
            var updateUserAthleteDTO = new UpdateUserAthleteDTO
            {
                NameAthlete = userAthlete.NameAthlete,
                DniAthlete = userAthlete.DniAthlete,
                Cellphone = userAthlete.Cellphone,
                Address = userAthlete.Address,
                Age = userAthlete.Age,
                Weight = userAthlete.Weight,
                PhotoPath = userAthlete.PhotoPath,
                Height = userAthlete.Height,
                Position = userAthlete.Position,
                
            };
            return View(updateUserAthleteDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserAthlete(UpdateUserAthleteDTO model, IFormFile photo)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var userAthlete = await _userAthleteRepository.GetByUserId(userId);
            if (userAthlete == null)
            {
                return NotFound("No se encontraron datos del Deportista.");
            }
            userAthlete.NameAthlete = model.NameAthlete;
            userAthlete.DniAthlete = model.DniAthlete;
            userAthlete.Cellphone = model.Cellphone;
            userAthlete.Address = model.Address;
            userAthlete.Age = model.Age;
            userAthlete.Weight  = model.Weight;
            userAthlete.Height = model.Height;
            userAthlete.Position = userAthlete.Position;


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
                userAthlete.PhotoPath = $"/img/{photo.FileName}";
            }
            await _userAthleteRepository.Update(userAthlete);
            return RedirectToAction("GetUserAthlete");
        }

    }
    }



