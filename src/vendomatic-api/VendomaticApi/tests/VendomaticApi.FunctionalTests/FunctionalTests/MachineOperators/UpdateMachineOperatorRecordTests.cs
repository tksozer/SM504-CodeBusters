namespace VendomaticApi.FunctionalTests.FunctionalTests.MachineOperators;

using VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateMachineOperatorRecordTests : TestBase
{
    [Fact]
    public async Task put_machineoperator_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeMachineOperator = new FakeMachineOperatorBuilder().Build();
        var updatedMachineOperatorDto = new FakeMachineOperatorForUpdateDto().Generate();
        await InsertAsync(fakeMachineOperator);

        // Act
        var route = ApiRoutes.MachineOperators.Put(fakeMachineOperator.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedMachineOperatorDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}