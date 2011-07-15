using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Mvc3Depot.Models
{
    public class Mvc3DepotInitializer : DropCreateDatabaseIfModelChanges<Mvc3DepotContext>
    {
        protected override void Seed(Mvc3DepotContext context)
        {
            var products = new List<Product>
            {
                new Product() {
                    Title = "Necronomicon",
                    Description = "According to this account, the book was originally called Al Azif, an Arabic word that Lovecraft defined as \"that nocturnal sound (made by insects) supposed to be the howling of demons\" (one Arabic/English dictionary translates `Azīf as \"whistling (of the wind); weird sound or noise\".)",
                    ImageUrl = "/images/necr.png",
                    Price = 42.95m
                },
                new Product() {
                    Title = "Celaeno Fragments",
                    Description = "Professor Laban Shrewsbury and his companions traveled to Celaeno several times to escape Cthulhu's minions. Shrewsbury later wrote the Celaeno Fragments, a transcript of what he remembered of his translations of the books in the Great Library of Celaeno.",
                    ImageUrl = "/images/cela.png",
                    Price = 49.50m
               },
                new Product() {
                    Title = "Pnakotic Manuscript",
                    Description = "The original manuscripts were in scroll form and were passed down through the ages, eventually falling into the hands of secretive cults. The Great Race of Yith is believed to have produced the first five chapters of the Manuscripts, which, among other things, contain a detailed chronicle of the race's history.",
                    ImageUrl = "/images/pnak.png",
                    Price = 43.75m
                }
            };
            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}