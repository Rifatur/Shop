namespace BookShop.Domain.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void UpdateCategory(Category category);
    }
}
