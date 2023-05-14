namespace VendomaticApi.IntegrationTests.FeatureTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.Domain.VendingMachines.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using VendomaticApi.SharedTestHelpers.Fakes.Operator;

public class DeleteVendingMachineCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_vendingmachine_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeOperatorOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder()
            .WithOperatorId(fakeOperatorOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeVendingMachineOne);
        var vendingMachine = await testingServiceScope.ExecuteDbContextAsync(db => db.VendingMachines
            .FirstOrDefaultAsync(v => v.Id == fakeVendingMachineOne.Id));

        // Act
        var command = new DeleteVendingMachine.Command(vendingMachine.Id);
        await testingServiceScope.SendAsync(command);
        var vendingMachineResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.VendingMachines.CountAsync(v => v.Id == vendingMachine.Id));

        // Assert
        vendingMachineResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_vendingmachine_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteVendingMachine.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_vendingmachine_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeOperatorOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder()
            .WithOperatorId(fakeOperatorOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeVendingMachineOne);
        var vendingMachine = await testingServiceScope.ExecuteDbContextAsync(db => db.VendingMachines
            .FirstOrDefaultAsync(v => v.Id == fakeVendingMachineOne.Id));

        // Act
        var command = new DeleteVendingMachine.Command(vendingMachine.Id);
        await testingServiceScope.SendAsync(command);
        var deletedVendingMachine = await testingServiceScope.ExecuteDbContextAsync(db => db.VendingMachines
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == vendingMachine.Id));

        // Assert
        deletedVendingMachine?.IsDeleted.Should().BeTrue();
    }
}