using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using System.Security.Claims;

namespace Safad.Controllers
{
    public class ProfessionalController : Controller
    {
        

        private readonly IUserRepository _userRepository;
        private readonly IProfesionalRepository _profesionalRepository;

        public ProfessionalController(IUserRepository userRepository,
            IProfesionalRepository IProfesionalRepository)
        {
            _userRepository = userRepository;
            _profesionalRepository = IProfesionalRepository;
        }
        public IActionResult CreateUserProfesional()
        {
            return View();
        }

        public IActionResult EditUserProfesional()
        {
            return View();
        }

        public IActionResult DeleteUserProfesional() { 
            return View();
        }

        public async Task<IActionResult> ListUserProfesional()
        {
            // Obtén el ID del usuario autenticado
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Obtén el profesional de manera asincrónica usando await
            var profesional = await _profesionalRepository.GetById(userId);

            // Si no se encuentra el profesional
            if (profesional == null)
            {
                return NotFound();
            }

            // Retorna la vista con el modelo correcto
            return View(profesional);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserProfesional(Profesional model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var lastProfesional = await _profesionalRepository.GetSequence(new Profesional());
            int newProfesionalId = (lastProfesional != null) ? lastProfesional.ProfesionalId + 1 : 1;
            var newProfesional = new Profesional
            {
                ProfesionalId = newProfesionalId,
                NameProfesional = model.NameProfesional,
                DniProfesional = model.DniProfesional,
                Cellphone = model.Cellphone,
                Address = model.Address,
                UserId = userId
            };
            await _profesionalRepository.Add(newProfesional);
            var user = await _userRepository.GetById(userId);
            user.Registration = true;
            await _userRepository.Update(user);
            return RedirectToAction("IndexProfesional");
        }


        public IActionResult IndexProfesional()
        {
            return View();
        }
    }
}
