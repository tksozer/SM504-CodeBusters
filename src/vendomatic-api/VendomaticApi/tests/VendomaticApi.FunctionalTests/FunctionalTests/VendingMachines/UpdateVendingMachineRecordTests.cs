namespace VendomaticApi.FunctionalTests.FunctionalTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateVendingMachineRecordTests : TestBase
{
    [Fact]
    public async Task put_vendingmachine_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeVendingMachine = new FakeVendingMachineBuilder().Build();
        var updatedVendingMachineDto = new FakeVendingMachineForUpdateDto().Generate();
        await InsertAsync(fakeVendingMachine);

        // Act
        var route = ApiRoutes.VendingMachines.Put(fakeVendingMachine.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedVendingMachineDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}