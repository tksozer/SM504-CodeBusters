namespace VendomaticApi.IntegrationTests.FeatureTests.MachineOperators;

using VendomaticApi.Domain.MachineOperators.Dtos;
using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.MachineOperators.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class MachineOperatorListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_machineoperator_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeMachineOperatorOne = new FakeMachineOperatorBuilder().Build();
        var fakeMachineOperatorTwo = new FakeMachineOperatorBuilder().Build();
        var queryParameters = new MachineOperatorParametersDto();

        await testingServiceScope.InsertAsync(fakeMachineOperatorOne, fakeMachineOperatorTwo);

        // Act
        var query = new GetMachineOperatorList.Query(queryParameters);
        var machineOperators = await testingServiceScope.SendAsync(query);

        // Assert
        machineOperators.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}