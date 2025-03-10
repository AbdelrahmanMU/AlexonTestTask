using MediatR;
using System.Text.Json.Serialization;

namespace ExperimentsDemo.Application.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
