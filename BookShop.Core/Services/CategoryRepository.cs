using BookShop.Domain;
using BookShop.Domain.Repository;

namespace BookShop.Core.Services
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        protected readonly ApplicationDbContext _Context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
