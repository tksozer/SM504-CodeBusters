namespace VendomaticApi.IntegrationTests.FeatureTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.Domain.VendingMachines.Dtos;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.VendingMachines.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;

public class UpdateVendingMachineCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_vendingmachine_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeMachineOperatorOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder()
            .WithMachineOperatorId(fakeMachineOperatorOne.Id)
            .Build();
        var updatedVendingMachineDto = new FakeVendingMachineForUpdateDto()
            .RuleFor(v => v.MachineOperatorId, _ => fakeMachineOperatorOne.Id)
            .Generate();
        await testingServiceScope.InsertAsync(fakeVendingMachineOne);

        var vendingMachine = await testingServiceScope.ExecuteDbContextAsync(db => db.VendingMachines
            .FirstOrDefaultAsync(v => v.Id == fakeVendingMachineOne.Id));
        var id = vendingMachine.Id;

        // Act
        var command = new UpdateVendingMachine.Command(id, updatedVendingMachineDto);
        await testingServiceScope.SendAsync(command);
        var updatedVendingMachine = await testingServiceScope.ExecuteDbContextAsync(db => db.VendingMachines.FirstOrDefaultAsync(v => v.Id == id));

        // Assert
        updatedVendingMachine.Alias.Should().Be(updatedVendingMachineDto.Alias);
        updatedVendingMachine.Latitude.Should().Be(updatedVendingMachineDto.Latitude);
        updatedVendingMachine.Longitude.Should().Be(updatedVendingMachineDto.Longitude);
        updatedVendingMachine.MachineType.Should().Be(updatedVendingMachineDto.MachineType);
        updatedVendingMachine.TotalIsleNumber.Should().Be(updatedVendingMachineDto.TotalIsleNumber);
        updatedVendingMachine.Status.Should().Be(updatedVendingMachineDto.Status);
        updatedVendingMachine.MachineOperatorId.Should().Be(updatedVendingMachineDto.MachineOperatorId);
    }
}