using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Application.Products.Querys.QuerysGetById;
using ProductService.Infrastructure.DataBases;
using System.Net;
using System.Net.Http.Json;


namespace ProductService.Tests.IntegrationTest.ProductTest
{
    public class IntegrationTestGetProdutcById : EnvironmentTestHttpClient
    {
        public IntegrationTestGetProdutcById (WebApplicationFactory<Program> factory) : base(factory)
        {

        }

        private string endPoint = "/api/product/";

        [Fact]
        public async Task GetProductById_WhenProductFound()
        {
            //Arrange
            var request = 1;

            //Act
            var response = await _client.GetAsync($"{endPoint}{request}");
            var content = await response.Content.ReadFromJsonAsync<DetailsProductDto>();


            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(content);

        }

        [Fact]
        public async Task GetProductDbyId_WhenProductNotFound()
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
