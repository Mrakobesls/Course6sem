using Application.Models.Position;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService PositionService)
        {
            _positionService = PositionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Positions()
        {
            var viewModel = new PositionListResponse();

            viewModel.Positions = _positionService.GetAll();

            return View("PositionList", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePosition()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePosition(CreatePositionRequest model)
        {
            if (ModelState.IsValid)
            {
                if (_positionService.ReadByName(model.Name) is null)
                {
                    var Position = _positionService.Create(new Position()
                    {
                        Name = model.Name
                    });

                    return RedirectToAction("EditPosition", "Position", new { PositionId = Position.Id });
                }
                ModelState.AddModelError("", "Должность с таким названием уже существует");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditPosition(int positionId)
        {
            var Position = _positionService.Get(positionId);

            var editPositionRequest = new EditPositionRequest
            {
                Id = Position.Id,
                Name = Position.Name
            };

            return View(editPositionRequest);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditPosition(EditPositionRequest model)
        {
            if (ModelState.IsValid)
            {
                var PositionByName = _positionService.ReadByName(model.Name);

                if (PositionByName is null || PositionByName.Id == model.Id)
                {
                    _positionService.Update(new Position
                    {
                        Id = model.Id,
                        Name = model.Name
                    });

                    return EditPosition(model.Id);
                }
                ModelState.AddModelError("", "Комната с таким названием уже существует");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePosition(int positionId)
        {
            _positionService.Delete(positionId);

            return Positions();
        }
    }
}
