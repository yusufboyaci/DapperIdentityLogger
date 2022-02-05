using DATAACCESS.Abstract;
using ENTITIES;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Insert() => View();
        [HttpPost]
        public IActionResult Insert(Product product)
        {
            product.UserId = HttpContext.Session.GetString("userId") ?? throw new Exception("Uye Bulunmamaktadır.");
            _productRepository.InsertProduct(product);
            
            return RedirectToAction("Index","Product");
        }
        [HttpGet]
        public IActionResult Update(Guid id) => View(_productRepository.GetById(id));
        [HttpPost]
        public IActionResult Update(Product product)
        {
            product.UserId = HttpContext.Session.GetString("userId") ?? throw new Exception("Uye Bulunmamaktadır.");
            _productRepository.UpdateProduct(product);
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Delete(Guid id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Index", "Product");
        }
        [HttpGet]
        public IActionResult ProductInfo(Guid id) => View(_productRepository.GetById(id));
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
