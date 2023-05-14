namespace VendomaticApi.IntegrationTests.FeatureTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.Domain.MachineOperators.Features;
using SharedKernel.Exceptions;

public class AddMachineOperatorCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_machineoperator_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorForCreationDto().Generate();

        // Act
        var command = new AddMachineOperator.Command(fakeMachineOperatorOne);
        var machineOperatorReturned = await testingServiceScope.SendAsync(command);
        var machineOperatorCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.MachineOperators
            .FirstOrDefaultAsync(m => m.Id == machineOperatorReturned.Id));

        // Assert
        machineOperatorReturned.CorrelationId.Should().Be(fakeMachineOperatorOne.CorrelationId);
        machineOperatorReturned.Name.Should().Be(fakeMachineOperatorOne.Name);

        machineOperatorCreated.CorrelationId.Should().Be(fakeMachineOperatorOne.CorrelationId);
        machineOperatorCreated.Name.Should().Be(fakeMachineOperatorOne.Name);
    }
}