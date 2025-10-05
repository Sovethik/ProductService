using Microsoft.AspNetCore.Mvc.Testing;
using ProductService.Tests.IntegrationTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.IntegrationTest.ProductTest
{
    public class IntegrationTestDeleteProduct : EnvironmentTestHttpClient
    {
        public IntegrationTestDeleteProduct(WebApplicationFactory<Program> factory)
            : base(factory) { }

        private string pathUri = "/api/product/";

        [Fact]
        public async Task DeleteProduct_WhenProductExist_ReturnNoContent()
        {
            //Arrange
            var productId = 1;
            var pathRequest = pathUri + productId.ToString();

            //Act
            var response = await _client.DeleteAsync(pathRequest);


            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var responsFromGetRequest = await _client.GetAsync(pathRequest);

            Assert.Equal(HttpStatusCode.NotFound, responsFromGetRequest.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_WhenProductNotExist_ReturnNotFound()
        {
            //Arrange
            var productId = 101;
            var pathRequest = pathUri + productId.ToString();

            //Act
            var response = await _client.DeleteAsync(pathRequest);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
    }
}
