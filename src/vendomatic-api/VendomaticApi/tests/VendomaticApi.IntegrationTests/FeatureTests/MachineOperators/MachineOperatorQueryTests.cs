namespace VendomaticApi.IntegrationTests.FeatureTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.Domain.MachineOperators.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class MachineOperatorQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_machineoperator_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeMachineOperatorOne);

        // Act
        var query = new GetMachineOperator.Query(fakeMachineOperatorOne.Id);
        var machineOperator = await testingServiceScope.SendAsync(query);

        // Assert
        machineOperator.CorrelationId.Should().Be(fakeMachineOperatorOne.CorrelationId);
        machineOperator.Name.Should().Be(fakeMachineOperatorOne.Name);
    }

    [Fact]
    public async Task get_machineoperator_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetMachineOperator.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}