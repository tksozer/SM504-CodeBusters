namespace VendomaticApi.UnitTests.Domain.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.Domain.MachineOperators;
using VendomaticApi.Domain.MachineOperators.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateMachineOperatorTests
{
    private readonly Faker _faker;

    public CreateMachineOperatorTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_machineOperator()
    {
        // Arrange
        var machineOperatorToCreate = new FakeMachineOperatorForCreation().Generate();
        
        // Act
        var fakeMachineOperator = MachineOperator.Create(machineOperatorToCreate);

        // Assert
        fakeMachineOperator.CorrelationId.Should().Be(machineOperatorToCreate.CorrelationId);
        fakeMachineOperator.Name.Should().Be(machineOperatorToCreate.Name);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var machineOperatorToCreate = new FakeMachineOperatorForCreation().Generate();
        
        // Act
        var fakeMachineOperator = MachineOperator.Create(machineOperatorToCreate);

        // Assert
        fakeMachineOperator.DomainEvents.Count.Should().Be(1);
        fakeMachineOperator.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(MachineOperatorCreated));
    }
}