namespace VendomaticApi.FunctionalTests.FunctionalTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.FunctionalTests.TestUtilities;
using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetVendingMachineTests : TestBase
{
    [Fact]
    public async Task get_vendingmachine_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        await InsertAsync(fakeOperatorOne);

        var fakeVendingMachine = new FakeVendingMachineBuilder()
            .WithOperatorId(fakeOperatorOne.Id).Build();
        await InsertAsync(fakeVendingMachine);

        // Act
        var route = ApiRoutes.VendingMachines.GetRecord(fakeVendingMachine.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}