using Application.Models.AccessLevel;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class AccessLevelController : Controller
    {
        private readonly IAccessLevelService _AccessLevelService;

        public AccessLevelController(IAccessLevelService AccessLevelService)
        {
            _AccessLevelService = AccessLevelService;
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

            viewModel.AccessLevels = _AccessLevelService.GetAll();

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
                if (_AccessLevelService.ReadByName(model.Name) is null)
                {
                    var AccessLevel = _AccessLevelService.Create(new AccessLevel()
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
            var AccessLevel = _AccessLevelService.Get(accessLevelId);

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
                var AccessLevelByName = _AccessLevelService.ReadByName(model.Name);

                if (AccessLevelByName is null || AccessLevelByName.Id == model.Id)
                {
                    _AccessLevelService.Update(new AccessLevel
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
    }
}
