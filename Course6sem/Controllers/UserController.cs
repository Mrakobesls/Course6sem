using Application.Models.User;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ShemTeh.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IAccessLevelService _accessLevelsService;
        private readonly IPositionService _positionService;

        public UserController(IUserService userService,
            IRoleService roleService,
            IAccessLevelService accessLevelsService,
            IPositionService positionService)
        {
            _userService = userService;
            _roleService = roleService;
            _accessLevelsService = accessLevelsService;
            _positionService = positionService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Authenticate(model.Login, model.Password);
                if (user is not null)
                {
                    if (user.IsDisabled)
                    {
                        ModelState.AddModelError("", "Аккаунт заблокирован администратором");
                    }
                    else
                    {
                        await Authorize(user); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            RolesViewBag();
            AccessLevelsViewBag();
            PositionsViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            RolesViewBag();
            AccessLevelsViewBag();
            PositionsViewBag();
            if (ModelState.IsValid)
            {
                if (!_userService.PersonExists(model.Login))
                {
                    _userService.Create(new User
                    {
                        Login = model.Login,
                        Password = model.Password,
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        Patronymic = model.Surname,
                        RoleId = model.RoleId,
                        PositionId = model.PositionId,
                        AccessLevelsId = model.AccessLevelsId
                    });

                    //var user = _userService.GetByLogin(model.Login);

                    //await Authorize(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует!");
            }

            return View(model);
        }

        private async Task Authorize(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, _roleService.Get(user.RoleId).Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword(ChangePasswordRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var id = Int32.Parse(HttpContext.User.Claims.Single(c => c.Type == "Id").Value);
            if (_userService.PasswordsMatch(id, model.OldPassword))
            {
                _userService.UpdatePassword(id, model.NewPassword);
                return RedirectToAction("Logout", "User");
            }
            else
            {
                ModelState.AddModelError("", "Неверный старый пароль");
                return View(model);
            }
        }



        [HttpGet]
        [Authorize]
        public IActionResult Users()
        {
            var viewModel = new UserListResponse();

            viewModel.Users = _userService.GetAll();

            return View("UserList", viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateUsers(UserListResponse model)
        {
            _userService.UpdateUsers(model.Users);
            return Users();
        }

        #region Private

        private void AccessLevelsViewBag()
        {
            ViewBag.AccessLevels = _accessLevelsService.GetAll()
                                    .Select(r => new
                                    {
                                        Id = r.Id,
                                        Name = r.Name
                                    }).ToList();
        }

        private void RolesViewBag()
        {
            ViewBag.Roles = _roleService.GetAll()
                                    .Select(r => new
                                    {
                                        Id = r.Id,
                                        Name = r.Name
                                    }).ToList();
        }

        private void PositionsViewBag()
        {
            ViewBag.Positions = _positionService.GetAll()
                                    .Select(r => new
                                    {
                                        Id = r.Id,
                                        Name = r.Name
                                    }).ToList();
        }

        #endregion
    }
}
