namespace VendomaticApi.IntegrationTests.FeatureTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.Domain.MachineOperators.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeleteMachineOperatorCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_machineoperator_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeMachineOperatorOne);
        var machineOperator = await testingServiceScope.ExecuteDbContextAsync(db => db.MachineOperators
            .FirstOrDefaultAsync(m => m.Id == fakeMachineOperatorOne.Id));

        // Act
        var command = new DeleteMachineOperator.Command(machineOperator.Id);
        await testingServiceScope.SendAsync(command);
        var machineOperatorResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.MachineOperators.CountAsync(m => m.Id == machineOperator.Id));

        // Assert
        machineOperatorResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_machineoperator_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteMachineOperator.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_machineoperator_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeMachineOperatorOne);
        var machineOperator = await testingServiceScope.ExecuteDbContextAsync(db => db.MachineOperators
            .FirstOrDefaultAsync(m => m.Id == fakeMachineOperatorOne.Id));

        // Act
        var command = new DeleteMachineOperator.Command(machineOperator.Id);
        await testingServiceScope.SendAsync(command);
        var deletedMachineOperator = await testingServiceScope.ExecuteDbContextAsync(db => db.MachineOperators
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == machineOperator.Id));

        // Assert
        deletedMachineOperator?.IsDeleted.Should().BeTrue();
    }
}