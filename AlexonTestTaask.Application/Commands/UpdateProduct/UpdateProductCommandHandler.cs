using AlexonTestTask.Core;
using AlexonTestTask.Core.Interfaces;
using AlexonTestTask.Core.Models;
using MediatR;

namespace AlexonTestTask.Application.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IBaseRepository<Product> productRepository,
    IBaseRepository<Category> categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IBaseRepository<Product> _productRepository = productRepository;
    private readonly IBaseRepository<Category> _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var oldProduct = await _productRepository
            .GetByIdAsync(request.Id, cancellationToken, ["Categories"]);

        var newCategories = _categoryRepository
            .GetAll()
            .Where(x => request.CategoriesIds.Contains(x.Id));

        
        oldProduct.Name = request.Name;
        oldProduct.Description = request.Description;
        oldProduct.Price = request.Price;
        oldProduct.Categories = [.. newCategories];

        _unitOfWork.Complete();

        return Unit.Value;
    }
}
