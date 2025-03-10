using ExperimentsDemo.Application.Commands.DeleteProduct;
using ExperimentsDemo.Application.Queries.ProductDetails;
using ExperimentsDemo.Application.Queries.ProductList;
using ExperimentsDemo.Application.Commands.CreateProduct;
using ExperimentsDemo.Application.Commands.UpdateProduct;
using ExperimentsDemo.Application.Queries.SharedDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentsDemo.API.Controllers
{
    [Route("api/products/")]
    public class ProductsController(ISender sender) : ControllerBase
    {
        public ISender Mediator { get; protected set; } = sender;

        #region Commands

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Unit))]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Unit))]
        public async Task<ActionResult<Unit>> Delete([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
        }


        #endregion

        #region Queries

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        public async Task<ActionResult<List<ProductDto>>> ProductList()
        {
            return Ok(await Mediator.Send(new ProductListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        public async Task<ActionResult<ProductDto>> ProductDetails([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new ProductDetailsQuery { Id = id }));
        }

        #endregion
    }
}
