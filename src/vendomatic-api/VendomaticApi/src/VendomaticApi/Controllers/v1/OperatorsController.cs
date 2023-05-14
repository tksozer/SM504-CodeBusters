namespace VendomaticApi.Controllers.v1;

using VendomaticApi.Domain.Operators.Features;
using VendomaticApi.Domain.Operators.Dtos;
using VendomaticApi.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/operators")]
[ApiVersion("1.0")]
public sealed class OperatorsController: ControllerBase
{
    private readonly IMediator _mediator;

    public OperatorsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Operator record.
    /// </summary>
    [HttpPost(Name = "AddOperator")]
    public async Task<ActionResult<OperatorDto>> AddOperator([FromBody]OperatorForCreationDto operatorForCreation)
    {
        var command = new AddOperator.Command(operatorForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetOperator",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Operator by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetOperator")]
    public async Task<ActionResult<OperatorDto>> GetOperator(Guid id)
    {
        var query = new GetOperator.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Operators.
    /// </summary>
    [HttpGet(Name = "GetOperators")]
    public async Task<IActionResult> GetOperators([FromQuery] OperatorParametersDto operatorParametersDto)
    {
        var query = new GetOperatorList.Query(operatorParametersDto);
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
    /// Updates an entire existing Operator.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateOperator")]
    public async Task<IActionResult> UpdateOperator(Guid id, OperatorForUpdateDto operator)
    {
        var command = new UpdateOperator.Command(id, operator);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Operator record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteOperator")]
    public async Task<ActionResult> DeleteOperator(Guid id)
    {
        var command = new DeleteOperator.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
