using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc3Depot.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required/* Unique check is not implemented because EF4.1 does not have unique validations */]
        public string Title { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.ImageUrl)]
        [Required, RegularExpression(@"^.+?\.(png|gif|jpg)$",
            ErrorMessage = "The {0} must be a URL for GIF, JPG or PNG image")]
        public string ImageUrl { get; set; }
        [Range(0.01, Double.MaxValue), DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Timestamp, ConcurrencyCheck]
        public byte[] Timestamp { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}