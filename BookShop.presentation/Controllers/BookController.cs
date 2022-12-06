using BookShop.Domain;
using BookShop.Domain.Entities;
using BookShop.Domain.Repository;
using BookShop.presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.presentation.Controllers
{
    public class BookController : Controller
    {

        private readonly IRepository<Product> _repository;
        protected readonly ApplicationDbContext _context;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IWebHostEnvironment webHostEnvironment, IRepository<Product> repository, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _repository = repository;
            _context = context;
        }

        public IActionResult Index()
        {
            var GetProducts = _context.Products.ToList();
            return View(GetProducts);
        }

        public IActionResult CreateBook()
        {
            var GetCategoryList = _context.Categories.ToList();
            return View(GetCategoryList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBook(ProductViewModel model)
        {


            if (model.CoverPhoto != null)
            {
                string folder = "Image/cover/";
                folder += Guid.NewGuid().ToString() + "_" + model.CoverPhoto.FileName;
                model.ImageUrl = folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await model.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }

            var CreateBook = new Product();
            {
                CreateBook.Title = model.Title;
                CreateBook.Description = model.Description;
                CreateBook.ISBN = model.ISBN;
                CreateBook.Author = model.Author;
                CreateBook.LastPrice = model.LastPrice;
                CreateBook.Price = model.Price;
                CreateBook.Price50 = model.Price50;
                CreateBook.Price100 = model.Price100;
                CreateBook.ImageUrl = model.ImageUrl;
                CreateBook.CategoryId = model.CategoryId;

            };
            await _repository.AddAsync(CreateBook);
            return RedirectToAction("Index");


        }
    }
}
