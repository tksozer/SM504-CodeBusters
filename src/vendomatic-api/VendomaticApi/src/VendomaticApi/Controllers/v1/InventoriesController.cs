namespace VendomaticApi.Controllers.v1;

using VendomaticApi.Domain.Inventories.Features;
using VendomaticApi.Domain.Inventories.Dtos;
using VendomaticApi.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/inventories")]
[ApiVersion("1.0")]
public sealed class InventoriesController: ControllerBase
{
    private readonly IMediator _mediator;

    public InventoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Inventory record.
    /// </summary>
    [HttpPost(Name = "AddInventory")]
    public async Task<ActionResult<InventoryDto>> AddInventory([FromBody]InventoryForCreationDto inventoryForCreation)
    {
        var command = new AddInventory.Command(inventoryForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetInventory",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Inventory by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetInventory")]
    public async Task<ActionResult<InventoryDto>> GetInventory(Guid id)
    {
        var query = new GetInventory.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Inventories.
    /// </summary>
    [HttpGet(Name = "GetInventories")]
    public async Task<IActionResult> GetInventories([FromQuery] InventoryParametersDto inventoryParametersDto)
    {
        var query = new GetInventoryList.Query(inventoryParametersDto);
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
    /// Updates an entire existing Inventory.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateInventory")]
    public async Task<IActionResult> UpdateInventory(Guid id, InventoryForUpdateDto inventory)
    {
        var command = new UpdateInventory.Command(id, inventory);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Inventory record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteInventory")]
    public async Task<ActionResult> DeleteInventory(Guid id)
    {
        var command = new DeleteInventory.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
