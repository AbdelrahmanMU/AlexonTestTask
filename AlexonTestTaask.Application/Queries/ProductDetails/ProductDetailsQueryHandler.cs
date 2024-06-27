using AlexonTestTask.Application.Queries.SharedDto;
using AlexonTestTask.Core.Interfaces;
using AlexonTestTask.Core.Models;
using Mapster;
using MediatR;

namespace AlexonTestTask.Application.Queries.ProductDetails;

public class ProductDetailsQueryHandler(IBaseRepository<Product> productRepository) : IRequestHandler<ProductDetailsQuery, ProductDto>
{
    private readonly IBaseRepository<Product> _productRepository = productRepository;

    public async Task<ProductDto> Handle(ProductDetailsQuery query, CancellationToken cancellationToken)
    {
        var productDetails = await _productRepository.GetByIdAsync(query.Id, cancellationToken, ["Categories"]);
        return productDetails.Adapt<ProductDto>();
    }
}
