namespace VendomaticApi.SharedTestHelpers.Fakes.Operator;

using AutoBogus;
using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.Models;

public sealed class FakeOperatorForCreation : AutoFaker<OperatorForCreation>
{
    public FakeOperatorForCreation()
    {
    }
}