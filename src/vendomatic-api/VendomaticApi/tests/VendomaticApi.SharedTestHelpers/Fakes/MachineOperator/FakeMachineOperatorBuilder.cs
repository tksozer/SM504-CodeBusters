namespace VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;

using VendomaticApi.Domain.MachineOperators;
using VendomaticApi.Domain.MachineOperators.Models;

public class FakeMachineOperatorBuilder
{
    private MachineOperatorForCreation _creationData = new FakeMachineOperatorForCreation().Generate();

    public FakeMachineOperatorBuilder WithModel(MachineOperatorForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeMachineOperatorBuilder WithCorrelationId(Guid correlationId)
    {
        _creationData.CorrelationId = correlationId;
        return this;
    }
    
    public FakeMachineOperatorBuilder WithName(string name)
    {
        _creationData.Name = name;
        return this;
    }
    
    public MachineOperator Build()
    {
        var result = MachineOperator.Create(_creationData);
        return result;
    }
}