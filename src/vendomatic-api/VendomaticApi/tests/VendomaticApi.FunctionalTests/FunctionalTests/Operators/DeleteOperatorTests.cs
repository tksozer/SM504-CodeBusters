namespace VendomaticApi.FunctionalTests.FunctionalTests.Operators;

using VendomaticApi.SharedTestHelpers.Fakes.Operator;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteOperatorTests : TestBase
{
    [Fact]
    public async Task delete_operator_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeOperator = new FakeOperatorBuilder().Build();
        await InsertAsync(fakeOperator);

        // Act
        var route = ApiRoutes.Operators.Delete(fakeOperator.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}