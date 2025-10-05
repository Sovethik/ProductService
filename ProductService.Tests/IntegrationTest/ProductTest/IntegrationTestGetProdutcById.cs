using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Application.Products.Querys.QuerysGetById;
using ProductService.Infrastructure.DataBases;
using ProductService.Tests.IntegrationTest.Common;
using System.Net;
using System.Net.Http.Json;


namespace ProductService.Tests.IntegrationTest.ProductTest
{
    public class IntegrationTestGetProdutcById : EnvironmentTestHttpClient
    {
        public IntegrationTestGetProdutcById (WebApplicationFactory<Program> factory)
            : base(factory) { }

        private string endPoint = "/api/product/";

        [Fact]
        public async Task GetProductById_WhenProductExist_ReturnOk()
        {
            //Arrange
            var request = 1;

            //Act
            var response = await _client.GetAsync($"{endPoint}{request}");
            var content = await response.Content.ReadFromJsonAsync<DetailsProductDto>();


            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(content);
            Assert.Equal(request, content.Id);
            

        }

        [Fact]
        public async Task GetProductDbyId_WhenProductDoesNotExist_ReturnNotFound()
        {
            //Arrange
            var request = 101;

            //Act
            var response = await _client.GetAsync($"{endPoint}{request}");


            //Assert
            Assert.Equal (HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
