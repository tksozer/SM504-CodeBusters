namespace VendomaticApi.FunctionalTests.FunctionalTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateOperatorRecordTests : TestBase
{
    [Fact]
    public async Task put_operator_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeOperator = new FakeOperatorBuilder().Build();
        var updatedOperatorDto = new FakeOperatorForUpdateDto().Generate();
        await InsertAsync(fakeOperator);

        // Act
        var route = ApiRoutes.Operators.Put(fakeOperator.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedOperatorDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}