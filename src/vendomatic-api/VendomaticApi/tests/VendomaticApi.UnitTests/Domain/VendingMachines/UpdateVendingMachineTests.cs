namespace VendomaticApi.UnitTests.Domain.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateVendingMachineTests
{
    private readonly Faker _faker;

    public UpdateVendingMachineTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_vendingMachine()
    {
        // Arrange
        var fakeVendingMachine = new FakeVendingMachineBuilder().Build();
        var updatedVendingMachine = new FakeVendingMachineForUpdate().Generate();
        
        // Act
        fakeVendingMachine.Update(updatedVendingMachine);

        // Assert
        fakeVendingMachine.Alias.Should().Be(updatedVendingMachine.Alias);
        fakeVendingMachine.Latitude.Should().Be(updatedVendingMachine.Latitude);
        fakeVendingMachine.Longitude.Should().Be(updatedVendingMachine.Longitude);
        fakeVendingMachine.MachineType.Should().Be(updatedVendingMachine.MachineType);
        fakeVendingMachine.TotalIsleNumber.Should().Be(updatedVendingMachine.TotalIsleNumber);
        fakeVendingMachine.Status.Should().Be(updatedVendingMachine.Status);
        fakeVendingMachine.OperatorId.Should().Be(updatedVendingMachine.OperatorId);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeVendingMachine = new FakeVendingMachineBuilder().Build();
        var updatedVendingMachine = new FakeVendingMachineForUpdate().Generate();
        fakeVendingMachine.DomainEvents.Clear();
        
        // Act
        fakeVendingMachine.Update(updatedVendingMachine);

        // Assert
        fakeVendingMachine.DomainEvents.Count.Should().Be(1);
        fakeVendingMachine.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(VendingMachineUpdated));
    }
}