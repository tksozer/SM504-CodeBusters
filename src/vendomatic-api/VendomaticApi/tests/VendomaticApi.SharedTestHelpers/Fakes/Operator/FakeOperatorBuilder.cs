namespace VendomaticApi.SharedTestHelpers.Fakes.Operator;

using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.Models;

public class FakeOperatorBuilder
{
    private OperatorForCreation _creationData = new FakeOperatorForCreation().Generate();

    public FakeOperatorBuilder WithModel(OperatorForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeOperatorBuilder WithCorrelationId(Guid correlationId)
    {
        _creationData.CorrelationId = correlationId;
        return this;
    }
    
    public Operator Build()
    {
        var result = Operator.Create(_creationData);
        return result;
    }
}