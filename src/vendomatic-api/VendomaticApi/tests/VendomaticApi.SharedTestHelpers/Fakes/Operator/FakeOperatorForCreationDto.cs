namespace VendomaticApi.SharedTestHelpers.Fakes.Operator;

using AutoBogus;
using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.Dtos;

public sealed class FakeOperatorForCreationDto : AutoFaker<OperatorForCreationDto>
{
    public FakeOperatorForCreationDto()
    {
    }
}