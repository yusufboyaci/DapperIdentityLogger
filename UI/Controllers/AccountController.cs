using DATAACCESS.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UI.Models;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims,"login");
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Index", "User");   
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    } 
}
