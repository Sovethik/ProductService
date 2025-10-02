using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Application.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.IntegrationTest.ProductTest
{
    public class IntegrationTestGetPageProducts : EnvironmentTestHttpClient
    {
        public IntegrationTestGetPageProducts(WebApplicationFactory<Program> factory) : base(factory)
        { }

        private string endPoint = "/api/Product?numberPage=";

        [Fact]
        public async Task GetPageProducts_WhenPageFound()
        {
            //Arrange
            var request = 1;

            //Act
            var response = await _client.GetAsync($"{endPoint}{request}");


            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

        [Fact]
        public async Task GetPageProducts_WhenPageNotFound()
        {
            //Arrange
            var request = 11;

            //Act
            var response = await _client.GetAsync($"{endPoint}{request}");


            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);


        }

    }
}
