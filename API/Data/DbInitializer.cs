using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            if (context.Products.Any()) return;
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Kulta katriina perinteinen",
                    Description = " hienoarominen kahviklassikko",
                    Price = 5,
                    PictureUrl = "",
                    Brand = "Meira",
                    Type = "Suodatinkahvi",
                    QuantityInStock = 50
                },
            };
            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}