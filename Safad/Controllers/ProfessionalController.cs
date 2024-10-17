using Microsoft.AspNetCore.Mvc;
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
