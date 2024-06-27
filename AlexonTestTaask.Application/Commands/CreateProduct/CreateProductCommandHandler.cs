using AlexonTestTask.Core;
using AlexonTestTask.Core.Interfaces;
using AlexonTestTask.Core.Models;
using MediatR;

namespace AlexonTestTask.Application.Commands.CreateProduct;

public class CreateProductCommandHandler(IBaseRepository<Product> productRepository,
    IBaseRepository<Category> categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Guid>
{

    private readonly IBaseRepository<Product> _productRepository = productRepository;
    private readonly IBaseRepository<Category> _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetAll().Where(x => request.CategoriesIds.Contains(x.Id));

        var newProduct = new Product()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Categories = [.. categories],
        };

        var product = await _productRepository.CreateAsync(newProduct, cancellationToken);
        
        _unitOfWork.Complete();

        return product.Id;
    }
}
