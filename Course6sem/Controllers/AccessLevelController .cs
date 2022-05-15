using Application.Models.AccessLevel;
using Application.Models.Admin;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class AccessLevelController : Controller
    {
        private readonly IAccessLevelService _accessLevelService;

        public AccessLevelController(IAccessLevelService AccessLevelService)
        {
            _accessLevelService = AccessLevelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AccessLevels()
        {
            var viewModel = new AccessLevelListResponse();

            viewModel.AccessLevels = _accessLevelService.GetAll();

            return View("AccessLevelList", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAccessLevel()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAccessLevel(CreateAccessLevelRequest model)
        {
            if (ModelState.IsValid)
            {
                if (_accessLevelService.ReadByName(model.Name) is null)
                {
                    var AccessLevel = _accessLevelService.Create(new AccessLevel()
                    {
                        Name = model.Name
                    });

                    return RedirectToAction("EditAccessLevel", "AccessLevel", new { AccessLevelId = AccessLevel.Id });
                }
                ModelState.AddModelError("", "Тест с таким названием уже существует");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditAccessLevel(int accessLevelId)
        {
            var AccessLevel = _accessLevelService.Get(accessLevelId);

            var editAccessLevelRequest = new EditAccessLevelRequest
            {
                Id = AccessLevel.Id,
                Name = AccessLevel.Name
            };

            return View(editAccessLevelRequest);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditAccessLevel(EditAccessLevelRequest model)
        {
            if (ModelState.IsValid)
            {
                var AccessLevelByName = _accessLevelService.ReadByName(model.Name);

                if (AccessLevelByName is null || AccessLevelByName.Id == model.Id)
                {
                    _accessLevelService.Update(new AccessLevel
                    {
                        Id = model.Id,
                        Name = model.Name
                    });

                    return EditAccessLevel(model.Id);
                }
                ModelState.AddModelError("", "Комната с таким названием уже существует");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAccessLevel(int accessLevelId)
        {
            _accessLevelService.Delete(accessLevelId);

            return AccessLevels();
        }
    }
}
