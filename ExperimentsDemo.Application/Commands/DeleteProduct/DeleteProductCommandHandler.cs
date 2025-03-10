using ExperimentsDemo.Core;
using ExperimentsDemo.Core.Interfaces;
using ExperimentsDemo.Core.Models;
using MediatR;

namespace ExperimentsDemo.Application.Commands.DeleteProduct;

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
