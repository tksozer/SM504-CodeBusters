namespace VendomaticApi.IntegrationTests.FeatureTests.Inventories;

using VendomaticApi.Domain.Inventories.Dtos;
using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Inventories.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class InventoryListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_inventory_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeInventoryOne = new FakeInventoryBuilder().Build();
        var fakeInventoryTwo = new FakeInventoryBuilder().Build();
        var queryParameters = new InventoryParametersDto();

        await testingServiceScope.InsertAsync(fakeInventoryOne, fakeInventoryTwo);

        // Act
        var query = new GetInventoryList.Query(queryParameters);
        var inventories = await testingServiceScope.SendAsync(query);

        // Assert
        inventories.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}