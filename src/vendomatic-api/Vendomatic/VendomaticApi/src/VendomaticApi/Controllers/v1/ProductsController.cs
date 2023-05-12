namespace VendomaticApi.Controllers.v1;

using VendomaticApi.Domain.Products.Features;
using VendomaticApi.Domain.Products.Dtos;
using VendomaticApi.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/products")]
[ApiVersion("1.0")]
public sealed class ProductsController: ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Product record.
    /// </summary>
    [HttpPost(Name = "AddProduct")]
    public async Task<ActionResult<ProductDto>> AddProduct([FromBody]ProductForCreationDto productForCreation)
    {
        var command = new AddProduct.Command(productForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetProduct",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Product by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var query = new GetProduct.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Products.
    /// </summary>
    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetProducts([FromQuery] ProductParametersDto productParametersDto)
    {
        var query = new GetProductList.Query(productParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Updates an entire existing Product.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductForUpdateDto product)
    {
        var command = new UpdateProduct.Command(id, product);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Product record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteProduct")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var command = new DeleteProduct.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
