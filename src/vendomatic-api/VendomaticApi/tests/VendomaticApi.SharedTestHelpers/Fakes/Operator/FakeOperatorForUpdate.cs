namespace VendomaticApi.SharedTestHelpers.Fakes.Operator;

using AutoBogus;
using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.Models;

public sealed class FakeOperatorForUpdate : AutoFaker<OperatorForUpdate>
{
    public FakeOperatorForUpdate()
    {
    }
}