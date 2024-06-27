using AlexonTestTask.Core;
using AlexonTestTask.Core.Interfaces;
using AlexonTestTask.Core.Models;
using MediatR;

namespace AlexonTestTask.Application.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IBaseRepository<Product> productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IBaseRepository<Product> _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var oldProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        _productRepository.Delete(oldProduct);

        _unitOfWork.Complete();

        return Unit.Value;
    }
}
