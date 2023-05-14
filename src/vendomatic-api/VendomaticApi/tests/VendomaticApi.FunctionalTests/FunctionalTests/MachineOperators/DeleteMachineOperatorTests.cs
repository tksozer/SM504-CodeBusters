namespace VendomaticApi.FunctionalTests.FunctionalTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteMachineOperatorTests : TestBase
{
    [Fact]
    public async Task delete_machineoperator_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeMachineOperator = new FakeMachineOperatorBuilder().Build();
        await InsertAsync(fakeMachineOperator);

        // Act
        var route = ApiRoutes.MachineOperators.Delete(fakeMachineOperator.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}