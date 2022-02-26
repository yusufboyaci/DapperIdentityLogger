using DATAACCESS.Abstract;
using ENTITIES;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UI.Models;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IUserRepository userRepository, ILogger<AccountController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        private bool LoginUser(string username, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                _logger.LogWarning($"{user.Name} {user.Surname} isimli ve {user.Id} li kullanıcı giriş yaptı");
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (LoginUser(loginViewModel.Username, loginViewModel.Password))
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,loginViewModel.Username),
                };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                User user = _userRepository.GetAll().FirstOrDefault(x => x.Username == loginViewModel.Username && x.Password == loginViewModel.Password) ?? throw new Exception("Böyle bir kullanıcı bulunmamaktadır");
                HttpContext.Session.SetString("userId", user.Id);
                await HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            //var user = _userRepository.GetById(model.);
            //_logger.LogWarning($"{user.Name} {user.Surname} isimli ve {user.Id} li kullanıcı çıkış yaptı");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
