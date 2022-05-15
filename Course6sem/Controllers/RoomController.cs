using Application.Models.Room;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Rooms()
        {
            var viewModel = new RoomListResponse();

            viewModel.Rooms = _roomService.GetAll();

            return View("RoomList", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRoom(CreateRoomRequest model)
        {
            if (ModelState.IsValid)
            {
                if (_roomService.ReadByName(model.Name) is null)
                {
                    var room = _roomService.Create(new Room()
                    {
                        Name = model.Name,
                        Description = model.Description
                    });

                    return RedirectToAction("EditRoom", "Room", new { roomId = room.Id });
                }
                ModelState.AddModelError("", "Тест с таким названием уже существует");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditRoom(int roomId)
        {
            var room = _roomService.Get(roomId);

            var editRoomRequest = new EditRoomRequest
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                Checkpoints = _roomService.GetRoomCheckpoints(room.Id)
            };

            return View(editRoomRequest);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditRoom(EditRoomRequest model)
        {
            if (ModelState.IsValid)
            {
                var roomByName = _roomService.ReadByName(model.Name);

                if (roomByName is null || roomByName.Id == model.Id)
                {
                    _roomService.Update(new Room
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description
                    });

                    return EditRoom(model.Id);
                }
                ModelState.AddModelError("", "Комната с таким названием уже существует");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCheckpoint(int roomId)
        {
            if(_roomService.IsRoomEmpty(roomId))
            {
                _roomService.Delete(roomId);
                return Rooms();
            }
            else
            {
                ModelState.AddModelError("", "В комнате присутствуют люди!\n Для удаления в комнате должны отсутствовать люди!");
                return View();
            }
        }
    }
}
