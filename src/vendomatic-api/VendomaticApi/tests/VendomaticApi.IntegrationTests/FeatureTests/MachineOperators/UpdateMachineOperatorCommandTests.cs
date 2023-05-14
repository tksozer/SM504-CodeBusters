namespace VendomaticApi.IntegrationTests.FeatureTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.Domain.MachineOperators.Dtos;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.MachineOperators.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdateMachineOperatorCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_machineoperator_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        var updatedMachineOperatorDto = new FakeMachineOperatorForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeMachineOperatorOne);

        var machineOperator = await testingServiceScope.ExecuteDbContextAsync(db => db.MachineOperators
            .FirstOrDefaultAsync(m => m.Id == fakeMachineOperatorOne.Id));
        var id = machineOperator.Id;

        // Act
        var command = new UpdateMachineOperator.Command(id, updatedMachineOperatorDto);
        await testingServiceScope.SendAsync(command);
        var updatedMachineOperator = await testingServiceScope.ExecuteDbContextAsync(db => db.MachineOperators.FirstOrDefaultAsync(m => m.Id == id));

        // Assert
        updatedMachineOperator.CorrelationId.Should().Be(updatedMachineOperatorDto.CorrelationId);
        updatedMachineOperator.Name.Should().Be(updatedMachineOperatorDto.Name);
    }
}