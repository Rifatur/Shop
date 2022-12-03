using BookShop.Domain;
using BookShop.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.presentation.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IRepository<Category> _repository;
        protected readonly ApplicationDbContext _context;
        public CategoryController(IRepository<Category> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Index()
        {
            var GetCategoryList = _context.Categories.ToList();
            return View(GetCategoryList);
        }

        public async Task<IActionResult> CreateCategory(Category model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = " Registration Failed" });
            }
            var AddCaregory = new Category
            {

                Name = model.Name,
                Description = model.Description,

            };
            await _repository.AddAsync(AddCaregory);
            return RedirectToAction("Index");

        }

    }
}
