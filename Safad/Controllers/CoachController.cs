using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Safad.Dtos;
using Safad.Interfaces;
using Safad.Models;
using Safad.Repositories;
using System.Security.Claims;

namespace Safad.Controllers
{
    public class CoachController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPhaseRepository _phaseRepository;
        private readonly IMetricRepository _metricRepository;
        private readonly IDivisionRepository _divisionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserCoachRepository _userCoachRepository;
        private readonly IUserAthleteRepository _userAthleteRepository;
        private readonly IGoalIndicatorRepository _goalIndicatorRepository;
        private readonly ITeamUserAthleteRepository _teamUserAthleteRepository;
        private readonly IConfigurationMetricRepository _configurationMetricRepository;

        public CoachController(IUserRepository userRepository,
            ITeamRepository teamRepository,
            IPhaseRepository phaseRepository,
            IMetricRepository metricRepository,
            ICategoryRepository categoryRepository,
            IDivisionRepository divisionRepository,
            IUserCoachRepository userCoachRepository,
            IUserAthleteRepository userAthleteRepository,
            IGoalIndicatorRepository goalIndicatorRepository,
            ITeamUserAthleteRepository teamUserAthleteRepository,
            IConfigurationMetricRepository configurationMetricRepository)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _phaseRepository = phaseRepository;
            _metricRepository = metricRepository;
            _categoryRepository = categoryRepository;
            _divisionRepository = divisionRepository;
            _userCoachRepository = userCoachRepository;
            _userAthleteRepository = userAthleteRepository;
            _goalIndicatorRepository = goalIndicatorRepository;
            _teamUserAthleteRepository = teamUserAthleteRepository;
            _configurationMetricRepository = configurationMetricRepository;
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
            return RedirectToAction("Login", "Account");
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

        [HttpGet]
        public async Task<IActionResult> GetTeam()
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
            var team = await _teamRepository.GetTeamByUserCoachId(userCoach.UserCoachId);
            if (team == null)
            {
                return NotFound("No se encontró el equipo con los datos del entrenador.");
            }
            var category = await _categoryRepository.GetById(team.CategoryId);
            var division = await _divisionRepository.GetById(team.DivisionId);
            var teamUserAthletes = await _teamUserAthleteRepository.GetTeamUserAthletesByTeamId(team.TeamId);
            var athletesDto = teamUserAthletes.Select(athlete => new UserAthleteDto
            {
                PhotoPath = athlete.User_Athlete.PhotoPath,
                NameAthlete = athlete.User_Athlete.NameAthlete,
                DniAthlete = athlete.User_Athlete.DniAthlete,
                Cellphone = athlete.User_Athlete.Cellphone,
                Address = athlete.User_Athlete.Address,
                Age = athlete.User_Athlete.Age,
                UserEmail = athlete.User_Athlete.User.UserEmail
            }).ToList();
            var teamDto = new TeamDto
            {
                TeamName = team.TeamName,
                CategoryName = category.CategoryName,
                DivisionName = division.DivisionName,
                NumberAthlete = athletesDto.Count(),
                TeamLogo = team.TeamLogo,
                Athletes = athletesDto
            };
            return View(teamDto);
        }

        public IActionResult GetPhase()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateMetric()
        {
            ViewData["Phases"] = await _phaseRepository.GetAll();
            ViewData["Categories"] = await _categoryRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMetric(MetricDto model)
        {
            var lastMetric = await _metricRepository.GetSequence(new Metric());
            int newMetricId = (lastMetric != null) ? lastMetric.MetricId + 1 : 1;
            var metric = new Metric
            {
                MetricId = newMetricId,
                MetricName = model.MetricName,
                Indicator = model.Indicator,
                Measure = model.Measure
            };
            await _metricRepository.Add(metric);
            var configureMetric = new ConfigurationMetric
            {
                MetricId = newMetricId,
                PhaseId = model.PhaseId,
                CategoryId = model.CategoryId
            };
            await _configurationMetricRepository.Add(configureMetric);
            return View("GetPhase");
        }

        [HttpGet]
        public async Task<IActionResult> CreateIndicator(int phaseId)
        {
            ViewData["PhaseId"] = phaseId;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var userCoach = await _userCoachRepository.GetByUserId(userId);
            var team = await _teamRepository.GetTeamByUserCoachId(userCoach.UserCoachId);
            var teamUserAthletes = await _teamUserAthleteRepository.GetTeamUserAthletesByTeamId(team.TeamId);
            var category = await _categoryRepository.GetById(team.CategoryId);
            var athleteList = teamUserAthletes.Select(athlete => new AthleteDto
            {
                UserAthleteId = athlete.User_Athlete.UserAthleteId,
                NameAthlete = athlete.User_Athlete.NameAthlete,
            }).ToList();
            var configurationMetrics = await _configurationMetricRepository.GetByPhaseAndCategory(phaseId, team.CategoryId);
            var metricList = configurationMetrics.Select(cm => new MetricDto
            {
                MetricId = cm.Metric.MetricId,
                MetricName = cm.Metric.MetricName,
            }).ToList();
            ViewData["AthleteList"] = athleteList;
            ViewData["MetricList"] = metricList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateIndicators(GoalIndicator model, int phaseId)
        {
            var lastIndicator = await _goalIndicatorRepository.GetSequence(new GoalIndicator());
            int newIndicatorId = (lastIndicator != null) ? lastIndicator.GoalIndicatorId + 1 : 1;
            var indicator = new GoalIndicator
            {
                GoalIndicatorId = newIndicatorId,
                UserAthleteId = model.UserAthleteId,
                MetricId = model.MetricId,
                MeasureAthlete = model.MeasureAthlete
            };
            await _goalIndicatorRepository.Add(indicator);
            return RedirectToAction("CreateIndicator", new { phaseId });
        }
    }
}
