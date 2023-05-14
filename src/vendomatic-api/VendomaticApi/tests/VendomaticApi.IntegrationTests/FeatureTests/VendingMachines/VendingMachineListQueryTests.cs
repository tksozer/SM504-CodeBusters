namespace VendomaticApi.IntegrationTests.FeatureTests.VendingMachines;

using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.VendingMachines.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;

public class VendingMachineListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_vendingmachine_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        var fakeMachineOperatorTwo = new FakeMachineOperatorBuilder().Build();
        await testingServiceScope.InsertAsync(fakeMachineOperatorOne, fakeMachineOperatorTwo);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder()
            .WithMachineOperatorId(fakeMachineOperatorOne.Id)
            .Build();
        var fakeVendingMachineTwo = new FakeVendingMachineBuilder()
            .WithMachineOperatorId(fakeMachineOperatorTwo.Id)
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