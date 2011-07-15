using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Mvc3Depot.Models
{
    public class Mvc3DepotContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}