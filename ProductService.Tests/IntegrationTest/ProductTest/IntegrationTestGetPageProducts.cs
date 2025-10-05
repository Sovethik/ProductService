using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Application.Products.Querys.QueryGetPage;
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
    public class IntegrationTestGetPageProducts : EnvironmentTestHttpClient
    {
        public IntegrationTestGetPageProducts(WebApplicationFactory<Program> factory) 
            : base(factory) { }

        private string endPoint = "/api/Product?numberPage=";

        [Fact]
        public async Task GetPageProducts_WhenPageExist_ReturnOk()
        {
            //Arrange
            var request = 1;
            var countProducts = 1;

            //Act
            var response = await _client.GetAsync($"{endPoint}{request}");
            var content = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(content);
            Assert.Equal(countProducts, content.Count());


        }

        [Fact]
        public async Task GetPageProducts_WhenPageDoesNot_ReturnNotFound()
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
