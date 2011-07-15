using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc3Depot.Models
{
    public class LineItem : IEquatable<LineItem>
    {
        public int LineItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required, ScaffoldColumn(false)]
        public Guid CartId { get; set; }
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
        [Timestamp, ConcurrencyCheck]
        public byte[] Timestamp { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }

        public bool Equals(LineItem other)
        {
            bool result = false;
            if (other != null)
            {
                result = this.ProductId.Equals(other.ProductId) && this.CartId.Equals(other.CartId);
            }
            return result;
        }

        public decimal TotalPrice()
        {
            return Product.Price * Quantity;
        }
    }
}