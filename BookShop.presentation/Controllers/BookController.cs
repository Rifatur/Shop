using BookShop.Domain;
using BookShop.Domain.Entities;
using BookShop.Domain.Repository;
using BookShop.presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Delete(Guid? Id)
        {
            var imageModel = await _context.Products.FindAsync(Id);
            string folder = "Image/cover/";
            //delete image from wwwroot/image
            if (imageModel.ImageUrl != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image/cover/", imageModel.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
                //Delete that Product
                _context.Products.Remove(imageModel);
                //Commit the transaction
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //Delete that Product
            _context.Products.Remove(imageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        //Product Details Get By Id = 1 
        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            Product product = new Product();
            var result = await _context.Products.FirstOrDefaultAsync(u => u.Id == Id);
            if (result! == null)
            {
                product.Title = result.Title;
            }
            return View(result);
        }
        //Update
        public IActionResult Update(Guid? Id)
        {
            //Find the product for specific post id
            var productIsExit = _context.Products.FirstOrDefault(u => u.Id == Id);

            return View();
        }

        public IActionResult Update(ProductViewModel book)
        {
            bool IsBookExist = false;
            Product findbook = _context.Products.Find(book.Id);
            if (findbook != null)
            {
                IsBookExist = true;
            }
            findbook.Title = book.Title;
            findbook.Description = book.Description;
            findbook.ISBN = book.ISBN;
            findbook.Author = book.Author;

            if (ModelState.IsValid)
            {
                if (IsBookExist)
                {
                    _context.Update(findbook);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }



    }
}
