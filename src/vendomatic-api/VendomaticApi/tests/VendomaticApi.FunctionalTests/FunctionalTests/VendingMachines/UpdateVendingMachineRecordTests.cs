namespace VendomaticApi.FunctionalTests.FunctionalTests.VendingMachines;

using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using VendomaticApi.FunctionalTests.TestUtilities;
using VendomaticApi.SharedTestHelpers.Fakes.Operator;
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
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        await InsertAsync(fakeOperatorOne);

        var fakeVendingMachine = new FakeVendingMachineBuilder()
            .WithOperatorId(fakeOperatorOne.Id).Build();
        var updatedVendingMachineDto = new FakeVendingMachineForUpdateDto()
            .RuleFor(v => v.OperatorId, _ => fakeOperatorOne.Id)
            .Generate();
        await InsertAsync(fakeVendingMachine);

        // Act
        var route = ApiRoutes.VendingMachines.Put(fakeVendingMachine.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedVendingMachineDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}