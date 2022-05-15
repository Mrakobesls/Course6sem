using Application.Models.Admin;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IBackupService _backupService;
        public AdminController(IBackupService backupService)
        {
            _backupService = backupService;
        }

        [HttpGet]
        public IActionResult Backups()
        {
            var viewModel = new BackupListResponse();

            viewModel.Backups = _backupService.GetBackUps();
            viewModel.Backups.ForEach(b => b.Name = b.Name.Remove(b.Name.Length - 4, 4));

            return View("BackupList", viewModel);
        }

        [HttpGet]
        public IActionResult CreateBackup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBackup(CreateBackupRequest createBackupRequest)
        {
            if (_backupService.GetBackUps()
                    .Any(b => b.Name == createBackupRequest.Name + ".txt"))
            {
                ModelState.AddModelError("", "Резервная копияа с таким наpванием уже существует");
                return View(createBackupRequest);
            }
            _backupService.MakeBackUp(createBackupRequest.Name + ".txt");

            return Backups();
        }

        [HttpGet]
        public IActionResult DeleteBackup(string backupName)
        {
            if (!_backupService.GetBackUps()
                    .Any(b => b.Name == backupName + ".txt"))
            {
                ModelState.AddModelError("", "Резервная копияа с таким наpванием не существует");
                return Backups();
            }
            _backupService.DeleteBackup(new Backup { Name = backupName });

            return Backups();
        }



        [HttpGet]
        public IActionResult MakeRestore(string backupName)
        {
            _backupService.MakeRestore(backupName);
            return Backups();
        }
    }
}
