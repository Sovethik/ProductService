using ProductService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.IntegrationTest.Common
{
    public static class TestDataForDataBase
    {
        public static Category GetCategory()
        {
            return new Category() {TypeCategory = "Электронника" };
        }

        public static Product GetProduct()
        {
            int categoryId = 1;
            return new Product() 
            { 
                Name = "Клавиатура", 
                Price = 1000,
                Description = "Механическая",
                CategoryId = categoryId,
                
            };
        }
    }
}
