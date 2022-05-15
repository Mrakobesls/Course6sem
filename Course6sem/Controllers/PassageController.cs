using Application.Models.Passage;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class PassageController : Controller
    {
        private IPassageService _passageService;

        public PassageController(IPassageService passageService)
        {
            _passageService = passageService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult PassCheckpoint()
        {
            var userId = Int32.Parse(HttpContext.User.Claims.Single(c => c.Type == "Id").Value);
            CheckpointsViewBag(userId);

            var model = new PassCheckpointRequest()
            {
                Room = _passageService.GetUserCurrentRoom(userId)
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult PassCheckpoint(int checkpointId)
        {
            var userId = Int32.Parse(HttpContext.User.Claims.Single(c => c.Type == "Id").Value);
            CheckpointsViewBag(userId);
            if (_passageService.IsAccessible(userId, checkpointId))
            {
                _passageService.PassCheckpoint(userId, checkpointId);
            }
            else
            {
                ModelState.AddModelError("", "Недостаточно прав для прохождения КПП");
            }

            var model = new PassCheckpointRequest()
            {
                Room = _passageService.GetUserCurrentRoom(userId)
            };
            return View(model);
        }

        #region Private

        private void CheckpointsViewBag(int userId)
        {
            ViewBag.Checkpoints
                = _passageService.GetUserCurrentCheckpoints(userId)
                    .Select(r => new
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToList();
        }

        #endregion
    }
}
