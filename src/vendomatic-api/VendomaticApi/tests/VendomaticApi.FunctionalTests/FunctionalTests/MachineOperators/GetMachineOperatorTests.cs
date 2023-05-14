namespace VendomaticApi.FunctionalTests.FunctionalTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetMachineOperatorTests : TestBase
{
    [Fact]
    public async Task get_machineoperator_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeMachineOperator = new FakeMachineOperatorBuilder().Build();
        await InsertAsync(fakeMachineOperator);

        // Act
        var route = ApiRoutes.MachineOperators.GetRecord(fakeMachineOperator.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}