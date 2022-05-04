using Application.Models.Checkpoint;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class CheckpointController : Controller
    {
        private readonly ICheckpointService _CheckpointService;
        private readonly IRoomService _roomService;

        public CheckpointController(ICheckpointService CheckpointService,
            IRoomService roomService)
        {
            _CheckpointService = CheckpointService;
            _roomService = roomService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Checkpoints()
        {
            var viewModel = new CheckpointListResponse();

            viewModel.Checkpoints = _CheckpointService.GetAll();

            return View("CheckpointList", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCheckpoint()
        {
            RoomsViewBag();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCheckpoint(CreateCheckpointRequest model)
        {
            RoomsViewBag();
            if (ModelState.IsValid)
            {
                if (_CheckpointService.ReadByName(model.Name) is null)
                {
                    var Checkpoint = _CheckpointService.Create(new Checkpoint()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        FirstRoomId = model.FirstRoomId,
                        SecondRoomId = model.SecondRoomId
                    });

                    return RedirectToAction("EditCheckpoint", "Checkpoint", new { CheckpointId = Checkpoint.Id });
                }
                ModelState.AddModelError("", "Тест с таким названием уже существует");
            }
            else
            {
                ModelState.AddModelError("", "Валидация фигня");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditCheckpoint(int checkpointId)
        {
            RoomsViewBag();
            var checkpoint = _CheckpointService.Get(checkpointId);

            var editCheckpointRequest = new EditCheckpointRequest
            {
                Id = checkpoint.Id,
                Name = checkpoint.Name,
                Description = checkpoint.Description,
                FirstRoomId = checkpoint.FirstRoomId,
                SecondRoomId = checkpoint.SecondRoomId
            };

            return View(editCheckpointRequest);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditCheckpoint(EditCheckpointRequest model)
        {
            RoomsViewBag();
            if (ModelState.IsValid)
            {
                var CheckpointByName = _CheckpointService.ReadByName(model.Name);

                if (CheckpointByName is null || CheckpointByName.Id == model.Id)
                {
                    _CheckpointService.Update(new Checkpoint
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        FirstRoomId = model.FirstRoomId,
                        SecondRoomId = model.SecondRoomId
                    });

                    return EditCheckpoint(model.Id);
                }
                ModelState.AddModelError("", "Комната с таким названием уже существует");
            }

            return View(model);
        }

        #region Private

        private void RoomsViewBag()
        {
            ViewBag.Rooms = _roomService.GetAll()
                                    .Select(r => new
                                    {
                                        Id = r.Id,
                                        Name = r.Name
                                    }).ToList();
        }

        #endregion
    }
}
