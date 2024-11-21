using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Repositories;
using System.Security.Claims;

namespace Safad.Controllers
{
    public class ProfessionalController : Controller
    {
        

        private readonly IUserRepository _userRepository;
        private readonly IProfesionalRepository _profesionalRepository;
        private readonly ITypeProfessionalRepository _typeprofessionalRepository;

        public ProfessionalController(IUserRepository userRepository,
            IProfesionalRepository IProfesionalRepository, ITypeProfessionalRepository typeProfessionalRepository)
        {
            _userRepository = userRepository;
            _profesionalRepository = IProfesionalRepository;
            _typeprofessionalRepository = typeProfessionalRepository;

        }

        public async Task<IActionResult> EditUserProfesional(int id)
        {
            var profesional = await _profesionalRepository.GetById(id);
            if (profesional == null)
            {
                return NotFound();
            }
            profesional.User = await _userRepository.GetById(profesional.UserId);
            return View(profesional); 
        }

        


        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> EditUserProfesional(int ProfesionalId, Profesional model)
        {
            System.Diagnostics.Debug.WriteLine("Llegó al método EditUserProfesional con ID: " + ProfesionalId);
           

            if (ProfesionalId != model.ProfesionalId)
            {
                return BadRequest(); 
            }

            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                model.UserId = int.Parse(userIdClaim.Value); 
                model.User = await _userRepository.GetById(model.UserId); 
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid) 
            {
                System.Diagnostics.Debug.WriteLine("El modelo es valido");
                await _profesionalRepository.Update(model); 
                return RedirectToAction("ListUserProfesional"); 
            }

            return View(model); 
        }


        public async Task<IActionResult> DeleteUserProfesional(int id)
        {
            var profesional = await _profesionalRepository.GetById(id);
            if (profesional == null)
            {
                return NotFound(); 
            }

            return View(profesional); 
        }
        [HttpPost, ActionName("DeleteUserProfesional")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesional = await _profesionalRepository.GetById(id);
            if (profesional == null)
            {
                return NotFound(); 
            }
            await _profesionalRepository.Delete(profesional);

            return RedirectToAction("ListUserProfesional"); 
        }

        public async Task<IActionResult> ListUserProfesional()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var profesional = await _profesionalRepository.GetById(userId + 1);
            System.Diagnostics.Debug.WriteLine("Llegó al método ListProfesional con ID: " + userId);
            if (profesional == null)
            {
                
                return RedirectToAction("CreateUserProfesional");
            }

            return View(profesional);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUserProfesional()
        {
            ViewData["Type"] = await _typeprofessionalRepository.GetAll();

            return View(new Profesional());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserProfesional(Profesional model, IFormFile photo)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var lastProfesional = await _profesionalRepository.GetSequence(new Profesional());
            int newProfesionalId = (lastProfesional != null) ? lastProfesional.ProfesionalId + 1 : 1;
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
            var newProfesional = new Profesional
            {
                ProfesionalId = newProfesionalId,
                NameProfesional = model.NameProfesional,
                DniProfesional = model.DniProfesional,
                PhotoPath = photoPath,
                TypeProfessionalId = model.TypeProfessionalId,
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
