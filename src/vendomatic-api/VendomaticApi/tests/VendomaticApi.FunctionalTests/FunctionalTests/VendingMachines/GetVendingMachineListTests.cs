namespace VendomaticApi.FunctionalTests.FunctionalTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetVendingMachineListTests : TestBase
{
    [Fact]
    public async Task get_vendingmachine_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.VendingMachines.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}