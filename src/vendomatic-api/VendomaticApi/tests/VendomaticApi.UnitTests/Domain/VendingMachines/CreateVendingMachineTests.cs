namespace VendomaticApi.UnitTests.Domain.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateVendingMachineTests
{
    private readonly Faker _faker;

    public CreateVendingMachineTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_vendingMachine()
    {
        // Arrange
        var vendingMachineToCreate = new FakeVendingMachineForCreation().Generate();
        
        // Act
        var fakeVendingMachine = VendingMachine.Create(vendingMachineToCreate);

        // Assert
        fakeVendingMachine.VendingMachineId.Should().Be(vendingMachineToCreate.VendingMachineId);
        fakeVendingMachine.Alias.Should().Be(vendingMachineToCreate.Alias);
        fakeVendingMachine.Latitude.Should().Be(vendingMachineToCreate.Latitude);
        fakeVendingMachine.Longitude.Should().Be(vendingMachineToCreate.Longitude);
        fakeVendingMachine.Type.Should().Be(vendingMachineToCreate.Type);
        fakeVendingMachine.TotalIsleNumber.Should().Be(vendingMachineToCreate.TotalIsleNumber);
        fakeVendingMachine.Status.Should().Be(vendingMachineToCreate.Status);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var vendingMachineToCreate = new FakeVendingMachineForCreation().Generate();
        
        // Act
        var fakeVendingMachine = VendingMachine.Create(vendingMachineToCreate);

        // Assert
        fakeVendingMachine.DomainEvents.Count.Should().Be(1);
        fakeVendingMachine.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(VendingMachineCreated));
    }
}