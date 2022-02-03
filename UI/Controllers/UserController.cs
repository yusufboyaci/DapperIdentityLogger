using DATAACCESS.Abstract;
using ENTITIES;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            return RedirectToAction("Index","User");
        }
        [HttpGet]
        public IActionResult Update(Guid id) => View(_userRepository.GetById(id));
        [HttpPost]
        public IActionResult Update(User user)
        {
            _userRepository.UpdateUser(user);
            return RedirectToAction("Index", "User");
        }
        public IActionResult Delete(Guid id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Index", "User");
        }
    }
}
