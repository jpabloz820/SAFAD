﻿using Microsoft.AspNetCore.Mvc;
using Safad.Dtos;
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
            int userId = int.Parse(userIdClaim.Value);
            var lastUserCoach = await _userCoachRepository.GetSequence(new UserCoach());
            int newUserCoachId = (lastUserCoach != null) ? lastUserCoach.UserCoachId + 1 : 1;
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
            return RedirectToAction("Login", "Account"); ;
        }

        public IActionResult IndexCoach()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCoach()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var userCoach = await _userCoachRepository.GetByUserId(userId);
            var user = await _userRepository.GetById(userId);
            if (userCoach == null || user == null)
            {
                return NotFound("No se encontraron datos del entrenador para este usuario.");
            }
            var userCoachDTO = new UserCoachDTO
            {
                NameCoach = userCoach.NameCoach,
                DniCoach = userCoach.DniCoach,
                Cellphone = userCoach.Cellphone,
                Address = userCoach.Address,
                PhotoPath = userCoach.PhotoPath,
                UserEmail = user.UserEmail,
                Password = new string('*', user.Password.Length)
            };
            return View(userCoachDTO);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserCoach()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var userCoach = await _userCoachRepository.GetByUserId(userId);
            if (userCoach == null)
            {
                return NotFound("No se encontraron datos del entrenador.");
            }
            var updateUserCoachDTO = new UpdateUserCoachDTO
            {
                NameCoach = userCoach.NameCoach,
                DniCoach = userCoach.DniCoach,
                Cellphone = userCoach.Cellphone,
                Address = userCoach.Address,
                PhotoPath = userCoach.PhotoPath
            };
            return View(updateUserCoachDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserCoach(UpdateUserCoachDTO model, IFormFile photo)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var userCoach = await _userCoachRepository.GetByUserId(userId);
            if (userCoach == null)
            {
                return NotFound("No se encontraron datos del entreandor.");
            }
            userCoach.NameCoach = model.NameCoach;
            userCoach.DniCoach = model.DniCoach;
            userCoach.Cellphone = model.Cellphone;
            userCoach.Address = model.Address;
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
                userCoach.PhotoPath = $"/img/{photo.FileName}";
            }
            await _userCoachRepository.Update(userCoach);
            return RedirectToAction("GetUserCoach");
        }
    }
}
