using Shop.Domain.Entities.Common;

namespace BookShop.Domain
{
    public class Category : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public int DisplayOrder { get; set; }
        public string? Description { get; set; }
    }
}
