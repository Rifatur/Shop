using BookShop.Domain.Entities;
using BookShop.presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.presentation.Controllers
{
    public class BookController : Controller
    {
        protected readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewBook(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CoverPhoto != null)
                {
                    string folder = "book/cover/";
                    folder += Guid.NewGuid().ToString() + "_" + model.CoverPhoto.FileName;
                    model.ImageUrl = folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                var CreateBook = new Product();
                {
                    CreateBook.Title = model.Title;
                    CreateBook.Description = model.Description;
                    CreateBook.Author = model.Author;
                    CreateBook.ISBN = model.ISBN;
                };


            }
            return View(model);
        }
    }
}
