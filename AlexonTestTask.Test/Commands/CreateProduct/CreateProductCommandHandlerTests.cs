using AlexonTestTask.Application.Commands.CreateProduct;
using AlexonTestTask.Core;
using AlexonTestTask.Core.Interfaces;
using AlexonTestTask.Core.Models;
using Moq;

namespace AlexonTestTask.Test.Commands.CreateProduct

{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IBaseRepository<Product>> _productRepositoryMock;
        private readonly Mock<IBaseRepository<Category>> _categoryRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IBaseRepository<Product>>();
            _categoryRepositoryMock = new Mock<IBaseRepository<Category>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateProductCommandHandler(
                _productRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsProductId()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 99.99M,
                CategoriesIds = [Guid.NewGuid()]
            };

            var categories = new List<Category>
            {
                new Category { Id = command.CategoriesIds.First(), Name = "Test Category" }
            }.AsQueryable();

            _categoryRepositoryMock.Setup(repo => repo.GetAll(null)).Returns(categories);
            _productRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(new Product { Id = Guid.NewGuid() });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _productRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Complete(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCategories_ThrowsException()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 99.99M,
                CategoriesIds = [Guid.NewGuid()]
            };

            _categoryRepositoryMock.Setup(repo => repo.GetAll(null)).Returns(Enumerable.Empty<Category>().AsQueryable());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            _productRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Complete(), Times.Never);
        }

    }
}

