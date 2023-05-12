namespace VendomaticApi.IntegrationTests.FeatureTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.Domain.VendingMachines.Features;
using SharedKernel.Exceptions;

public class AddVendingMachineCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_vendingmachine_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeVendingMachineOne = new FakeVendingMachineForCreationDto().Generate();

        // Act
        var command = new AddVendingMachine.Command(fakeVendingMachineOne);
        var vendingMachineReturned = await testingServiceScope.SendAsync(command);
        var vendingMachineCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.VendingMachines
            .FirstOrDefaultAsync(v => v.Id == vendingMachineReturned.Id));

        // Assert
        vendingMachineReturned.VendingMachineId.Should().Be(fakeVendingMachineOne.VendingMachineId);
        vendingMachineReturned.Alias.Should().Be(fakeVendingMachineOne.Alias);
        vendingMachineReturned.Latitude.Should().Be(fakeVendingMachineOne.Latitude);
        vendingMachineReturned.Longitude.Should().Be(fakeVendingMachineOne.Longitude);
        vendingMachineReturned.Type.Should().Be(fakeVendingMachineOne.Type);
        vendingMachineReturned.TotalIsleNumber.Should().Be(fakeVendingMachineOne.TotalIsleNumber);
        vendingMachineReturned.Status.Should().Be(fakeVendingMachineOne.Status);

        vendingMachineCreated.VendingMachineId.Should().Be(fakeVendingMachineOne.VendingMachineId);
        vendingMachineCreated.Alias.Should().Be(fakeVendingMachineOne.Alias);
        vendingMachineCreated.Latitude.Should().Be(fakeVendingMachineOne.Latitude);
        vendingMachineCreated.Longitude.Should().Be(fakeVendingMachineOne.Longitude);
        vendingMachineCreated.Type.Should().Be(fakeVendingMachineOne.Type);
        vendingMachineCreated.TotalIsleNumber.Should().Be(fakeVendingMachineOne.TotalIsleNumber);
        vendingMachineCreated.Status.Should().Be(fakeVendingMachineOne.Status);
    }
}