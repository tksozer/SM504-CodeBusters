namespace VendomaticApi.IntegrationTests.FeatureTests.VendingMachines;

using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.VendingMachines.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.SharedTestHelpers.Fakes.Operator;

public class VendingMachineListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_vendingmachine_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeOperatorOne = new FakeOperatorBuilder().Build();
        var fakeOperatorTwo = new FakeOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeOperatorOne, fakeOperatorTwo);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder()
            .WithOperatorId(fakeOperatorOne.Id)
            .Build();
        var fakeVendingMachineTwo = new FakeVendingMachineBuilder()
            .WithOperatorId(fakeOperatorTwo.Id)
            .Build();
        var queryParameters = new VendingMachineParametersDto();

        await testingServiceScope.InsertAsync(fakeVendingMachineOne, fakeVendingMachineTwo);

        // Act
        var query = new GetVendingMachineList.Query(queryParameters);
        var vendingMachines = await testingServiceScope.SendAsync(query);

        // Assert
        vendingMachines.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}