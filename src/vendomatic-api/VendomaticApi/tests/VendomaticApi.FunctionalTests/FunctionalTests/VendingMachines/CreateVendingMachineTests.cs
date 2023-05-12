namespace VendomaticApi.FunctionalTests.FunctionalTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateVendingMachineTests : TestBase
{
    [Fact]
    public async Task create_vendingmachine_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeVendingMachine = new FakeVendingMachineForCreationDto().Generate();

        // Act
        var route = ApiRoutes.VendingMachines.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeVendingMachine);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}