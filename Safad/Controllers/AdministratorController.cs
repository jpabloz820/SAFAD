using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Safad.Interfaces;
using Safad.Models;

namespace Safad.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAdministrativeRepositor _userAdministrativeRepository;
        private readonly IRoleRepository _roleRepository;

        public AdministratorController(IUserRepository userRepository,
            IUserAdministrativeRepositor IUserAdministrativeRepository,
            IRoleRepository IRoleRepository)
        {
            _userRepository = userRepository;
            _userAdministrativeRepository = IUserAdministrativeRepository;
            _roleRepository = IRoleRepository;
        }
        public IActionResult CreateUserAdministrative()
        {
            return View();
        }

        public IActionResult IndexAdministrative()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAdministrative(UserAdministrative model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var lastUserAdministrative = await _userAdministrativeRepository.GetSequence(new UserAdministrative());
            int newUserAdministrativeId = (lastUserAdministrative != null) ? lastUserAdministrative.UserAdministrativeId + 1 : 1;
            var newUserAdministrative = new UserAdministrative
            {
                UserAdministrativeId = newUserAdministrativeId,
                NameAdministrative = model.NameAdministrative,
                DniAdministrative = model.DniAdministrative,
                Cellphone = model.Cellphone,
                Address = model.Address,
                UserId = userId
            };
            await _userAdministrativeRepository.Add(newUserAdministrative);
            var user = await _userRepository.GetById(userId);
            user.Registration = true;
            await _userRepository.Update(user);
            return RedirectToAction("IndexAdministrative");
        }

        public async Task<IActionResult> ListUserAdministrative()
        {
            // Obtén el ID del usuario autenticado
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Obtén el administrative de manera asincrónica usando await
            var administrative = await _userAdministrativeRepository.GetUserByIdUserAsync(userId);

            // Si no se encuentra el administrative
            if (administrative == null)
            {
                return NotFound();
            }

            // Retorna la vista con el modelo correcto
            return View(administrative);
        }

        public async Task<IActionResult> EditUserAdministrative(int id)
        {
            var administrative = await _userAdministrativeRepository.GetById(id);
            if (administrative == null)
            {
                return NotFound();
            }
            administrative.User = await _userRepository.GetById(administrative.UserId);
            return View(administrative); // Devuelve la vista con el modelo
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Para proteger contra ataques CSRF
        public async Task<IActionResult> EditUserAdministrative(int UserAdministrativeId, UserAdministrative model)
        {
            System.Diagnostics.Debug.WriteLine("Llegó al método EditUserProfesional con ID: " + UserAdministrativeId);

            if (UserAdministrativeId != model.UserAdministrativeId)
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
                await _userAdministrativeRepository.Update(model); // Actualiza el profesional
                return RedirectToAction("ListUserAdministrative"); // Redirige a la lista de profesionales o a otra acción
            }

            return View(model); // Si hay errores, vuelve a mostrar el formulario
        }

        public async Task<IActionResult> CreateUserAsync()
        {
            // Obtén la lista de roles desde el repositorio
            var roles = await _roleRepository.GetAll();
            
            // Guarda la lista de roles en ViewBag
            ViewBag.Roles = roles.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            var lastUser = await _userRepository.GetSequence(new User());
            int newUserId = (lastUser != null) ? lastUser.UserId + 1 : 1;
            var newUser = new User
            {
                UserId = newUserId,
                UserEmail = model.UserEmail,
                Password = model.Password,
                RoleId = model.RoleId,
                Registration = false
            };
            await _userRepository.Add(newUser);
            return RedirectToAction("IndexAdministrative");
        }

    }
}
