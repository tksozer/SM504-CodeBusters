namespace VendomaticApi.FunctionalTests.FunctionalTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateMachineOperatorTests : TestBase
{
    [Fact]
    public async Task create_machineoperator_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeMachineOperator = new FakeMachineOperatorForCreationDto().Generate();

        // Act
        var route = ApiRoutes.MachineOperators.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeMachineOperator);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}