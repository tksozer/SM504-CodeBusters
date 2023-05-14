namespace VendomaticApi.FunctionalTests.FunctionalTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetMachineOperatorListTests : TestBase
{
    [Fact]
    public async Task get_machineoperator_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.MachineOperators.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}