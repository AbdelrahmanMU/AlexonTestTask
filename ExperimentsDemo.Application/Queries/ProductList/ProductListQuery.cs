using ExperimentsDemo.Application.Queries.SharedDto;
using MediatR;

namespace ExperimentsDemo.Application.Queries.ProductList;

public class ProductListQuery : IRequest<List<ProductDto>>
{

}
