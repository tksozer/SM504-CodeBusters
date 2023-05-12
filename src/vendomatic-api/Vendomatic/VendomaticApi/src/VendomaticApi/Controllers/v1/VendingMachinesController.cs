namespace VendomaticApi.Controllers.v1;

using VendomaticApi.Domain.VendingMachines.Features;
using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/vendingmachines")]
[ApiVersion("1.0")]
public sealed class VendingMachinesController: ControllerBase
{
    private readonly IMediator _mediator;

    public VendingMachinesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new VendingMachine record.
    /// </summary>
    [HttpPost(Name = "AddVendingMachine")]
    public async Task<ActionResult<VendingMachineDto>> AddVendingMachine([FromBody]VendingMachineForCreationDto vendingMachineForCreation)
    {
        var command = new AddVendingMachine.Command(vendingMachineForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetVendingMachine",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single VendingMachine by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetVendingMachine")]
    public async Task<ActionResult<VendingMachineDto>> GetVendingMachine(Guid id)
    {
        var query = new GetVendingMachine.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all VendingMachines.
    /// </summary>
    [HttpGet(Name = "GetVendingMachines")]
    public async Task<IActionResult> GetVendingMachines([FromQuery] VendingMachineParametersDto vendingMachineParametersDto)
    {
        var query = new GetVendingMachineList.Query(vendingMachineParametersDto);
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
    /// Updates an entire existing VendingMachine.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateVendingMachine")]
    public async Task<IActionResult> UpdateVendingMachine(Guid id, VendingMachineForUpdateDto vendingMachine)
    {
        var command = new UpdateVendingMachine.Command(id, vendingMachine);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing VendingMachine record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteVendingMachine")]
    public async Task<ActionResult> DeleteVendingMachine(Guid id)
    {
        var command = new DeleteVendingMachine.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
