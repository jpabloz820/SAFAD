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

        // Método para mostrar el formulario de edición
        public async Task<IActionResult> EditUserProfesional(int id)
        {
            var profesional = await _profesionalRepository.GetById(id);
            if (profesional == null)
            {
                return NotFound();
            }
            profesional.User = await _userRepository.GetById(profesional.UserId);
            return View(profesional); // Devuelve la vista con el modelo
        }

        // Método para manejar la edición del profesional
        [HttpPost]
        [ValidateAntiForgeryToken] // Para proteger contra ataques CSRF
        public async Task<IActionResult> EditUserProfesional(int ProfesionalId, Profesional model)
        {
            System.Diagnostics.Debug.WriteLine("Llegó al método EditUserProfesional con ID: " + ProfesionalId);
           

            if (ProfesionalId != model.ProfesionalId)
            {
                return BadRequest(); // Si el ID no coincide
            }

            // Asignar el UserId (si estás utilizando el UserId y no el objeto User completo)
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                model.UserId = int.Parse(userIdClaim.Value); // Asigna el UserId
                model.User = await _userRepository.GetById(model.UserId); // Carga el usuario
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {error.ErrorMessage}");
                }
            }


            if (ModelState.IsValid) // Verifica si el modelo es válido
            {
                System.Diagnostics.Debug.WriteLine("El modelo es valido");
                await _profesionalRepository.Update(model); // Actualiza el profesional
                return RedirectToAction("ListUserProfesional"); // Redirige a la lista de profesionales o a otra acción
            }

            return View(model); // Si hay errores, vuelve a mostrar el formulario
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
