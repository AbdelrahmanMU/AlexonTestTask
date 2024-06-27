using MediatR;
using System.Text.Json.Serialization;

namespace AlexonTestTask.Application.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
