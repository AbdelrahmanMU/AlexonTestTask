using ExperimentsDemo.Application.Queries.SharedDto;
using ExperimentsDemo.Core.Interfaces;
using ExperimentsDemo.Core.Models;
using Mapster;
using MediatR;

namespace ExperimentsDemo.Application.Queries.ProductDetails;

public class ProductDetailsQueryHandler(IBaseRepository<Product> productRepository) : IRequestHandler<ProductDetailsQuery, ProductDto>
{
    private readonly IBaseRepository<Product> _productRepository = productRepository;

    public async Task<ProductDto> Handle(ProductDetailsQuery query, CancellationToken cancellationToken)
    {
        var productDetails = await _productRepository.GetByIdAsync(query.Id, cancellationToken, ["Categories"]);
        return productDetails.Adapt<ProductDto>();
    }
}
