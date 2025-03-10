using ExperimentsDemo.Application.Queries.SharedDto;
using MediatR;

namespace ExperimentsDemo.Application.Queries.ProductDetails;

public class ProductDetailsQuery : IRequest<ProductDto>
{
    public Guid Id { get; set; }
}
