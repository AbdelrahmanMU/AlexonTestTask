using ExperimentsDemo.Application.Queries.SharedDto;
using ExperimentsDemo.Core.Interfaces;
using ExperimentsDemo.Core.Models;
using Mapster;
using MediatR;

namespace ExperimentsDemo.Application.Queries.ProductList;

public class ProductListQueryHandler(IBaseRepository<Product> productRepository) : IRequestHandler<ProductListQuery, List<ProductDto>>
{
    private readonly IBaseRepository<Product> _productRepository = productRepository;

    public async Task<List<ProductDto>> Handle(ProductListQuery query, CancellationToken cancellationToken)
    {
        return _productRepository.GetAll(["Categories"]).Adapt<List<ProductDto>>();
    }
}
