namespace VendomaticApi.UnitTests.Domain.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.Domain.MachineOperators;
using VendomaticApi.Domain.MachineOperators.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateMachineOperatorTests
{
    private readonly Faker _faker;

    public UpdateMachineOperatorTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_machineOperator()
    {
        // Arrange
        var fakeMachineOperator = new FakeMachineOperatorBuilder().Build();
        var updatedMachineOperator = new FakeMachineOperatorForUpdate().Generate();
        
        // Act
        fakeMachineOperator.Update(updatedMachineOperator);

        // Assert
        fakeMachineOperator.CorrelationId.Should().Be(updatedMachineOperator.CorrelationId);
        fakeMachineOperator.Name.Should().Be(updatedMachineOperator.Name);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeMachineOperator = new FakeMachineOperatorBuilder().Build();
        var updatedMachineOperator = new FakeMachineOperatorForUpdate().Generate();
        fakeMachineOperator.DomainEvents.Clear();
        
        // Act
        fakeMachineOperator.Update(updatedMachineOperator);

        // Assert
        fakeMachineOperator.DomainEvents.Count.Should().Be(1);
        fakeMachineOperator.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(MachineOperatorUpdated));
    }
}