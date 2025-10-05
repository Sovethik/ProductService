

using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Application.Products.Commands.UpdateProduct;
using ProductService.Application.Products.Querys.QuerysGetById;
using ProductService.Tests.IntegrationTest.Common;
using System.Net;
using System.Net.Http.Json;

namespace ProductService.Tests.IntegrationTest.ProductTest
{
    public class IntegrationTestUpdateProduct : EnvironmentTestHttpClient
    {
        public IntegrationTestUpdateProduct(WebApplicationFactory<Program> factory)
            : base(factory) { }

        private string pathUri = "/api/product/";

        [Fact]
        public async Task UpdateProduct_WhenProductExist_ReturnNoContent()
        {
            //Arrange
            var productId = 1;

            var updateProductCommand = new UpdateProductCommand()
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Price = 5000,
                CategoryId = 1
            };

            var request = pathUri + productId.ToString();


            //Act
            var response = await _client.PutAsJsonAsync(request, updateProductCommand);
            var responseFromGetRequest = await _client.GetFromJsonAsync<DetailsProductDto>(request);
            

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(productId, responseFromGetRequest.Id);
            Assert.Equal(updateProductCommand.Name, responseFromGetRequest.Name);
            Assert.Equal(updateProductCommand.Price, responseFromGetRequest.Price);
            
        }

        [Fact]
        public async Task UpdateProduct_WhenProductNotExist_ReturnNotFound()
        {
            //Arrange
            var productId = 101;

            var updateProductCommand = new UpdateProductCommand()
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Price = 5000,
                CategoryId = 1
            };

            var request = pathUri + productId.ToString();

            //Act
            var response = await _client.PutAsJsonAsync(request, updateProductCommand);
            var responseFromGetRequest = await _client.GetAsync(request);


            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, responseFromGetRequest.StatusCode);

        }


    }
}
