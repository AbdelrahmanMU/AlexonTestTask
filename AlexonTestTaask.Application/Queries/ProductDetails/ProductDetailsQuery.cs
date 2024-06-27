using AlexonTestTask.Application.Queries.SharedDto;
using MediatR;

namespace AlexonTestTask.Application.Queries.ProductDetails;

public class ProductDetailsQuery : IRequest<ProductDto>
{
    public Guid Id { get; set; }
}
