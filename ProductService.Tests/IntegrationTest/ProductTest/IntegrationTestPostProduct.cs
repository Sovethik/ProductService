using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Tests.IntegrationTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.IntegrationTest.ProductTest
{
    public class IntegrationTestPostProduct : EnvironmentTestHttpClient
    {
        public IntegrationTestPostProduct(WebApplicationFactory<Program> factory)
            : base(factory) { }

        private string endPoint = "/api/product";

        [Fact]
        public async Task PostProduct_WhenValidRequest()
        {
            //Arrange

            var request = new CreateProductCommand()
            {
                Name = "Мышка",
                Price = 1500,
                Description = "Игровая",
                CategoryId = 1
            };


            //Act
            var response = await _client.PostAsJsonAsync(endPoint, request);
            var id = await response.Content.ReadFromJsonAsync<int>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(id > 0);

        }

        [Fact]
        public async Task PostProduct_WhenRequestNoValid()
        {
            //Arrange
            var request = new CreateProductCommand()
            {
                Name = "Мышка",
                Price = 1500,
                Description = "Игровая",
                CategoryId = 0
            };

            //Act
            var response = await _client.PostAsJsonAsync(endPoint, request);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
           
        }

    }
}
