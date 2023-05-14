namespace VendomaticApi.Controllers.v1;

using VendomaticApi.Domain.MachineOperators.Features;
using VendomaticApi.Domain.MachineOperators.Dtos;
using VendomaticApi.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/machineoperators")]
[ApiVersion("1.0")]
public sealed class MachineOperatorsController: ControllerBase
{
    private readonly IMediator _mediator;

    public MachineOperatorsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new MachineOperator record.
    /// </summary>
    [HttpPost(Name = "AddMachineOperator")]
    public async Task<ActionResult<MachineOperatorDto>> AddMachineOperator([FromBody]MachineOperatorForCreationDto machineOperatorForCreation)
    {
        var command = new AddMachineOperator.Command(machineOperatorForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetMachineOperator",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single MachineOperator by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetMachineOperator")]
    public async Task<ActionResult<MachineOperatorDto>> GetMachineOperator(Guid id)
    {
        var query = new GetMachineOperator.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all MachineOperators.
    /// </summary>
    [HttpGet(Name = "GetMachineOperators")]
    public async Task<IActionResult> GetMachineOperators([FromQuery] MachineOperatorParametersDto machineOperatorParametersDto)
    {
        var query = new GetMachineOperatorList.Query(machineOperatorParametersDto);
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
    /// Updates an entire existing MachineOperator.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateMachineOperator")]
    public async Task<IActionResult> UpdateMachineOperator(Guid id, MachineOperatorForUpdateDto machineOperator)
    {
        var command = new UpdateMachineOperator.Command(id, machineOperator);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing MachineOperator record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteMachineOperator")]
    public async Task<ActionResult> DeleteMachineOperator(Guid id)
    {
        var command = new DeleteMachineOperator.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
