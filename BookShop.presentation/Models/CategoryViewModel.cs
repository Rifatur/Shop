namespace BookShop.presentation.Models
{
    public class CategoryViewModel
    {
        public virtual Guid Id { get; set; }
        public string? Name { get; set; }
        public int DisplayOrder { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
