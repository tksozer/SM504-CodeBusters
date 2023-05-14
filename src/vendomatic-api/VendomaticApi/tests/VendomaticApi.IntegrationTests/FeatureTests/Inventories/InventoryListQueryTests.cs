namespace VendomaticApi.IntegrationTests.FeatureTests.Inventories;

using VendomaticApi.Domain.Inventories.Dtos;
using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Inventories.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

public class InventoryListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_inventory_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductBuilder().Build();
        var fakeProductTwo = new FakeProductBuilder().Build();
        await testingServiceScope.InsertAsync(fakeProductOne, fakeProductTwo);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder().Build();
        var fakeVendingMachineTwo = new FakeVendingMachineBuilder().Build();
        await testingServiceScope.InsertAsync(fakeVendingMachineOne, fakeVendingMachineTwo);

        var fakeInventoryOne = new FakeInventoryBuilder()
            .WithProductId(fakeProductOne.Id)
            .WithVendingMachineId(fakeVendingMachineOne.Id)
            .Build();
        var fakeInventoryTwo = new FakeInventoryBuilder()
            .WithProductId(fakeProductTwo.Id)
            .WithVendingMachineId(fakeVendingMachineTwo.Id)
            .Build();
        var queryParameters = new InventoryParametersDto();

        await testingServiceScope.InsertAsync(fakeInventoryOne, fakeInventoryTwo);

        // Act
        var query = new GetInventoryList.Query(queryParameters);
        var inventories = await testingServiceScope.SendAsync(query);

        // Assert
        inventories.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}