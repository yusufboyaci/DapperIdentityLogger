using DATAACCESS.Abstract;
using ENTITIES;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(_userRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Insert() => View();
        [HttpPost]
        public IActionResult Insert(User user)
        {
            _userRepository.InsertUser(user);
            _logger.LogInformation("Kullanıcı eklendi");//Console da log u gösterir.
            return RedirectToAction("Index","User");
        }
        [HttpGet]
        public IActionResult Update(Guid id) => View(_userRepository.GetById(id));
        [HttpPost]
        public IActionResult Update(User user)
        {
            _userRepository.UpdateUser(user);
            _logger.LogInformation("Kullanıcı güncellendi.");
            return RedirectToAction("Index", "User");
        }
        public IActionResult Delete(Guid id)
        {
            _userRepository.DeleteUser(id);
            _logger.LogCritical("Kullanıcı silindi.");
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public IActionResult UserInfo(Guid id) => View(_userRepository.GetById(id));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
