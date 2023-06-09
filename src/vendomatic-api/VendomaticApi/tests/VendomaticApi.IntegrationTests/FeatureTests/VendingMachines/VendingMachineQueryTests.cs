namespace VendomaticApi.IntegrationTests.FeatureTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.Domain.VendingMachines.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;

public class VendingMachineQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_vendingmachine_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeMachineOperatorOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder()
            .WithMachineOperatorId(fakeMachineOperatorOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeVendingMachineOne);

        // Act
        var query = new GetVendingMachine.Query(fakeVendingMachineOne.Id);
        var vendingMachine = await testingServiceScope.SendAsync(query);

        // Assert
        vendingMachine.Alias.Should().Be(fakeVendingMachineOne.Alias);
        vendingMachine.Latitude.Should().Be(fakeVendingMachineOne.Latitude);
        vendingMachine.Longitude.Should().Be(fakeVendingMachineOne.Longitude);
        vendingMachine.MachineType.Should().Be(fakeVendingMachineOne.MachineType);
        vendingMachine.TotalIsleNumber.Should().Be(fakeVendingMachineOne.TotalIsleNumber);
        vendingMachine.Status.Should().Be(fakeVendingMachineOne.Status);
        vendingMachine.MachineOperatorId.Should().Be(fakeVendingMachineOne.MachineOperatorId);
    }

    [Fact]
    public async Task get_vendingmachine_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetVendingMachine.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}