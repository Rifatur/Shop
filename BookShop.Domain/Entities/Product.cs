using Shop.Domain.Entities;
using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string ISBN { get; set; }
        public string Author { get; set; }
        [Required]
        [Range(1, 10000)]
        public double LastPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
