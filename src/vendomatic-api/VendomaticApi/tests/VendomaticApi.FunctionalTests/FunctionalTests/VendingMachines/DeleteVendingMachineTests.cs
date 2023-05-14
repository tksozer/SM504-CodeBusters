namespace VendomaticApi.FunctionalTests.FunctionalTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.FunctionalTests.TestUtilities;
using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteVendingMachineTests : TestBase
{
    [Fact]
    public async Task delete_vendingmachine_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        await InsertAsync(fakeMachineOperatorOne);

        var fakeVendingMachine = new FakeVendingMachineBuilder()
            .WithMachineOperatorId(fakeMachineOperatorOne.Id).Build();
        await InsertAsync(fakeVendingMachine);

        // Act
        var route = ApiRoutes.VendingMachines.Delete(fakeVendingMachine.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}