using AlexonTestTask.Application.Queries.SharedDto;
using AlexonTestTask.Core.Interfaces;
using AlexonTestTask.Core.Models;
using Mapster;
using MediatR;

namespace AlexonTestTask.Application.Queries.ProductList;

public class ProductListQueryHandler(IBaseRepository<Product> productRepository) : IRequestHandler<ProductListQuery, List<ProductDto>>
{
    private readonly IBaseRepository<Product> _productRepository = productRepository;

    public async Task<List<ProductDto>> Handle(ProductListQuery query, CancellationToken cancellationToken)
    {
        return _productRepository.GetAll(["Categories"]).Adapt<List<ProductDto>>();
    }
}
