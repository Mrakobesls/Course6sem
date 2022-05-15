using Application.Models.Checkpoint;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class CheckpointController : Controller
    {
        private readonly ICheckpointService _checkpointService;
        private readonly IRoomService _roomService;
        private readonly IAccessLevelService _accessLevelsService;

        public CheckpointController(ICheckpointService checkpointService,
            IRoomService roomService,
            IAccessLevelService accessLevelsService)
        {
            _checkpointService = checkpointService;
            _roomService = roomService;
            _accessLevelsService = accessLevelsService;
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

            viewModel.Checkpoints = _checkpointService.GetAll();

            return View("CheckpointList", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCheckpoint()
        {
            RoomsViewBag();
            AccessLevelsViewBag();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCheckpoint(CreateCheckpointRequest model)
        {
            RoomsViewBag();
            AccessLevelsViewBag();
            if (ModelState.IsValid)
            {
                if (model.FirstRoomId == model.SecondRoomId)
                {
                    ModelState.AddModelError("", "Комнаты должны быть разными");
                    return View(model);
                }
                if (_checkpointService.ReadByName(model.Name) is null)
                {
                    var Checkpoint = _checkpointService.Create(new Checkpoint()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        FirstRoomId = model.FirstRoomId,
                        SecondRoomId = model.SecondRoomId,
                        AccessLevelsId = model.AccessLevelsId,
                    });

                    return RedirectToAction("EditCheckpoint", "Checkpoint", new { CheckpointId = Checkpoint.Id });
                }
                ModelState.AddModelError("", "Комната с таким названием уже существует");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditCheckpoint(int checkpointId)
        {
            RoomsViewBag();
            AccessLevelsViewBag();
            var checkpoint = _checkpointService.Get(checkpointId);

            var editCheckpointRequest = new EditCheckpointRequest
            {
                Id = checkpoint.Id,
                Name = checkpoint.Name,
                Description = checkpoint.Description,
                FirstRoomId = checkpoint.FirstRoomId,
                SecondRoomId = checkpoint.SecondRoomId,
                AccessLevelsId = checkpoint.AccessLevelsId,
            };

            return View(editCheckpointRequest);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditCheckpoint(EditCheckpointRequest model)
        {
            RoomsViewBag();
            AccessLevelsViewBag();
            if (ModelState.IsValid)
            {
                if (model.FirstRoomId == model.SecondRoomId)
                {
                    ModelState.AddModelError("", "Комнаты должны быть разными");
                    return View(model);
                }
                var CheckpointByName = _checkpointService.ReadByName(model.Name);

                if (CheckpointByName is null || CheckpointByName.Id == model.Id)
                {
                    _checkpointService.Update(new Checkpoint
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        FirstRoomId = model.FirstRoomId,
                        SecondRoomId = model.SecondRoomId,
                        AccessLevelsId = model.AccessLevelsId,
                    });

                    return EditCheckpoint(model.Id);
                }
                ModelState.AddModelError("", "Комната с таким названием уже существует");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCheckpoint(int checkpointId)
        {
            _checkpointService.Delete(checkpointId);

            return Checkpoints();
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

        private void AccessLevelsViewBag()
        {
            ViewBag.AccessLevels = _accessLevelsService.GetAll()
                                    .Select(r => new
                                    {
                                        Id = r.Id,
                                        Name = r.Name
                                    }).ToList();
        }

        #endregion
    }
}
