using AlexonTestTask.Application.Commands.CreateProduct;
using AlexonTestTask.Application.Commands.UpdateProduct;
using AlexonTestTask.Application.Queries.SharedDto;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace AlexonTestTask.Test.API;

public class ProductsControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CreateProduct_ReturnsGuid()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 99.99M,
            CategoriesIds = [Guid.NewGuid()]
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products/", command);
        response.EnsureSuccessStatusCode();

        var productId = await response.Content.ReadFromJsonAsync<Guid>();

        // Assert
        productId.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsUnit()
    {
        // Arrange
        var createCommand = new CreateProductCommand
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 99.99M,
            CategoriesIds = [Guid.NewGuid()]
        };
        var createResponse = await _client.PostAsJsonAsync("/api/products/", createCommand);
        createResponse.EnsureSuccessStatusCode();
        var productId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var updateCommand = new UpdateProductCommand
        {
            Id = productId,
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 199.99M,
            CategoriesIds = [Guid.NewGuid()]
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/products/{productId}", updateCommand);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Unit>();

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsUnit()
    {
        // Arrange
        var createCommand = new CreateProductCommand
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 99.99M,
            CategoriesIds = [Guid.NewGuid()]
        };
        var createResponse = await _client.PostAsJsonAsync("/api/products/", createCommand);
        createResponse.EnsureSuccessStatusCode();
        var productId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var response = await _client.DeleteAsync($"/api/products/{productId}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Unit>();

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GetProductList_ReturnsProductList()
    {
        // Act
        var response = await _client.GetAsync("/api/products/");
        response.EnsureSuccessStatusCode();

        var productList = await response.Content.ReadFromJsonAsync<List<ProductDto>>();

        // Assert
        productList.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProductDetails_ReturnsProduct()
    {
        // Arrange
        var createCommand = new CreateProductCommand
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 99.99M,
            CategoriesIds = [Guid.NewGuid()]
        };
        var createResponse = await _client.PostAsJsonAsync("/api/products/", createCommand);
        createResponse.EnsureSuccessStatusCode();
        var productId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var response = await _client.GetAsync($"/api/products/{productId}");
        response.EnsureSuccessStatusCode();

        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        // Assert
        product.Should().NotBeNull();
        product?.Id.Should().Be(productId);
    }
}
