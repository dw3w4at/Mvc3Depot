using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Mvc3Depot.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public int PayType { get; set; }
        [Timestamp, ConcurrencyCheck]
        public byte[] Timestamp { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}